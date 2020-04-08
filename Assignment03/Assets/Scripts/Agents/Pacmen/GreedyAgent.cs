﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyAgent : MonoBehaviour
{
    private Vector3 target;
    private bool movingTowardTarget;
    public bool MovingTowardTarget { get { return movingTowardTarget; } }

    private float speed = AgentConstants.SPEED;

    //Path
    private Vector3[] path;
    private int pathIndex;

    public void SetTarget(Vector3 _target)
    {
        target = _target;
        if (Mathf.Abs(transform.position.x - target.x) < AgentConstants.EPSILON)//Special case moving vertically
        {
            Vector3 eulerAngles = transform.eulerAngles;
            if (transform.position.y < target.y)
            {
                eulerAngles.x = -90;
            }
            else
            {
                eulerAngles.x = 90;
            }
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            transform.LookAt(target, Vector3.up);
        }
        movingTowardTarget = true;
    }

    void Start()
    {
        Vector3 currPos = transform.position;
        currPos += new Vector3(0.1f, 0, 0);
        SetTarget(currPos);
    }

    // Update is called once per frame
    void Update()
    {

        Pellet p = PelletHandler.Instance.GetClosestPellet(transform.position);
        if (p != null)
        {
            Vector3 target = p.transform.position;

            //CalculatePath	
            GraphNode closestStart = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position);
            GraphNode closestGoal = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(target);
            path = HW3NavigationHandler.Instance.PathFinder.CalculatePath(closestStart, closestGoal);

            if (path == null || path.Length<=1)
            {
                SetTarget(target);
            }
            else
            {
                pathIndex = 0;
                SetTarget(path[pathIndex]);
            }

        }
        else
        {
            movingTowardTarget = false;
        }

        if (movingTowardTarget)
        {
            if ((target - transform.position).sqrMagnitude < AgentConstants.THRESHOLD)
            {
                movingTowardTarget = false;
                transform.position = target;
            }
            else
            {
                Vector3 potentialNewPosition = transform.position + (target - transform.position).normalized * Time.deltaTime * speed;
                if (ObstacleHandler.Instance.AnyIntersect(new Vector2(transform.position.x, transform.position.y), new Vector2(potentialNewPosition.x, potentialNewPosition.y)))
                {
                    movingTowardTarget = false;
                }
                else
                {
                    transform.position = potentialNewPosition;
                }
            }

        }
    }
}
