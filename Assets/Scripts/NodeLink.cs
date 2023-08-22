using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLink {
    Node firstNode;
    Node secondNode;

    public NodeLink(Node firstNode, Node secondNode)
    {
        this.firstNode = firstNode;
        this.secondNode = secondNode;
    }

    public Node FirstNode { get => firstNode; }
    public Node SecondNode { get => secondNode; }

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
