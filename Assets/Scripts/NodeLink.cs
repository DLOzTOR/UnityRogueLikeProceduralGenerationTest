using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeLink : MonoBehaviour{
    public static float lineHeight = 0.15f;
    public static Material lineMaterial = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Line.mat");
    [SerializeField]
    Node firstNode;
    [SerializeField]
    Node secondNode;
    LineRenderer lineRenderer;
    public NodeLink(Node firstNode, Node secondNode)
    {
        FirstNode = firstNode;
        SecondNode = secondNode;
        Instantiate(this);
        transform.parent = firstNode.transform;
    }

    public Node FirstNode { get => firstNode; set => firstNode = value; }
    public Node SecondNode { get => secondNode; set => secondNode = value; }

    public void CreateLine()
    {
        if (lineRenderer == null)
        {
            lineRenderer = this.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineHeight;
            lineRenderer.endWidth = lineHeight;
            lineRenderer.material = lineMaterial;
            lineRenderer.sortingOrder = -1;
            lineRenderer.SetPosition(0, FirstNode.transform.position);
            lineRenderer.SetPosition(1, SecondNode.transform.position);
        }
    }
    public override bool Equals(object obj)
    {
        if (obj is NodeLink)
        {
            var nodeLink = obj as NodeLink;
            if (FirstNode == nodeLink.FirstNode && SecondNode == nodeLink.SecondNode || FirstNode == nodeLink.SecondNode && SecondNode == nodeLink.FirstNode)
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
