using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //-------------------------------
    public Vector2 direction;
    public float thrust;
    public float shootingForce;
    float rotationZ;

    public float health = 100.0f;
    public float stamina = 100.0f;
    public float maxstamina;
    public float staminainc;
    public float staminadec;
    public float staminaregtime;

    public Queue<GameObject> queue;

    private bool mousehold  = false;
    private float mousetimer = 0.0f;

    private bool nottired = true;
    private float tiredtimer = 0.0f;

    //===============================
    public GameObject bulletPrefab;

    public Rigidbody2D rb2d;



    //public float timer = 0f;
    //public float velocity = 0f;
    //public float acceler = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 100;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        queue = new Queue<GameObject>();


    }

    // Update is called once per frame
    void Update()
    {

        bool isRunning = Input.GetKey("left shift") && ((Input.GetKey("w")) || (Input.GetKey("a")) || (Input.GetKey("s")) || (Input.GetKey("d")));

        if (isRunning & nottired)
        {
            stamina = Mathf.Clamp(stamina - (staminadec * Time.deltaTime), 0.0f, maxstamina);
            thrust = 80f;

            if (stamina <= 1)
            {
                nottired = false;
                tiredtimer = 0.0f;
                tiredtimer += Time.deltaTime;
            }

        }

        else if (tiredtimer >= staminaregtime)
        {
            nottired = true;
            thrust = 60f;
            stamina = Mathf.Clamp(stamina + (staminainc * Time.deltaTime), 0.0f, maxstamina);
        }

        else
        {
            tiredtimer += Time.deltaTime;
            thrust = 30f;
        }


        if (Input.GetKey("w"))
        {
            rb2d.AddForce(new Vector2(0, 1) * thrust);
            //rb2d.velocity = new Vector2(0, 1) * thrust;
        }

        if (Input.GetKey("a"))
        {
            rb2d.AddForce(new Vector2(-1, 0) * thrust);
            //rb2d.velocity = new Vector2(-1, 0) * thrust;
        }
        if (Input.GetKey("s"))
        {
            rb2d.AddForce(new Vector2(0, -1) * thrust);
            //rb2d.velocity = new Vector2(0, -1) * thrust;

        }
        if (Input.GetKey("d"))
        {
            rb2d.AddForce(new Vector2(1, 0) * thrust);
            //rb2d.velocity = new Vector2(1, 0) * thrust;
        }


        if (health <= 0)
        { 
            Application.LoadLevel(Application.loadedLevel);
        }


        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (Vector2)((worldMousePos - transform.position));
        rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        direction.Normalize();


        if (Input.GetMouseButton(0))
        {
            mousehold = true;
            mousetimer += Time.deltaTime;
        }

        else
        {


            if (mousehold)
            {
                GameObject bullet = (GameObject)Instantiate(
                                            bulletPrefab,
                                            transform.position + (Vector3)(direction * 1.3f),
                                            Quaternion.identity);

                bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                // Adds velocity to the bullet
                bullet.GetComponent<Rigidbody2D>().velocity = direction * shootingForce * Mathf.Min(mousetimer, 2f) * 1.01f;
                queue.Enqueue(bullet);
                print(queue.Count);
            }

            mousehold = false;
            mousetimer = 0.0f;

            if (queue.Count == 5)
            {
                GameObject oldBullet = queue.Dequeue();
                Destroy(oldBullet);
            }

        }

    }
    public void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            health -= 20f;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (coll.gameObject.tag == "Object" || coll.gameObject.tag == "Gem1" || coll.gameObject.tag == "Gem2" || coll.gameObject.tag == "Gem3" || coll.gameObject.tag == "Gem4")
            {
                bulletPrefab = coll.gameObject;
            }
        }

        if (coll.gameObject.tag == "Enemy1")
        {
            health -= 0.4f;
        }

        if (coll.gameObject.tag == "Enemy2")
        {
            health -= 0.9f;
        }

        if (coll.gameObject.tag == "Enemy3")
        {
            health -= 0.5f;
        }

    }


}
