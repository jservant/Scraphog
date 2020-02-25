using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float shotDestroyDelay;
    public float autoFireDelay;
    public float immunityLength;
    [Space()]
    public int powerLevel;
    public int gearXP;
    public int dmgLevel;
    [Space()]
    public GameObject shot;
    public Transform shotSpawn;
    public Text levelText;
    public GameObject gameOverPanel;
    [Space()]
    public float clampMinX;
    public float clampMaxX;
    public float clampMinY;
    public float clampMaxY;
    [Space()]
    bool canFire = true;
    public bool canBeHit = true;

    SpriteRenderer sr;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        levelText.text = "P: " + powerLevel;
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

        if (powerLevel == 3)
        {
            dmgLevel = 15;
        }
        else if (powerLevel == 2)
        {
            dmgLevel = 10;
        }
        else if (powerLevel == 1)
        {
            dmgLevel = 5;
        }
        
        //Level Up!
        if (gearXP >= 5)
        {
            powerLevel++;
            levelText.text = "P: " + powerLevel;
            gearXP = 0;
            if (powerLevel >= 4)
            {
                powerLevel = 3;
                levelText.text = "P: " + powerLevel;
            }
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

    public void LoseALevel()
    {
        powerLevel--;
        gearXP = 0;
        levelText.text = "";
        if (powerLevel <= 0)
        {
            levelText.text = "P: 0";
            Destroy(gameObject);
            gameOverPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(ImmunityCooldown());
            levelText.text += "P: " + powerLevel;
        }
    }

    IEnumerator ImmunityCooldown()
    {
        canBeHit = false;
        sr.color = Color.gray;
        yield return new WaitForSeconds(immunityLength);
        canBeHit = true;
        sr.color = Color.white;
    }
}
