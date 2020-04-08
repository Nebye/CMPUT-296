using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAnimator : MonoBehaviour
{
    private Vector3 prevLocation;
    public Sprite open, close;
    public SpriteRenderer myRenderer;
    private float EPSILON = 0.0001f;

    private float stateTimer;
    private float stateMax = 0.1f;
    private bool openState = true;

    // Update is called once per frame
    void Update()
    {
        if (prevLocation != transform.position)
        {
            stateTimer += Time.deltaTime;
            if (stateTimer >= stateMax)
            {
                openState = !openState;
                stateTimer = 0f;

                if (openState)
                {
                    myRenderer.sprite = open;
                }
                else
                {
                    myRenderer.sprite = close;
                }
            }
        }

        prevLocation = transform.position;
    }
}
