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

    public float stamina = 100.0f;
    public float maxstamina;
    public float staminainc;
    public float staminadec;
    public float staminaregtime;

    public Queue<GameObject> queue;

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
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        queue = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 pos = transform.position;
        float maxspeed = 20f;

        if (Input.GetKey("w"))
        {
            pos.y += velocity * Time.deltaTime * 1.2f;
        }

        if (Input.GetKey("s"))
        {
            pos.y -= velocity * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += velocity * Time.deltaTime;

        }
        if (Input.GetKey("a"))
        {
            pos.x -= velocity * Time.del5rtaTime;
        }

        //EGER HIC BISIYE BASMIYORSA TIMER'I SIFIRLA
        //YAVAS YAVAS VELOCITY 0'A DUSSUN DIYE HER INSTANCE'TA CIKAR, 0'A GELINCE DUR

        if (!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
        {
            timer = 0f;
            velocity = Mathf.Max(0f, velocity - 0.5f);
        }

        //TIMER BASIS SURESINI ALGILIYOR HER INSTANCE'TA
        //EGER BI TUSA BASILIRSA VELOCITY'YE ACCELERATION EKLE MAXSPEEDE ULASANA KADAR

        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            timer += Time.deltaTime;
            velocity += timer * 10 * acceler;
            velocity = Mathf.Min(velocity, maxspeed);
            

        }

        transform.position = pos;*/

        //Debug.Log(Time.deltaTime);

        bool isRunning = Input.GetKey("left shift") && ((Input.GetKey("w")) || (Input.GetKey("a")) || (Input.GetKey("s")) || (Input.GetKey("d")));

        if (isRunning & nottired)
        {
            stamina = Mathf.Clamp(stamina - (staminadec * Time.deltaTime), 0.0f, maxstamina);
            thrust = 50;

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
            thrust   = 25;
            stamina  = Mathf.Clamp(stamina + (staminainc * Time.deltaTime), 0.0f, maxstamina);
        }

        else
        {
            tiredtimer += Time.deltaTime;
            thrust = 10; 
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


        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (Vector2)((worldMousePos - transform.position));
        rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        direction.Normalize();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = (GameObject)Instantiate(
                                            bulletPrefab,
                                            transform.position + (Vector3)(direction * 1.3f),
                                            Quaternion.identity);

            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            // Adds velocity to the bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * shootingForce;
            queue.Enqueue(bullet);
            print(queue.Count);
            if(queue.Count == 5)
            {
                GameObject oldBullet = queue.Dequeue();
                Destroy(oldBullet);
            }
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            if (collision.gameObject.tag == "Object" || collision.gameObject.tag == "Gem1" || collision.gameObject.tag == "Gem2" || collision.gameObject.tag == "Gem3" || collision.gameObject.tag == "Gem4")
            {
                bulletPrefab = collision.gameObject;
            }
        }
    }


}
