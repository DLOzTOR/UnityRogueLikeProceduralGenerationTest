using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
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
    void Start()
    {
        CheckNodesInstances();
        SpawnNodes();
        GenerateNet();
    }
    void CheckNodesInstances()
    {
        GetNodeInstance(NodeType.Start).ToString();
        GetNodeInstance(NodeType.Fight).ToString();
        GetNodeInstance(NodeType.Chest).ToString();
        GetNodeInstance(NodeType.Boss).ToString();
        GetNodeInstance(NodeType.End).ToString();
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
        float shift = fightNodes.Length/2;
        for(int i = 0; i < fightNodes.Length; i++)
        {
            var node = fightNodes[i];
            node.transform.position = new Vector3(2, (i - shift) * 2);
            var nodeLink = Instantiate(nodeLinkInstance, startNode.transform, startNode);
            nodeLink.FirstNode = startNode;
            nodeLink.SecondNode = node;
            AddLink(nodeLink);
        }
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
