using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyImprovedPathFinder : GreedyPathFinder
{
    public override Vector3[] CalculatePath(GraphNode startNode, GraphNode goalNode)
    {
        GreedyNode start = new GreedyNode(null, startNode, Heuristic(startNode, goalNode));
        
        PriorityQueue<GreedyNode> openSet = new PriorityQueue<GreedyNode>();
        List<GreedyNode> visitedNodes = new List<GreedyNode>();

        openSet.Enqueue(start);
        int attempts = 0;
        while (openSet.Count() > 0 && attempts<100000)
        {
            attempts+=1;
            GreedyNode currNode = openSet.Dequeue();

            if (currNode.Location == goalNode.Location)
            {
                return ReconstructPath(start, currNode);
            }

            foreach (GraphNode neighbor in currNode.GraphNode.Neighbors)
            {
                GreedyNode greedyNodeNeighbor = new GreedyNode(currNode, neighbor, Heuristic(neighbor, goalNode));
                if (!visitedNodes.Contains(greedyNodeNeighbor))
                {
                    openSet.Enqueue(greedyNodeNeighbor);
                    visitedNodes.Add(greedyNodeNeighbor);
                }
            }


        }
        return null;
    }

    public float Heuristic(GraphNode currNode, GraphNode goalNode)
    {
        return Mathf.Abs(currNode.Location.x-goalNode.Location.x)+Mathf.Abs(currNode.Location.y - goalNode.Location.y);
    }

    private Vector3[] ReconstructPath(GreedyNode startNode, GreedyNode currNode)
    {
        List<Vector3> backwardsPath = new List<Vector3>();

        while (currNode != startNode)
        {
            backwardsPath.Add(currNode.Location);
            currNode = currNode.Parent;
        }
        backwardsPath.Reverse();

        return backwardsPath.ToArray();
    }
}


class GreedyNode : IComparable<GreedyNode>
{
    private GreedyNode parent;
    public GreedyNode Parent { get { return parent; } }
    private GraphNode graphNode;
    public GraphNode GraphNode { get { return graphNode; } }
    public Vector3 Location { get { return graphNode.Location; } }
    private float hScore;
    public float HScore { get { return hScore; } }

    public GreedyNode(GreedyNode _parent, GraphNode _graphNode, float _hScore)
    {
        this.parent = _parent;
        this.graphNode = _graphNode;
        this.hScore = _hScore;
    }

    public int CompareTo(GreedyNode other)
    {
        if (other == null) return 1;

        GreedyNode otherNode = other as GreedyNode;
        if (otherNode != null)
            return this.HScore.CompareTo(otherNode.HScore);
        else
            throw new ArgumentException("Object is not an AStarNode");
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        GreedyNode otherNode = obj as GreedyNode;
        if (otherNode.Location == Location && otherNode.hScore.Equals(hScore))
            return true;
        else
            return false;
    }

    public override int GetHashCode()
    {
        return Location.GetHashCode() + HScore.GetHashCode();
    }
}
