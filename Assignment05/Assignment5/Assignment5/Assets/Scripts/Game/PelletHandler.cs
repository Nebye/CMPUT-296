using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletHandler : MonoBehaviour
{
    public static PelletHandler Instance;

    public GameObject protoPellet, protoPowerPellet;

    private Dictionary<string, Pellet> locationToPellets;

    private bool justEatenPowerPellet;
    public bool JustEatenPowerPellet { get { return justEatenPowerPellet; } }
    private float justEatenPowerPelletTimer;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (justEatenPowerPelletTimer > 0)
        {
            justEatenPowerPelletTimer -= Time.deltaTime;
            if (justEatenPowerPelletTimer <= 0)
            {
                justEatenPowerPellet = false;
            }
        }
        
    }

    public void AddPellet(Vector3 location, bool powerPellet = false)
    {
        if (locationToPellets == null)
        {
            locationToPellets = new Dictionary<string, Pellet>();
        }

        if (!locationToPellets.ContainsKey(location.ToString()))
        {
            GameObject newPelletObj = null;

            if (powerPellet)
            {
                newPelletObj = GameObject.Instantiate(protoPowerPellet);
            }
            else {
                newPelletObj = GameObject.Instantiate(protoPellet);
            }

            newPelletObj.transform.position = location;
            newPelletObj.transform.parent = transform;
            locationToPellets.Add(location.ToString(), newPelletObj.GetComponent<Pellet>());
        }
    }

    public void RemovePellet(Vector3 location)
    {
        if (locationToPellets.ContainsKey(location.ToString()))
        {
            if (locationToPellets[location.ToString()].powerPellet)
            {
                justEatenPowerPellet = true;
                justEatenPowerPelletTimer = 0.1f;
            }

            locationToPellets.Remove(location.ToString());
        }
    }

    public Pellet GetClosestPellet(Vector3 location)
    {
        float minDist = 1000;
        Pellet closest = null;
        foreach (KeyValuePair<string, Pellet> kvp in locationToPellets)
        {
            float dist = (kvp.Value.transform.position - location).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = kvp.Value;
            }
        }
        return closest;
    }
}
