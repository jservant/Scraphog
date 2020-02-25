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
                other.GetComponent<Enemy>().health -= GameObject.FindWithTag("Player").GetComponent<PlayerController>().dmgLevel;
                if (other.GetComponent<Enemy>().health <= 0)
                {
                    int points = other.GetComponent<Enemy>().points;
                    GameObject.FindWithTag("GameController").GetComponent<GameController>().AddToScore(points);
                }
            }
            else if (tagToHit == "Player" && gameObject.CompareTag("Pickup"))
            {
                other.GetComponent<PlayerController>().gearXP++;
            }
            else if (tagToHit == "Player" && other.GetComponent<PlayerController>().canBeHit == true)
            {
                other.GetComponent<PlayerController>().LoseALevel();
            }
            Destroy(gameObject);
        }
    }
}
