using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
        
    public GameObject[] obstacle;
    public GameObject particleSys;
    
    private GameObject mainCamera;

    [HideInInspector] public float distance;            //Distance between moveing obstacles, value passed in from "Game Controller" at instantiate
    [HideInInspector] public float speed;               //Obstacle movment speed, value passed in from "Game Controller" at instantiate;
    [HideInInspector] public float obstacleStartPos;    //Start position, value passed in from "Game Controller" at instantiate
    [HideInInspector] public float moveDirection;       //Movment direction, left or right, value passed in from "Game Controller" at instantiate
    [HideInInspector] public int randObstacle;

    private float spawnRate;                            //Time in which new obstacle will be spawned
    
    private float timeSinceLastSpawn;                   //Time that has passed since last obstacle spawn
       
    private BoxCollider2D box;
    private float boxLenght;

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

        box = obstacle[randObstacle].GetComponent<BoxCollider2D>();
        boxLenght = box.size.x;        
        spawnRate = (distance + boxLenght) / speed;
        
        //Debug.Log("Distance: " + distance);
        //Debug.Log("Speed: " + speed);
        //Debug.Log("Obstacle: " + randObstacle);

        SpanwnObstacleAtStart();        
    }

    // Update is called once per frame
    void Update ()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnRate)
        {
            timeSinceLastSpawn = 0f;
            SpanwnObstacle((obstacleStartPos + boxLenght) * moveDirection, randObstacle);
        }
    }


    void SpanwnObstacleAtStart()
    {
        float levelWith = obstacleStartPos + 1;
        if (moveDirection < 0)
        {
            for (float spownPos = (obstacleStartPos + boxLenght) * moveDirection; -levelWith * moveDirection > spownPos; spownPos = spownPos - (distance + boxLenght) * moveDirection)
            {
                SpanwnObstacle(spownPos, randObstacle);
            }
        }

        else
        {
            for (float spownPos = (obstacleStartPos + boxLenght) * moveDirection; -levelWith * moveDirection < spownPos; spownPos = spownPos - (distance + boxLenght) * moveDirection)
            {
                SpanwnObstacle(spownPos, randObstacle);
            }
        }
    }

    void SpanwnObstacle(float posX, int obstacleIndex)
    {
        //Spawns an obstacle to game scene        
        GameObject _obstacle = Instantiate(obstacle[obstacleIndex], new Vector3(posX, transform.position.y, transform.position.z), transform.rotation);

        //Passing values to SpawnController script:
        var newObsticle = _obstacle.GetComponent<ObstacleController>();
        newObsticle.speed = -speed * moveDirection;

        //Moves a obstacle in hierarchy under this gameobject
        _obstacle.transform.parent = gameObject.transform;        
    }

    float RandomDirection()
    {
        int randomNr = Random.Range(1, 3);
        if (randomNr == 1)
        {
            return -1f;
        }

        else
        {
            return 1f;
        }
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {      
            GameController.instance.ScoreUpdate();
            Vector3 particleSpownPos;

            mainCamera.GetComponent<AudioSource>().Play();            

            int childs = transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                particleSpownPos = transform.GetChild(i).position;

                // Instantiateing particle system, passing the lenght of the particle system and lenght of the obstacles: 
                GameObject partSyst = Instantiate(particleSys, particleSpownPos, transform.rotation);
                var new_partSyst = partSyst.GetComponent<ParticalController>();
                new_partSyst.LenghtOfSystem = boxLenght;
                new_partSyst.obsticalLeght = randObstacle;

                Destroy(transform.GetChild(i).gameObject);
            }

            Destroy(gameObject);            
        }       
    }    
}

