using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorScript : MonoBehaviour
{

    public Color color;
    public Color startingColor;
    // Start is called before the first frame update
    void Start()
    {
        startingColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().color = startingColor;
        }
    }
}
