using System;
using UnityEngine;


public class MiniMapRendererLink : MonoBehaviour
{
    [NonSerialized] public Color viewportLineColor;

    //Viewport
    [NonSerialized] public Vector3 bottomLeftPosition;
    [NonSerialized] public Vector3 bottomRightPosition;
    [NonSerialized] public Vector3 topLeftPosition;
    [NonSerialized] public Vector3 topRightPosition;


    private void OnPostRender()
    {
        GL.PushMatrix();
        {
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            {
                GL.Color(viewportLineColor);
                GL.Vertex(topLeftPosition);
                GL.Vertex(topRightPosition);
                GL.Vertex(topRightPosition);
                GL.Vertex(bottomRightPosition);
                GL.Vertex(bottomRightPosition);
                GL.Vertex(bottomLeftPosition);
                GL.Vertex(bottomLeftPosition);
                GL.Vertex(topLeftPosition);
            }
            GL.End();
        }
        GL.PopMatrix();
    }
}
