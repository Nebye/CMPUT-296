using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance;

    public GameObject[] ghosts;//The ghosts in the order that they will appear
    private int ghostIndex = 0;

    private List<FSMAgent> ghostsInPlay;
    public FSMAgent[] GhostsInPlay { get { return ghostsInPlay.ToArray(); } }

    private float ghostSpawner;
    public float ghostSpawnerMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ghostsInPlay = new List<FSMAgent>();
        ghostSpawner = ghostSpawnerMax/3f;
    }

    void Update()
    {
        if (ghostSpawner > 0)
        {
            ghostSpawner -= Time.deltaTime;
            if (ghostSpawner <= 0)
            {
                if (ghostIndex < ghosts.Length)
                {
                    InstantiateNextGhost();
                }
            }
        }
    }

    private void InstantiateNextGhost()
    {
        ghostSpawner = ghostSpawnerMax;
        GameObject ghost1 = GameObject.Instantiate(ghosts[ghostIndex]);
        ghostIndex += 1;

        ghostsInPlay.Add(ghost1.GetComponent<FSMAgent>());
    }

    public FSMAgent GetClosestGhost(Vector3 position)
    {
        float minDist = 1000;
        FSMAgent closest = null;
        foreach (FSMAgent ghost in ghostsInPlay)
        {
            float dist = (ghost.GetPosition() - position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = ghost;
            }
        }
        return closest;
    }
}
