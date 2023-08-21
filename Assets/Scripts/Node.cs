using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public NodeType type;
    public List<Node> LinkedNodes = new List<Node>();
}
public enum NodeType
{
    Start, 
    Fight,
    Chest,
    Boss,
    End,
}