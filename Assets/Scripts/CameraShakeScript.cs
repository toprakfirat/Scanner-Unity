using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    public Camera camera;
    float shakeAmount;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    public void Shake(float amount, float duration)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", duration);

    }

    public void BeginShake()
    {
        Vector3 camPos = camera.transform.position;

        float shakeX = Random.value * shakeAmount * 2 - shakeAmount;
        float shakeY = Random.value * shakeAmount * 2 - shakeAmount;

        camPos.x += shakeX;
        camPos.y += shakeY;

        camera.transform.position = camPos;
    }
    public void StopShake()
    {
        CancelInvoke("BeginShake");
        camera.transform.localPosition = Vector3.zero;
    }
}
