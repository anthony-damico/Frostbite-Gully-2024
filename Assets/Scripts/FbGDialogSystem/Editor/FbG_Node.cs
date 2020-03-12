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
        GUI.Label(new Rect(rect.x + 15, rect.y + 10, 100, 20), "Name", fontStyle);

        GUI.Label(new Rect(rect.x + 15, rect.y + 25, 50, 20), "Line 1:", fontStyle);

        line1 = EditorGUI.TextArea(new Rect(rect.x + 15, rect.y + 40, rect.width - 30, 100), line1);

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
