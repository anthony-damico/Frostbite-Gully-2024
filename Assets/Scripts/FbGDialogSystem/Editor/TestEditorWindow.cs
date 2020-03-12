using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TestEditorWindow : EditorWindow
{

    private List<FbG_Node> nodes;
    private List<FbG_Connection> connections;

    private GUIStyle nodeStyle;
    private GUIStyle selectedNodeStyle;
    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;

    private FbG_ConnectionPoint selectedInPoint;
    private FbG_ConnectionPoint selectedOutPoint;

    private Vector2 offset;
    private Vector2 drag;

    private string eventName;

    private float menuBarHeight = 20.0f;
    private Rect menuBar;

    [MenuItem("FbG/Dialog Editor")]
    public static void OpenWindow()
    {
        TestEditorWindow window = GetWindow<TestEditorWindow>(false, "Dialog Editor", true);
        window.titleContent = new GUIContent("Dialog Editor");

    }

    private void OnEnable()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);

        eventName = "";
    }

    private void OnGUI()
    {

        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);
        DrawMenuBar();

        DrawNodes();
        DrawConnections();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    private void DrawMenuBar()
    {
        menuBar = new Rect(0, 0, position.width, menuBarHeight);

        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        GUILayout.Label(new GUIContent("Event Name"), GUILayout.Width(100));
        eventName = EditorGUILayout.TextField("", eventName, GUILayout.Width(150)).ToString();
        
        
        // Need to serialize the entire Editor Window with unique name
        // 

        GUILayout.Space(5);

        if (GUILayout.Button(new GUIContent("Save"), EditorStyles.toolbarButton, GUILayout.Width(35)))
        {
            Save();
        }

        GUILayout.Space(5);

        if(GUILayout.Button(new GUIContent("Load"), EditorStyles.toolbarButton, GUILayout.Width(35)))
        {
            Load();
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;

        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void DrawNodes()
    {
        if(nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw();
            }
        }
    }

    private void DrawConnections()
    {
        if(connections != null)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }
        }
    }

    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
            if(e.button == 1)
            {
                ProcessContextMenu(e.mousePosition);
            }
            break;

            case EventType.MouseDrag:
            if(e.button == 0)
            {
                OnDrag(e.delta);
            }
            break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(e);

                if(guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Node"), false, () => OnClickAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    private void DrawConnectionLine(Event e)
    {
        if(selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
                );

            GUI.changed = true;
        }

        if(selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
                );

            GUI.changed = true;
        }
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        if(nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Drag(delta);
            }
        }

        GUI.changed = true;
    }
       
    private void OnClickAddNode(Vector2 mousePosition)
    {
        if(nodes == null)
        {
            nodes = new List<FbG_Node>();
        }

        nodes.Add(new FbG_Node(mousePosition, 250, 300, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
    }

    private void OnClickInPoint(FbG_ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if(selectedOutPoint != null)
        {
            if(selectedOutPoint.node != selectedInPoint.node) // Not the same node
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }

    }

    private void OnClickOutPoint(FbG_ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node) // Not the same node
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }

    }

    private void OnClickRemoveNode(FbG_Node node)
    {
        if(connections != null)
        {
            List<FbG_Connection> connectionsToRemove = new List<FbG_Connection>();

            for (int i = 0; i < connections.Count; i++)
            {
                if(connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
                {
                    connectionsToRemove.Add(connections[i]);
                }
            }

            for (int i = 0; i < connectionsToRemove.Count; i++)
            {
                connections.Remove(connectionsToRemove[i]);
            }

            connectionsToRemove = null;
        }

        nodes.Remove(node);
    }

    private void OnClickRemoveConnection(FbG_Connection connection)
    {
        connections.Remove(connection);
    }

    private void CreateConnection()
    {
        if(connections == null)
        {
            connections = new List<FbG_Connection>();
        }

        connections.Add(new FbG_Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    private void Save()
    {
        Debug.Log("Assets/Resources/" + eventName + ".xml");

        if(nodes != null)
        {
            XMLOp.Serialize(nodes, "Assets/Resources/" + eventName + "_nodes.xml");
        }

        if (connections != null)
        {
            XMLOp.Serialize(connections, "Assets/Resources/" + eventName + "_connections.xml");
        }

    }

    private void Load()
    {
        var nodesDeserialized = XMLOp.Deserialize<List<FbG_Node>>("Assets/Resources/" + eventName + "_nodes.xml");
        var connectionsDeserialized = XMLOp.Deserialize<List<FbG_Connection>>("Assets/Resources/" + eventName + "_connections.xml");

        nodes = new List<FbG_Node>();
        connections = new List<FbG_Connection>();

        foreach (var nodeDeserialized in nodesDeserialized)
        {
            //nodes.Add(new FbG_Node(
            //    nodeDeserialized.rect.position,
            //    nodeDeserialized.rect.width,
            //    nodeDeserialized.rect.height,
            //    nodeStyle,
            //    selectedNodeStyle,
            //    inPointStyle,
            //    outPointStyle,
            //    OnClickInPoint,
            //    OnClickOutPoint,
            //    OnClickRemoveNode,
            //    nodeDeserialized.inPoint.id,
            //    nodeDeserialized.outPoint.id
            //    )
            //);

            nodes.Add(new FbG_Node(
                nodeDeserialized.rect.position,
                nodeDeserialized.rect.width,
                nodeDeserialized.rect.height,
                nodeStyle,
                selectedNodeStyle,
                inPointStyle,
                outPointStyle,
                OnClickInPoint,
                OnClickOutPoint,
                OnClickRemoveNode,
                nodeDeserialized.inPoint.id,
                nodeDeserialized.outPoint.id,
                nodeDeserialized.lines
                )
            );
        }

        foreach (var connectionDeserialized in connectionsDeserialized)
        {
            var inPoint = nodes.First(n => n.inPoint.id == connectionDeserialized.inPoint.id).inPoint;
            var outPoint = nodes.First(n => n.outPoint.id == connectionDeserialized.outPoint.id).outPoint;
            connections.Add(new FbG_Connection(inPoint, outPoint, OnClickRemoveConnection));
        }
    }
}
