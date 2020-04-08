using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public bool powerPellet = false;

    public void Eat() {
        PelletHandler.Instance.RemovePellet(transform.position);
        ScoreHandler.Instance.UpdateScore();
        Destroy(gameObject);
    }

}
