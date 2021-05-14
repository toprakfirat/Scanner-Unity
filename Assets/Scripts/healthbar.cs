using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour


{
    // Start is called before the first frame update


    private PlayerMovementScript playerScript;
    private float phealth;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovementScript>();
    
    }

    // Update is called once per frame
    private void Update()
    {
        phealth = playerScript.health;

        gameObject.transform.localScale = new Vector3(phealth / 100f, 1, 1);


    }
}
