using System;
using UnityEngine;
using System.Xml.Serialization;

public enum ConnectionPointType { In, Out};

public class FbG_ConnectionPoint
{
    public string id;

    [XmlIgnore] public Rect rect;

    [XmlIgnore] public ConnectionPointType type;

    [XmlIgnore] public FbG_Node node;

    [XmlIgnore] public GUIStyle style;

    [XmlIgnore] public Action<FbG_ConnectionPoint> OnClickConnectionPoint;

    public FbG_ConnectionPoint() { }

    public FbG_ConnectionPoint(FbG_Node node, ConnectionPointType type, GUIStyle style, Action<FbG_ConnectionPoint> OnClickConnectionPoint, string id = null)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        rect = new Rect(0, 0, 10.0f, 20.0f);

        this.id = id ?? Guid.NewGuid().ToString();
    }

    public void Draw()
    {
        rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

        switch(type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                break;
        }

        if (GUI.Button(rect, "", style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }
}
