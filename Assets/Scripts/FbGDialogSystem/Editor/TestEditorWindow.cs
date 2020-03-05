using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestEditorWindow : EditorWindow
{

    private List<FbG_Node> nodes;

    private GUIStyle nodeStyle;


    [MenuItem("FbG/Dialog Editor")]
    public static void ShowWindow()
    {
        TestEditorWindow window = GetWindow<TestEditorWindow>(false, "Dialog Editor", true);

    }

    private void OnEnable()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);
    }


    private void OnGUI()
    {
        DrawNodes();

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();


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

    private void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
            if(e.button == 1)
            {
                ProcessContextMenu(e.mousePosition);
            }
            break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i > 0; i--)
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

       
    private void OnClickAddNode(Vector2 mousePosition)
    {
        if(nodes == null)
        {
            nodes = new List<FbG_Node>();
        }

        nodes.Add(new FbG_Node(mousePosition, 200, 50, nodeStyle));
    }
}
