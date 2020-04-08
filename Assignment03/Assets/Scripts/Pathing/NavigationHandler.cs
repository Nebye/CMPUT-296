﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationHandler : MonoBehaviour
{
	public ObstacleHandler obstacleHandler;
	public ObstacleDefiner obstacleDefiner;
	public NodeHandler nodeHandler;
	public PathFinder pathFinder;

	public PathFollowingAgent agent;

	//Path
	private Vector3[] path;
	private int pathIndex;

	// Start is called before the first frame update
	void Start()
	{
		obstacleHandler.Init(obstacleDefiner);
		obstacleHandler.CreateAndRenderObstacles(obstacleDefiner.GetObstaclePoints());
        nodeHandler = new GridHandler();
		nodeHandler.CreateNodes();

	}

	void Update()
	{
        nodeHandler.VisualizeNodes();
		if (Input.GetMouseButtonDown(0))
		{
            pathIndex = 0;
            Vector3 target = Camera.main.ScreenToWorldPoint(
				 new Vector3(Input.mousePosition.x,
				 Input.mousePosition.y,
				 10f));

			//CalculatePath	
			GraphNode closestStart = nodeHandler.ClosestNode(agent.transform.position);
			GraphNode closestGoal = nodeHandler.ClosestNode(target);
			path = pathFinder.CalculatePath(closestStart, closestGoal);
            
			if (path == null || path.Length<=pathIndex)
			{
				agent.SetTarget(target);
			}
			else
			{
				pathIndex = 0;
				agent.SetTarget(path[pathIndex]);
				
			}
		}
		else if (path != null)
		{
			if (!agent.MovingTowardTarget)
			{
				if (pathIndex < path.Length - 1)
				{
					pathIndex += 1;
					agent.SetTarget(path[pathIndex]);

					if (pathIndex == path.Length - 1)
					{
						path = null;
					}
				}
			}
		}
	}

}
