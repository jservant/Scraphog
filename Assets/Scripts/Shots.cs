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
            if (tagToHit == "Enemy") // Player shooting Enemy
            {
                other.GetComponent<Enemy>().health -= GameObject.FindWithTag("Player").GetComponent<PlayerController>().dmgLevel;
                if (other.GetComponent<Enemy>().health <= 0)
                {
                    int points = other.GetComponent<Enemy>().points;
                    GameObject.FindWithTag("GameController").GetComponent<GameController>().AddToScore(points);
                    Instantiate(enemyExplosion, other.transform.position, other.transform.rotation);
                }
            }
            else if (tagToHit == "Player" && gameObject.CompareTag("Pickup")) // Gear touching Player
            {
                other.GetComponent<PlayerController>().gearXP++;
                int points = 5;
                GameObject.FindWithTag("GameController").GetComponent<GameController>().AddToScore(points);
            }
            else if (tagToHit == "Player" && other.GetComponent<PlayerController>().canBeHit == true) // Enemy shooting Player when not invincible
            {
                other.GetComponent<PlayerController>().LoseALevel();
                if (other.GetComponent<PlayerController>().powerLevel <= 0)
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                }
            }
            Destroy(gameObject);
        }
    }
}
