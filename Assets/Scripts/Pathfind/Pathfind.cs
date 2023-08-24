using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class Pathfind 
{
    static public bool GetPath(Node startNode, Node endNode, out List<Node> path)
    {
        if(startNode == endNode)
        {
            path = null;
            return false;
        }
        List<List<Node>> pathes = new List<List<Node>>();
        List<List<Node>> completePathes = new List<List<Node>>();
        pathes.Add(new List<Node> {startNode});
        while (pathes.Count != 0)
        {
            var tPathes = new List<List<Node>>();
            foreach (var tPath in pathes)
            {
                if (tPath.Last().linkedNodes.Contains(endNode))
                {
                    completePathes.Add(new List<Node>(tPath) { endNode });
                }
                else
                {
                    foreach (var tNode in tPath.Last().linkedNodes)
                    {
                        if (!tPath.Contains(tNode))
                        {
                            tPathes.Add(new List<Node>(tPath) { tNode });
                        }
                    }
                }
            }
            pathes = tPathes;
        }
        if(completePathes.Count > 0){
            path = completePathes.FirstOrDefault(a => a.Count == completePathes.Min(p => p.Count));
            return true;
        }
        path = null;
        return false;
    } 
}
