using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeLink {
    public static float lineHeight = 0.15f;
    public static Material lineMaterial = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Line.mat");
    Node firstNode;
    Node secondNode;
    LineRenderer lineRenderer;
    public NodeLink(Node firstNode, Node secondNode)
    {
        this.firstNode = firstNode;
        this.secondNode = secondNode;
    }

    public Node FirstNode { get => firstNode; }
    public Node SecondNode { get => secondNode; }
    public void CreateLine()
    {
        if (lineRenderer == null)
        {
            lineRenderer = firstNode.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineHeight;
            lineRenderer.endWidth = lineHeight;
            lineRenderer.material = lineMaterial;
            lineRenderer.sortingOrder = -1;
            lineRenderer.SetPosition(0, firstNode.transform.position);
            lineRenderer.SetPosition(1, secondNode.transform.position);
        }
    }
    public override bool Equals(object obj)
    {
        if (obj is NodeLink)
        {
            var nodeLink = obj as NodeLink;
            if (firstNode == nodeLink.firstNode && secondNode == nodeLink.secondNode || firstNode == nodeLink.secondNode && secondNode == nodeLink.firstNode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
