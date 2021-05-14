using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En3Follow: MonoBehaviour
{
    public float speed;
    public float angle;
    public float distance;

    private GameObject player;
    private Rigidbody2D rb;

    public GameObject bulletPrefab;

    public List<GameObject> Sight;
    public bool sawPlayer = false;

    public bool ableShoot  = true;
    public float shootcool = 10f;

    public float maxhealth;
    public float health;
    private float damage;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        distance = Vector3.Distance(player.transform.position, transform.position);
        direction.Normalize();

        if (distance > 10f && sawPlayer || health != maxhealth)
        {
            speed = 0.03f;
            transform.position += direction * speed;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }

        else if (distance <= 10f && sawPlayer || health != maxhealth)
        {
            speed = 0.01f;
            transform.position -= direction * speed;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }


        if (health < 0)
        {
            Destroy(gameObject);
        }

        sawPlayer = Sight[0].GetComponent<Sight>().PonSight;

        

        if (ableShoot && distance < 20f)
        {
            GameObject bullet = (GameObject)Instantiate(
                                            bulletPrefab,
                                            transform.position + (Vector3)(direction * 2.3f),
                                            Quaternion.identity);

            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rb.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 20f ;

            ableShoot = false;
        }
        else
        {
            if (shootcool > 5f)
            {
                shootcool = 0f;
                ableShoot = true;

            }

            shootcool += Time.deltaTime;

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Object" && collision.rigidbody.velocity.magnitude > 15)
        {
            damage = collision.rigidbody.velocity.magnitude / 2.5f * collision.rigidbody.mass;

            health -= damage;

            gameObject.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color - new Color(damage / 50, 0f, 0f, 0f);

        }

    }
}
