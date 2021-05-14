using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemHolderScript : MonoBehaviour
{
    public int gemType;
    public bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gem" + gemType.ToString())
        {
            isActivated = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem" + gemType.ToString())
        {
            isActivated = false;
        }
    }
}
