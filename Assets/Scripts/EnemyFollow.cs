using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour{

    public float speed = 0.01f;
    public float angle;

    private GameObject player;
    private Rigidbody2D rb;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.01f;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        Debug.Log(direction);
        direction.Normalize();
        transform.position += direction * speed;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
         

        

    }
}
