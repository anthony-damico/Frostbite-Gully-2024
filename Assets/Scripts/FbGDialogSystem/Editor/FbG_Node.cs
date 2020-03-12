using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Xml.Serialization;


public class FbG_Node
{
    public Rect rect;

    [XmlAttribute("title")] public string title;
    [XmlIgnore] private bool isDragged;
    [XmlIgnore] private bool isSelected;
    [XmlIgnore] private bool isResized;

    public FbG_ConnectionPoint inPoint;
    public FbG_ConnectionPoint outPoint;

    [XmlIgnore] public GUIStyle style;
    [XmlIgnore] public GUIStyle defaultNodeStyle;
    [XmlIgnore] public GUIStyle selectedNodeStyle;


    [XmlIgnore] public GUIStyle fontStyle;

    public string[] lines = new string[5];
    [XmlIgnore] public string line1 = "";
    [XmlIgnore] public string line2 = "";
    [XmlIgnore] public string line3 = "";
    [XmlIgnore] public string line4 = "";
    [XmlIgnore] public string line5 = "";

    [XmlIgnore] private int numLines = 1;

    [XmlIgnore] public Action<FbG_Node> OnRemoveNode;

    public FbG_Node() { }

    public FbG_Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<FbG_ConnectionPoint> OnClickInPoint, Action<FbG_ConnectionPoint> OnClickOutPoint, Action<FbG_Node> OnClickRemoveNode)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new FbG_ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new FbG_ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;

    }

   
	public FbG_Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<FbG_ConnectionPoint> OnClickInPoint, Action<FbG_ConnectionPoint> OnClickOutPoint, Action<FbG_Node> OnClickRemoveNode, string inPointID, string outPointID)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new FbG_ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint, inPointID);
        outPoint = new FbG_ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint, outPointID);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
    }

    public FbG_Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<FbG_ConnectionPoint> OnClickInPoint, Action<FbG_ConnectionPoint> OnClickOutPoint, Action<FbG_Node> OnClickRemoveNode, string inPointID, string outPointID, string[] decodedLines)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new FbG_ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint, inPointID);
        outPoint = new FbG_ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint, outPointID);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;

        int count = 0;

        for (int i = 0; i < decodedLines.Length - 1; i++)
        {
            if (decodedLines[i] != "")
                count++;
        }

        numLines = count;

        Debug.Log("Count: " + count);

        line1 = decodedLines[0];
        line2 = decodedLines[1];
        line3 = decodedLines[2];
        line4 = decodedLines[3];
        line5 = decodedLines[4];

    }

    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
        //GUILayout.TextField("Name", GUILayout.ExpandWidth(true));
        //GUI.TextField(new Rect(rect.x + 15, rect.y + 10, 100, 20), "name");
        fontStyle = new GUIStyle();
        fontStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(rect.x + 15, rect.y + 10, 100, 20), "Dialogue Node", fontStyle);


        GUI.Label(new Rect(rect.x + 15, rect.y + 30, 100, 20), "Add New Line", fontStyle);
        if (GUI.Button(new Rect(rect.x + 100, rect.y + 25, 20, 20), "+") && numLines < 2)
        {
            numLines++;
            //GUI.changed = true;
        }

        GUI.Label(new Rect(rect.x + 15, rect.y + 50, 100, 20), "Del Last Line", fontStyle);
        if (GUI.Button(new Rect(rect.x + 100, rect.y + 45, 20, 20), "-") && numLines > 1)
        {
            numLines--;
            //GUI.changed = true;
        }

        switch (numLines)
        {
            case (2):
                GUI.Label(new Rect(rect.x + 15, rect.y + 70, 50, 20), "Line 1:", fontStyle);
                line1 = EditorGUI.TextArea(new Rect(rect.x + 15, rect.y + 85, rect.width - 30, 50), line1);

                GUI.Label(new Rect(rect.x + 15, rect.y + 140, 50, 20), "Line 2:", fontStyle);
                line2 = EditorGUI.TextArea(new Rect(rect.x + 15, rect.y + 155, rect.width - 30, 50), line2);
                break;

            case (1):
                GUI.Label(new Rect(rect.x + 15, rect.y + 70, 50, 20), "Line 1:", fontStyle);
                line1 = EditorGUI.TextArea(new Rect(rect.x + 15, rect.y + 85, rect.width - 30, 50), line1);
                break;
        }


        //lines.Enqueue(Line);
        GUI.changed = true;
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if(e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if(e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
            break;

            case EventType.MouseDrag:
                if(e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
            break;

            default:
                lines[0] = line1;
                lines[1] = line2;
                lines[2] = line3;
                lines[3] = line4;
                lines[4] = line5;
                break;
        }
        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove Node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if(OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }
}
