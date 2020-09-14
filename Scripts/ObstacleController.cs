using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    [HideInInspector]
    public float speed;         //Obsticle movment speed (Value passed from SpawnController)   

    private float destroyLocation;

    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;

    // Use this for initialization
    void Start()
    {        
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();

        //Applaying velocity to obsticale:
        rb2d.velocity = new Vector2(speed, 0);
        destroyLocation = GameController.unitsFromCenter + box2d.size.x;
    }

    // Update is called once per frame
    void Update()
    {      
        if (speed < 0)
        {            
            if(transform.position.x < -destroyLocation)
            {
                Destroy(gameObject);
            }
        }

        else if (speed > 0)
            if (transform.position.x > destroyLocation)
            {
                Destroy(gameObject);
            }

    }
}
