using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public enum GameStatus
    {
        lose,play
    }
   public enum EnemyStatus
    {
        normal,flying,bigger
    }
    public GameStatus gameStatus = GameStatus.play;
    public int maxHealth=10;
    public int health;
    public int waveNum=1;
    public int score = 0;
    public int highScore;
    private float spawnRate = 1.5f;
    public int waveScore;
    public EnemyStatus enemyStatus;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyBigger;
    //[SerializeField] GameObject trap;
    [SerializeField] List<Transform> spawnsers;

    //UI
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthScore;
    [SerializeField] private Text announceText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private Text loseText;
    [SerializeField] public Text highScoreText;
    [SerializeField] public Text muteBtnText;

    //Music
    private AudioSource musicSource;
    [SerializeField] AudioClip hitNoise;
    [SerializeField] AudioClip loseNoise;
    bool mute = false;

    // Start is called before the first frame update
    void Start()
    {
        announceText.text = "Press on the corona to destroy it and protect the hospital";
        StartCoroutine("Announce");
        pauseBtn.SetActive(true);
        this.gameObject.GetComponent<SaveScript>().LoadBtn();
        enemyStatus = EnemyStatus.normal;
        musicSource = Camera.main.GetComponent<AudioSource>();
        StartCoroutine("WaveSystem");
        health = maxHealth;
        healthScore.text = "Health: " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pausePanel.activeSelf)
        Clicker();
    }
    public void Clicker()
    {
      

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2d= Physics2D.Raycast(pos,Vector2.zero);

            if(hit2d.collider!=null)
                if (hit2d.collider.gameObject.tag == "Enemy")
                {
                    musicSource.PlayOneShot(hitNoise);
                    hit2d.collider.gameObject.GetComponent<EnemyMovement>().health--;
                    if (hit2d.collider.gameObject.GetComponent<EnemyMovement>().health<=0)
                    {
                        score += 1;
                        Destroy(hit2d.collider.gameObject);
                    }
                   

                }

                scoreText.text = "Score: " + score;
        }

    }

    IEnumerator WaveSystem()
    {
        
        int countEnemy = Random.Range(waveNum, waveNum * 2);

        switch(enemyStatus)
        {
            case EnemyStatus.flying:
                for (int i = 0; i < countEnemy; i++)
                {
                    int s = Random.Range(0, 4);
                    GameObject enemySpawn = Instantiate(enemy, spawnsers[s]);
                    if (s == 1 || s==3)
                        enemySpawn.GetComponent<SpriteRenderer>().flipX = true;
                    if (s == 2 || s == 3)
                        enemySpawn.GetComponent<Rigidbody2D>().gravityScale = 0;

                    if (waveNum / 5 >= 1)
                        enemySpawn.GetComponent<EnemyMovement>().speed *= (1 + waveNum / 5);


                    yield return new WaitForSeconds(spawnRate);
                }
                break;

            case EnemyStatus.normal:
                for (int i = 0; i < countEnemy; i++)
                {
                    int s = Random.Range(0, 2);
                    GameObject enemySpawn = Instantiate(enemy, spawnsers[s]);
                    if (s == 1)
                        enemySpawn.GetComponent<SpriteRenderer>().flipX = true;
                    if (waveNum / 5 >= 1)
                        enemySpawn.GetComponent<EnemyMovement>().speed *= (1 + waveNum / 5);


                    yield return new WaitForSeconds(spawnRate);
                }
                break;
            case EnemyStatus.bigger:
                for (int i = 0; i < countEnemy; i++)
                {
                    GameObject enemySpawn;
                    int s = Random.Range(0, 4);
                    int e = Random.Range(0, 2);
                    if(e==0)
                     enemySpawn = Instantiate(enemy, spawnsers[s]);
                    else
                    enemySpawn = Instantiate(enemyBigger, spawnsers[s]);
                    if (s == 1 || s == 3)
                        enemySpawn.GetComponent<SpriteRenderer>().flipX = true;
                    if (s == 2 || s == 3)
                        enemySpawn.GetComponent<Rigidbody2D>().gravityScale = 0;

                    if (waveNum / 5 >= 1)
                        enemySpawn.GetComponent<EnemyMovement>().speed *= (1 + waveNum / 5);


                    yield return new WaitForSeconds(spawnRate);
                }
                break;
        }
       
        waveNum++;

        if (waveNum == 10)
        {
            announceText.text = "Corona can now fly!!";
            StartCoroutine("Announce");
            enemyStatus = EnemyStatus.flying;
        }
        if (waveNum == 11)
        {
            announceText.text = "Corona Spread faster now!!";
            StartCoroutine("Announce");
            spawnRate = 1.2f;
        }
        if(waveNum==15)
        {
            announceText.text = "Corona evolved to be bigger!!";
            StartCoroutine("Announce");
            enemyStatus = EnemyStatus.bigger;
        }
           
        if (waveNum == 20)
        {
            announceText.text = "Corona Spread faster now!!";
            StartCoroutine("Announce");
            spawnRate = 1f;
        }
         



        StartCoroutine("WaveSystem");

       
       
    }

    IEnumerator Announce()
    {
        announceText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        announceText.gameObject.SetActive(false);
    }
    public void ChangeHealthText()
    {
        healthScore.text = "Health: " + health;
    }

    public void RestartBtn()
    {
        pauseBtn.SetActive(true);
        loseText.gameObject.SetActive(false);
        this.gameObject.GetComponent<SaveScript>().LoadBtn();
        StopAllCoroutines();
        score = 0;
        scoreText.text = "Score: " + score;
        health = maxHealth;
        waveNum = 1;
       GameObject[] enemies=GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject game in enemies)
            Destroy(game);
        ChangeHealthText();
        StartCoroutine("WaveSystem");
    }
    public void PauseBtn()
    {
        
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0f;
            pauseBtn.GetComponentInChildren<Text>().text = "Play";
        }
           
        else
        {
            Time.timeScale = 1f;
            pauseBtn.GetComponentInChildren<Text>().text = "Pause";
        }
          
      
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
    public void MuteBtn()
    {
        mute =!mute;
        
        if(mute)
        {
            musicSource.volume = 0;
            muteBtnText.text = "Unmute";
        }
        else
        {
            musicSource.volume = 100;
            muteBtnText.text = "Mute";
        }
       
    }
    public void Lose()
    {
        gameStatus = GameStatus.lose;
        musicSource.PlayOneShot(loseNoise);
        this.gameObject.GetComponent<SaveScript>().SaveBtn();
        PauseBtn();
        pauseBtn.SetActive(false);
        loseText.gameObject.SetActive(true);
        loseText.text = "You lost to corona with the score: " + score;

    }
}
