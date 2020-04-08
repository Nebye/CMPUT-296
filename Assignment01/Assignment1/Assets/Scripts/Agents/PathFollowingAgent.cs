using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingAgent : MonoBehaviour
{
	private Vector3 target;
	private bool movingTowardTarget;
	public bool MovingTowardTarget { get { return movingTowardTarget; } }

	private float speed = 2.0f;
	private float threshold = 0.001f;
    public Transform pacManTransform;
    private float EPSILON = 0.00001f;
	public void SetTarget(Vector3 _target)
	{
		target = _target;
        if (Mathf.Abs(transform.position.x - target.x) < EPSILON)//Special case moving vertically
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
        SetTarget(new Vector3(0.1f, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

		if (movingTowardTarget)
		{
			if ((target - transform.position).sqrMagnitude < threshold)
			{
				movingTowardTarget = false;
                transform.position = target;
			}
			else {
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
