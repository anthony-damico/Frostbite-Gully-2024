using System;
using UnityEditor;
using UnityEngine;

public class FbG_Connection
{
    public FbG_ConnectionPoint inPoint;
    public FbG_ConnectionPoint outPoint;
    public Action<FbG_Connection> OnClickRemoveConnection;

    public FbG_Connection(FbG_ConnectionPoint inPoint, FbG_ConnectionPoint outPoint, Action<FbG_Connection> OnClickRemoveConnection)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        this.OnClickRemoveConnection = OnClickRemoveConnection;
    }

    public void Draw()
    {
        Handles.DrawBezier(
            inPoint.rect.center,
            outPoint.rect.center,
            inPoint.rect.center + Vector2.left * 50f,
            outPoint.rect.center + Vector2.right * 50f,
            Color.white,
            null,
            2f
            );

        if(Handles.Button((inPoint.rect.center + outPoint.rect.center)* 0.5f, Quaternion.identity, 4, 8, Handles.RectangleCap))
        {
            if(OnClickRemoveConnection != null)
            {
                OnClickRemoveConnection(this);
            }
        }
    }
}
