using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    public string tagToHit;
    public GameObject playerExplosion;
    public GameObject enemyExplosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagToHit))
        {
            if (tagToHit == "Enemy")
            {
                other.GetComponent<Enemy>().health -= 1;
                //1 is there for debugging purposes, when the gear mechanic is functional change one to this line:
                //other.GetComponent<PlayerController>().dmgLevel
            }
        }
    }
}
