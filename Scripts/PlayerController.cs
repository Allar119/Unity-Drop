using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject particle;
    public GameObject tabToStar;
    public GameObject playerJump;
    public Transform cameraPos;
    
    public static bool gameOver;
    public static bool gameStarted;
    
    public float upForce = 1f;
    
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
        gameStarted = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AudioSource>().Play();
            switch (gameStarted)
            {
                case false:
                    tabToStar.SetActive(false);
                    rb2d.isKinematic = false;
                    Instantiate(playerJump, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    rb2d.velocity = new Vector3(transform.position.x, upForce, transform.position.z);
                    gameStarted = true;
                    break;

                case true:
                    Instantiate(playerJump, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    rb2d.velocity = new Vector3(transform.position.x, upForce, transform.position.z);                                   
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("GAME OVER");
        gameOver = true;
        Instantiate (particle, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        gameObject.SetActive(false);
    }

}
