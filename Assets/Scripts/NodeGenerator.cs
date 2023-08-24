using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public static Action<Node> OnNodeClick;
    public static Action<Node> OnNodeOver;
    [SerializeField]
    Node[] nodeTypesGameObjects;
    List<Node> nodes = new List<Node>();
    List<NodeLink> nodeLinks = new List<NodeLink>();
    [SerializeField]
    int FightCount;
    [SerializeField]
    int ChestCount;
    [SerializeField]
    NodeLink nodeLinkInstance;
    [SerializeField]
    float nodeDisatance = 2;
    [SerializeField]
    Color lineColor = Color.white;    
    [SerializeField]
    Color activeLineColor = Color.magenta;
    List<NodeLink> activeLinkList = new List<NodeLink>();
    Node selectedNode;
    void Start()
    {
        CheckNodesInstances();
        SpawnNodes();
        GenerateNet();
        selectedNode = nodes.First(a => a.type == NodeType.Start);
    }
    private void OnEnable()
    {
        OnNodeClick += NodeClick;
        OnNodeOver += NodeOver;
    }
    private void OnDisable()
    {
        OnNodeClick -= NodeClick;
        OnNodeOver -= NodeOver;
    }
    void NodeClick(Node node)
    {
        selectedNode = node;
    }
    void NodeOver(Node node)
    {
        if (Pathfind.GetPath(selectedNode, node, out var t))
        {
            SetActivePath(t);
        }
        else
        {
            ClearActivePath();
        }
    }
    void SetActivePath(List<Node> path)
    {
        foreach (var link in activeLinkList) 
        {
            link.Color = lineColor;
        }
        var tLinkList = new List<NodeLink>();
        for(int i = 0; i < path.Count - 1; i++)
        {
            tLinkList.Add(FindLink(path[i], path[i + 1]));
        }
        activeLinkList = tLinkList;
        foreach (var link in activeLinkList)
        {
            link.Color = activeLineColor;
        }
    }
    void ClearActivePath()
    {
        foreach (var link in activeLinkList)
        {
            link.Color = lineColor;
        }
        activeLinkList.Clear();
    }
    NodeLink FindLink(Node n1, Node n2)
    {
        return nodeLinks.FirstOrDefault(a => a.Equals(n1, n2));
    }
    void SpawnNodes()
    {
        AddNode(NodeType.Start);
        for (int i = 0; i < FightCount; i++)
        {
            AddNode(NodeType.Fight);
        }
        for (int i = 0; i < ChestCount; i++)
        {
            AddNode(NodeType.Chest);
        }
        AddNode(NodeType.Boss);
        AddNode(NodeType.End);
    }
    void GenerateNet()
    {
        var startNode = nodes.First(a => a.type == NodeType.Start);
        var fightNodes = nodes.Where(a => a.type == NodeType.Fight).ToArray();
        var chestNodes = nodes.Where(a => a.type == NodeType.Chest).ToArray();
        var bossNode = nodes.First(a => a.type == NodeType.Boss);
        var endNode = nodes.First(b => b.type == NodeType.End);
        float shift = (chestNodes.Length) / 2;
        if (chestNodes.Length % 2 == 0)
        {
            shift -= 0.5f;
        }
        for (int i = 0; i < chestNodes.Length; i++)
        {
            var node = chestNodes[i];
            node.transform.position = new Vector3(2 * nodeDisatance, (i - shift) * nodeDisatance);
        }
        shift = (fightNodes.Length) / 2;
        if (fightNodes.Length % 2 == 0)
        {
            shift -= 0.5f;
        }
        for (int i = 0; i < fightNodes.Length; i++)
        {
            var node = fightNodes[i];
            node.transform.position = new Vector3(nodeDisatance, (i - shift) * nodeDisatance);
            AddLink(NodeLink.Create(startNode, node, nodeLinkInstance));
            for (int j = 0; j < chestNodes.Length; j++)
            {
                var chestNode = chestNodes[j];
                AddLink(NodeLink.Create(node, chestNode, nodeLinkInstance));
            }
        }
        bossNode.transform.position = new Vector3(3 * nodeDisatance, 0);
        for (int i = 0; i < chestNodes.Length; i++)
        {
            var node = chestNodes[i];
            AddLink(NodeLink.Create(node, bossNode, nodeLinkInstance));
        }
        endNode.transform.position = new Vector3(4 * nodeDisatance, 0);
        AddLink(NodeLink.Create(bossNode, endNode, nodeLinkInstance));
    }
    bool IsLinkExist(NodeLink link)
    {
        return nodeLinks.Any(a => a.Equals(link));
    }
    void AddNode(NodeType type)
    {
        var t = Instantiate(GetNodeInstance(type));
        t.gameObject.SetActive(true);
        nodes.Add(t);
    }
    void CheckNodesInstances()
    {
        GetNodeInstance(NodeType.Start).ToString();
        GetNodeInstance(NodeType.Fight).ToString();
        GetNodeInstance(NodeType.Chest).ToString();
        GetNodeInstance(NodeType.Boss).ToString();
        GetNodeInstance(NodeType.End).ToString();
    }
    void PrintNodeLinkes()
    {
        foreach (var node in nodes)
        {
            Debug.Log($"Node: {node} linked nodes: " + string.Join(", ", node.linkedNodes.Select(x => x.ToString()).ToArray()).Trim() + ".");
        }
    }
    void AddLink(NodeLink link)
    {
        if (!IsLinkExist(link))
        {
            nodeLinks.Add(link);
            link.CreateLine();
        }
    }
    void CheckLinkWork()
    {
        //var l1 = new NodeLink(nodes[0], nodes[1]);
        //var l2 = new NodeLink(nodes[0], nodes[1]);
        //var l3 = new NodeLink(nodes[1], nodes[0]);
        //var l4 = new NodeLink(nodes[0], nodes[2]);
        //var l5 = new NodeLink(nodes[1], nodes[2]);
        //nodeLinks.AddRange(new List<NodeLink> { l1, l2, l3});
        //Debug.Log("l1 l2 " + l1.Equals(l2) + " true");
        //Debug.Log("l1 l3 " + l1.Equals(l3) + " true");
        //Debug.Log("l1 l4 " + l1.Equals(l4) + " true");
        //Debug.Log("l1 l5 " + l1.Equals(l5) + " false");
        //Debug.Log("l3 l4 " + l1.Equals(l4) + " false");
        //Debug.Log("l3 l5 " + l1.Equals(l5) + " false");
        //Debug.Log("l1 exist in list" + IsLinkExist(l1) + " true");
        //Debug.Log("l2 exist in list" + IsLinkExist(l2) + " true");
        //Debug.Log("l3 exist in list" + IsLinkExist(l3) + " true");
        //Debug.Log("l4 exist in list" + IsLinkExist(l4) + " false");
        //Debug.Log("l5 exist in list" + IsLinkExist(l5) + " false");
    }
    Node GetNodeInstance(NodeType type)
    {
        return nodeTypesGameObjects.First(t => t.type == type);
    }
}
