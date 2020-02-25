using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject shot;
    public GameObject gear;
    public Transform shotSpawn;
    public float minShotDelay;
    public float maxShotDelay;
    public int points;
    public int health;
    [Space()]
    [SerializeField] float moveSpeed; // Vertical movement speed
    [SerializeField] float frequency; // How often object goes left and right
    [SerializeField] float magnitude; // The highest/lowest points the object will go in the wave
    Vector3 pos;
    
    void Start()
    {
        pos = transform.position;
        StartCoroutine(Shoot());
    }

    void Update()
    {
        pos -= transform.up * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.right * Mathf.Sin(Time.time * frequency) * magnitude;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(minShotDelay, maxShotDelay));
        while (true)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            yield return new WaitForSeconds(Random.Range(minShotDelay, maxShotDelay));
        }
    }

    private void OnDestroy()
    {
        Instantiate(gear, shotSpawn.position, shotSpawn.rotation);
    }
}
