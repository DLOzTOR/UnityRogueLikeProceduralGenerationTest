using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeLink : MonoBehaviour {
    public static float lineHeight = 0.15f;
    [SerializeField]
    public Material lineMaterial;
    [SerializeField]
    Node firstNode;
    [SerializeField]
    Node secondNode;
    LineRenderer lineRenderer;
    public Node FirstNode { get => firstNode; set { if (firstNode == null) firstNode = value; } }
    public Node SecondNode { get => secondNode; set { if (secondNode == null) secondNode = value; } }

    public Color Color { set => lineRenderer.SetColors(value, value); }

    public static NodeLink Create(Node firstNode, Node secondNode, NodeLink instance)
    {
        var nodeLink = Instantiate(instance, firstNode.transform, firstNode);
        nodeLink.FirstNode = firstNode;
        nodeLink.SecondNode = secondNode;
        firstNode.AddLinkedNode(secondNode);
        secondNode.AddLinkedNode(firstNode);
        return nodeLink;
    }
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
    public bool Equals(Node n1, Node n2)
    {
        if (FirstNode == n1 && SecondNode == n2 || FirstNode == n2 && SecondNode == n1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
