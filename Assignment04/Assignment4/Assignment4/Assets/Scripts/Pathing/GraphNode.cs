using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode
{
	private Vector3 location;
	private List<GraphNode> neighbors;

	public Vector3 Location { get { return location; } }
	public GraphNode[] Neighbors { get { return neighbors.ToArray(); } }

	public GraphNode(Vector3 _location)
	{
		location = _location;
		neighbors = new List<GraphNode>();
	}

	public void AddNeighbor(GraphNode neighbor) {
		if (!neighbors.Contains(neighbor))
		{
			neighbors.Add(neighbor);
		}
	}

    public override int GetHashCode()
    {
        return location.ToString().GetHashCode();
    }
}
