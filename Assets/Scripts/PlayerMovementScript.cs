using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Vector2 direction;
    float rotationZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        float speed = 10f;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;

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
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;
        }
    }

    

}
