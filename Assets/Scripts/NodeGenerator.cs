using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    [SerializeField]
    Node[] nodeTypesGameObjects;
    List<Node> nodes = new List<Node>();
    [SerializeField]
    int FightCount;
    [SerializeField]
    int ChestCount;
    [SerializeField]
    Material lineMaterial;
    void Start()
    {
        CheckNodesInstances();
        SpawnNodes();
        GenerateNet();
    }
    void Update()
    {
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
            if (i < nodes.Count - 1)
            {
                node.LinkedNodes.Add(nodes[i + 1]);
            }

        }
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            if (node.LinkedNodes.Count != 0)
            {
                var t = node.AddComponent<LineRenderer>();
                t.positionCount = 2;
                t.startWidth = 0.15f;
                t.endWidth = 0.15f;
                t.material = lineMaterial;
                t.sortingOrder = -1;
                t.SetPosition(0, node.transform.position);
                t.SetPosition(1, node.LinkedNodes[0].transform.position);
            }
        }
    }
    void AddNode(NodeType type)
    {
        var t = Instantiate(GetNodeInstance(type));
        t.gameObject.SetActive(true);
        nodes.Add(t);
    }
    Node GetNodeInstance(NodeType type)
    {
        return nodeTypesGameObjects.First(t => t.type == type);
    }
}
