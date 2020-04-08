using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationHandler : MonoBehaviour
{
    public PacmanAnimator animator;
    public Sprite north1, north2, east1, east2, west1, west2, south1, south2;

    private Vector3 prevLocation;

    void Update()
    {
        if (transform.position.y - prevLocation.y > AgentConstants.EPSILON)
        {
            animator.open = north1;
            animator.close = north2;
        }
        else if (transform.position.y - prevLocation.y < -1*AgentConstants.EPSILON)
        {
            animator.open = south1;
            animator.close = south2;
        }
        else if(transform.position.x - prevLocation.x > AgentConstants.EPSILON)
        {
            animator.open = east1;
            animator.close = east2;
        }
        else
        {
            animator.open = west1;
            animator.close = west2;
        }

        prevLocation = transform.position;


    }
}
