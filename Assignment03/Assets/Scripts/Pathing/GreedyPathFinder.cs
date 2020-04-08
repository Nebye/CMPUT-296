using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyPathFinder : PathFinder
{
	public override Vector3[] CalculatePath(GraphNode currNode, GraphNode goalNode)
	{
        if(currNode==null || goalNode == null)
        {
            return null;
        }

		List<GraphNode> path = new List<GraphNode>();

		while (currNode != goalNode)
		{
			path.Add(currNode);

			GraphNode bestNeighbor = null;
			float minDist = 1000f;
			foreach (GraphNode neighbor in currNode.Neighbors)
			{
				if (!path.Contains(neighbor)){
					float dist = (neighbor.Location - goalNode.Location).sqrMagnitude;
					if (dist < minDist)
					{
						minDist = dist;
						bestNeighbor = neighbor;
					}
				}
			}

			if (bestNeighbor != null)
			{
                currNode = bestNeighbor;
			}
			else
			{
				break;
			}
		}
		GraphNode[] pathArray = path.ToArray();
        Vector3[] vector3Path = new Vector3[pathArray.Length + 1];

        for (int i = 0; i < pathArray.Length; i++)
        {
            vector3Path[i] = pathArray[i].Location;
        }

        vector3Path[pathArray.Length] = goalNode.Location;

        return vector3Path;
        
	}
}
