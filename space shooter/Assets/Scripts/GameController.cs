using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text timeUpText;
    public AudioSource winMusic;
    public AudioSource loseMusic;
    public AudioSource BGM;
    public Collider playerCollider;
    public Collider boltCollider;


    private bool gameOver;
    private bool restart;
    private int score;
    float currentTime = 0f;
    float startingTime = 15f;
    [SerializeField] Text countdownText;


    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        timeUpText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        currentTime = startingTime;
        playerCollider.enabled = true;
        boltCollider.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.Q))
            {
                SceneManager.LoadScene("Space Shooter"); // or whatever the name of your scene is
            }
        }

       
       currentTime -= 1 * Time.deltaTime;
       countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            gameOver = true;
            restart = true;
            timeUpText.text = "Time's up!";
            playerCollider.enabled = false;
            boltCollider.enabled = false; 
            BGM.Stop();
        }

    }



    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Q' to Restart";
                restart = true;
                break;
            }  
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score == 100 | score == 105 | score == 110 | score == 115)
        {
            winText.text = "You win! Game Created by Jason Ajucum!";
            gameOver = true;
            restart = true;
            BGM.Stop();
            winMusic.Play();
            playerCollider.enabled = false;
            boltCollider.enabled = false;
            GameObject.Find("Background").GetComponent<BG_Scroller>().scrollSpeed = -30f;

        }
    }


    public void GameOver()
    {
        gameOverText.text = "Game Over! Game Made by Jason Ajucum!";
        gameOver = true;
        restart = true;
        loseMusic.Play();
        BGM.Stop();
        boltCollider.enabled = false;
    }


}


