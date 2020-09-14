using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class GameController : MonoBehaviour {

    public static GameController instance;    

    public GameObject obstacleSpawner;      // Gameobject that spawns obstacles
    public GameObject inGameScore;          // UI that displays score during gameplay
    public GameObject endScene;             // UI "GAME OVER" window
    public GameObject levels;               // Gameobject under which obstacle levels will be placed after spawning

    public Transform player;                // Getting player position reference

    private Animator animator;
    private Animator animGameOver;
    private SpawnController spawnControllerScript;

    public Text[] scoreText;                // in game score text element    
    public Text endScoreText;               // end score text element
    [Space]
    public Text[] highScoreText;            // high score text element
    public Text achievementLevel;           // achievement unlock level text

    private int direction = 1;              // starting direction of obstacles
    [SerializeField] private int score = 0;                  // start score
    private int endScore;                   // finel score after game over
    private int numOfObstacles;             // Nubmer of obstacles in Obstacle spawner array
    private int randomObstacle;             // Random obstacle from obstacle spawner array;

    private float lastSpawnPosY = 3;        // Last spwned obstical level Y position
    private float verticalDistance = 2.5f;  // Vertical distance between obstical levels (Spawners)
        
    private float pixelsPerUnit;            // Pixels per game units in current screen resolution (To be calculated in Awake)
    public static float horizontalUnits;    // Number of horizontal game units visible to camera (edge to edge) (To be calculated in Awake)
    public static float unitsFromCenter;    // Number of horizontal game units visible to camera (center to edge) (To be calculated in Awake) 

    private bool gowShown = false;          // is game over window been shown, (to stop couroutine)
    
    [Space]
    public float speed;
    public float distance;

    [Space]
    public float magnitude = 1f;
    public float roughness = 1f;
    public float fadeInTime = 1f;
    public float fadeOutTime = 1f;

    void Awake()
    {
        //Singelton pattern (restricts the instantiation of a class to one object):
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        // Screen size info in pixels:
        //Debug.Log("Screen Width : " + Screen.width);
        //Debug.Log("Screen height : " + Screen.height);

        // Calculating pixels per game units = Screen height in pixels devided by 2*camera size
        pixelsPerUnit = Screen.height / (5 * 2);            
        //Debug.Log("Pixels per units : " + pixelsPerUnit);

        // Calculating horizontal game units visible to camera (edge to edge):
        horizontalUnits = Screen.width / pixelsPerUnit;        
        //Debug.Log("Horizontal game units : " + horizontalUnits);

        // Calculating horizontal game units visible to camera (center to edge):
        unitsFromCenter = (horizontalUnits / 2);        
    }
    
    // Use this for initialization
    void Start ()
    {
        inGameScore.SetActive(true);
        endScene.SetActive(false);

        animator = inGameScore.GetComponent<Animator>();
        animGameOver = endScene.GetComponent<Animator>();

        spawnControllerScript = obstacleSpawner.GetComponent<SpawnController>();
        numOfObstacles = spawnControllerScript.obstacle.Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (lastSpawnPosY > player.position.y - 6f)
        {
            direction = direction * -1;            
            SpawnObstaclesController(0, NextSpawnPosY(), direction);
        }
        
        if (PlayerController.gameOver == true)
        { 
            // Displaying "game over windows" once:
            if (gowShown == false)
            {
                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);

                endScore = score;
                PlayServices.AddScoreToLeaderboard(DIFGPS.leaderboard_leaderboard, score);

                // Converting text to score:
                endScoreText.text = endScore.ToString();
                highScoreText[0].text = PlayerPrefs.GetInt("Best", 0).ToString();
                highScoreText[1].text = PlayerPrefs.GetInt("Best", 0).ToString();
                
                StartCoroutine(GameOver());
            }                       
        }        
    }

    void SpawnObstaclesController(float posX, float posY, int direction)
    {
        // Geting random obstacle
        if (direction == -1) RandomObstacle();

        //Raising difficulty
        LevelUp();

        // Instantiating new obstacle spawner with ramdom direction at next Y position:
        GameObject spawnController = Instantiate(obstacleSpawner, new Vector3(posX, posY, transform.position.z), transform.rotation);
        
        //Passing values to SpawnController script:
        var newSpawner = spawnController.GetComponent<SpawnController>();
        newSpawner.obstacleStartPos = unitsFromCenter;
        newSpawner.moveDirection = direction;
        newSpawner.speed = speed;
        newSpawner.distance = distance;
        newSpawner.randObstacle = randomObstacle;

        //Placeing spowner game objects under "levels" game object:
        newSpawner.transform.parent = levels.transform;

    }    

    float NextSpawnPosY()
    {
        return lastSpawnPosY = lastSpawnPosY - verticalDistance;
    }

    public void ScoreUpdate()
    {
        score++;
        animator.Play("InGameScore");
        scoreText[0].text = score.ToString();
        scoreText[1].text = score.ToString();
    }

    void LevelUp()
    {
        speed = speed + 0.05f;
        distance = distance + 0.01f;
    }

    void RandomObstacle()
    {
        int oldRandomObstcale = randomObstacle;
        int randomNr;

        do
        {
            randomNr = Random.Range(0, numOfObstacles);
        }
        while (oldRandomObstcale == randomNr);

        randomObstacle = randomNr;
    }

    IEnumerator GameOver()
    {
        gowShown = true;
        yield return new WaitForSeconds(0.5f);

        inGameScore.SetActive(false);
        endScene.SetActive(true);

        if (endScore > PlayerPrefs.GetInt("Best", 0))
        {
            PlayerPrefs.SetInt("Best", endScore);

            // Archivment unlock "level 5"
            if (endScore >= 5 && endScore < 25 && PlayerPrefs.GetInt("achievement", 0) != 5)
            {                
                PlayerPrefs.SetInt("achievement", 5);
                achievementLevel.text = "5";
                animGameOver.Play("GameOverB");
            }
            
            // Archivment unlock "level 25"
            else if (endScore >= 25 && endScore < 50 && PlayerPrefs.GetInt("achievement", 0) != 25)
            {
                PlayerPrefs.SetInt("achievement", 25);
                achievementLevel.text = "25";
                animGameOver.Play("GameOverB");
            }

            // Archivment unlock "level 50"
            else if (endScore >= 50 && endScore < 75 && PlayerPrefs.GetInt("achievement", 0) != 50)
            {
                PlayerPrefs.SetInt("achievement", 50);
                achievementLevel.text = "50";
                animGameOver.Play("GameOverB");
            }

            // Archivment unlock "level 75"
            else if (endScore >= 75 && PlayerPrefs.GetInt("achievement", 0) != 75)
            {
                PlayerPrefs.SetInt("achievement", 75);
                achievementLevel.text = "75";
                animGameOver.Play("GameOverB");
            }

            // New record:
            else if (endScore > 75 && PlayerPrefs.GetInt("achievement", 0) == 75)
            {
                animGameOver.Play("GameOverC");
            }

            else
            {
                animGameOver.Play("GameOver");
            }

        } 

        else
        {
            animGameOver.Play("GameOver");                                    
        } 
    }
}
