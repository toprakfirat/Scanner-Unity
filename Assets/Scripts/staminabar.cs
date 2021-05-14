using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminabar : MonoBehaviour


{
    // Start is called before the first frame update


    private PlayerMovementScript playerScript;
    private float pstamina;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovementScript>();

    }

    // Update is called once per frame
    private void Update()
    {
        pstamina = playerScript.stamina;

        gameObject.transform.localScale = new Vector3(Mathf.Min(pstamina / 100f,1), 1, 1);


    }
}
