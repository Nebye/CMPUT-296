using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletCollector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Pellet p = PelletHandler.Instance.GetClosestPellet(transform.position);
        if (p != null)
        {
            float dist = (transform.position - p.transform.position).sqrMagnitude;
            //Extremely slow way to do this, don't do this normally. Just want to avoid collision issues
            if (dist < 0.01f)
            {
                p.Eat();
            }
        }
    }
}
