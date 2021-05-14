using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour{

    public float speed;
    public float angle;
    public float distance;

    private GameObject player;
    private Rigidbody2D rb;

    public List<GameObject> Sight;

    public bool sawPlayer = false;


    public float maxhealth;
    public float health;
    private float damage;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
   
        if (gameObject.tag == "Enemy2")
        {
            maxhealth = 45f;
            speed = 0.1f;
        }

        else if (gameObject.tag == "Enemy1")
        {
            maxhealth = 15f;
            speed = 0.20f;
        }

        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        distance = Vector3.Distance(player.transform.position, transform.position);

        direction.Normalize();

        sawPlayer = Sight[0].GetComponent<Sight>().PonSight;


        if (distance < 30f && sawPlayer && distance > 1f || health != maxhealth)
        {
            transform.position += direction * speed;

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rb.rotation = angle;

            Vector3 forward = transform.forward;

         
        }

        if (health < 0)
        {
            Destroy(gameObject);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Object" && collision.rigidbody.velocity.magnitude > 15)
        {
            damage = collision.rigidbody.velocity.magnitude / 2 * collision.rigidbody.mass;

            health -= damage;

            gameObject.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color - new Color(damage/maxhealth, 0f, 0f, 0f);

        }
     
    }
}
