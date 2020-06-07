using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class House : MonoBehaviour
{
    private GameObject gmO;
    private GameManager gm;

    //Audio
    AudioSource audio;
    [SerializeField] AudioClip dmgNoise;

    // Start is called before the first frame update
    void Start()
    {
        audio = Camera.main.GetComponent<AudioSource>();
        gmO = GameObject.FindGameObjectWithTag("GameManager");
        gm = gmO.GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Enemy")
        {
            Destroy(collider2D.gameObject);
            audio.PlayOneShot(dmgNoise);
            gm.health--;
            gm.ChangeHealthText();
        }

        if (gm.health <= 0)
            gm.Lose();
    }
    


}
