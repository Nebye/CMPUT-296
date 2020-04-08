using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAgent : MonoBehaviour
{
    private Vector3 target;
    private bool movingTowardTarget = true;
    public bool MovingTowardTarget { get { return movingTowardTarget; } }

    private const int RIGHT = 0, DOWN = 1, LEFT = 2, UP = 3;
    private int currDirection = 0;//0 = right, 1 = down, 2 = left, 3 = up

    private float randomTimer = 0;
    private const float RANDOM_TIMER_MAX = 0.5f;

    private float speed = AgentConstants.SPEED;
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
        currPos += new Vector3(0.001f, 0, 0);
        SetTarget(currPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (randomTimer < RANDOM_TIMER_MAX)
        {
            randomTimer += Time.deltaTime;
        }
        else
        {
            randomTimer = 0;
            currDirection = Random.RandomRange(0, 3);
        }

        

        if (currDirection == RIGHT)
        {
            SetTarget(HW5NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position + Vector3.right * 0.2f).Location);
        }
        else if (currDirection == DOWN)
        {
            SetTarget(HW5NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position + Vector3.down * 0.2f).Location);
        }
        else if (currDirection == LEFT)
        {
            SetTarget(HW5NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position + Vector3.left * 0.2f).Location);
        }
        else if (currDirection == UP)
        {
            SetTarget(HW5NavigationHandler.Instance.NodeHandler.ClosestNode(transform.position + Vector3.up * 0.2f).Location);
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
