using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    [SerializeField] GameObject creditPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameBtn()
    {
        this.gameObject.GetComponent<AdManage>().PlayInterstitialAd();
       // SceneManager.LoadScene("GameScene");
    }
    public void MainMenuBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
    public void CreditBtn()
    {
        creditPanel.SetActive(!creditPanel.activeSelf);
    }
    public void BackBtn()
    {
        creditPanel.SetActive(!creditPanel.activeSelf);
    }
}
