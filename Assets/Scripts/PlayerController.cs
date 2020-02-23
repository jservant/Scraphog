using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int dmgLevel;
    public float shotDestroyDelay;
    public GameObject shot;
    public Transform shotSpawn;
    public float autoFireDelay;

    bool canFire = true;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveH, moveV);
        rb.velocity = move * speed;

        if (Input.GetButton("Jump") && canFire)
        {
            Shoot();
            StartCoroutine(ShotCooldown());
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Destroy(projectile, shotDestroyDelay);
    }

    IEnumerator ShotCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(autoFireDelay);
        canFire = true;
    }
}
