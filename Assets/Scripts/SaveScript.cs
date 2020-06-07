using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    private int highScore;
    private int firstGameSave;
    private GameObject gm;
    // Start is called before the first frame update
    public void Awake()
    {
        // PlayerPrefs.DeleteAll(); //for testing save and load
        gm = GameObject.FindGameObjectWithTag("GameManager");
       // PlayerPrefs.SetInt("highScore", highScore);
    }

    public void Start()
    {
        
    }
    public void SaveBtn()
    {
        if(gm.GetComponent<GameManager>().highScore < gm.GetComponent<GameManager>().score)
        {
            gm.GetComponent<GameManager>().highScore = gm.GetComponent<GameManager>().score;
            highScore = gm.GetComponent<GameManager>().highScore;
           PlayerPrefs.SetInt("highScore", highScore);
        }
      
      
        
    }
    public void LoadBtn()
    {
        gm.GetComponent<GameManager>().highScore = PlayerPrefs.GetInt("highScore");
        gm.GetComponent<GameManager>().highScoreText.text = "HighScore: " + gm.GetComponent<GameManager>().highScore;




    }

    

   
}
