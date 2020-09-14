using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackround : MonoBehaviour
{
    private GameObject mainCamera;
    private Transform cameraPos;

    float startPos;

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        cameraPos = mainCamera.GetComponent<Transform>();

        startPos = transform.position.y;        
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos - 11f > cameraPos.position.y)
        {
            transform.position = new Vector3(0, startPos - 30f, transform.position.z);
            startPos = transform.position.y;            
        }
    }
}

