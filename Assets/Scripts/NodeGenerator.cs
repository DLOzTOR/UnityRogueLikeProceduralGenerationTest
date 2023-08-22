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
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            node.transform.position = new Vector3(i * 3, 0, 0);
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            if (i < nodes.Count - 1 )
            {
                var link = new NodeLink(node, nodes[i + 1]);
                Debug.Log("2");
                if (!IsLinkExist(link)) {
                    Debug.Log("@");
                    nodeLinks.Add(link);
                    link.CreateLine();
                }
            }
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
