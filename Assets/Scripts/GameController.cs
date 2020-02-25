using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float startDelay;
    public float enemyDelay;
    public float waveDelay;

    public int enemiesInWave;
    public GameObject[] enemyPrefabs;

    public int score;
    public Text scoreText;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);
        while (true) // Spawn a wave
        {
            for (int i = 0; i < enemiesInWave; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-3, 3), 6);
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
                // Possibly randomize enemy
                yield return new WaitForSeconds(enemyDelay);
            }
            yield return new WaitForSeconds(waveDelay);
        }
    }

    public void AddToScore(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score.ToString();
    }
}
