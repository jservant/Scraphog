using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int gearXP;
    public int dmgLevel;
    public float shotDestroyDelay;
    public GameObject shot;
    public Transform shotSpawn;
    public float autoFireDelay;
    [Space()]
    public float clampMinX;
    public float clampMaxX;
    public float clampMinY;
    public float clampMaxY;

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
        if (transform.position.x < clampMinX) transform.position = new Vector2(clampMinX, transform.position.y);
        if (transform.position.x > clampMaxX) transform.position = new Vector2(clampMaxX, transform.position.y);
        if (transform.position.y < clampMinY) transform.position = new Vector2(transform.position.x, clampMinY);
        if (transform.position.y > clampMaxY) transform.position = new Vector2(transform.position.x, clampMaxY);

        if (Input.GetButton("Jump") && canFire)
        {
            Shoot();
            StartCoroutine(ShotCooldown());
        }

        if (gearXP >= 10)
        {
            dmgLevel = 5;
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
