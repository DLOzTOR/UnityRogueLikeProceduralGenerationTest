using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public NodeType type;
    public List<Node> linkedNodes = new List<Node>();
    public void AddLinkedNode(Node node)
    {
        if (!linkedNodes.Contains(node))
        {
            linkedNodes.Add(node);
        }
        if (!node.linkedNodes.Contains(this))
        {
            node.linkedNodes.Add(this);
        }
    }
    private void OnMouseDown()
    {
        NodeGenerator.OnNodeClick(this);
    }
    private void OnMouseOver()
    {
        NodeGenerator.OnNodeOver(this);
    }
}
public enum NodeType
{
    Start,
    Fight,
    Chest,
    Boss,
    End,
}