using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    GameObject gameOverPanel;
    Text recordTxt;
    bool justOnce;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        recordTxt = GameObject.Find("PlayerUi").transform.GetChild(1).
            gameObject.GetComponent<Text>();
        
        DontDestroyOnLoad(recordTxt);
        justOnce = false;
    }
    void Start()
    {
        gameOverPanel = Resources.Load<GameObject>("Prefabs/GameOverPanel");
        if (gameObject.tag=="Up")
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if (justOnce == false)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Instantiate(gameOverPanel, transform.position, Quaternion.identity);
                //recordTxt.text = GameObject.Find("Player").GetComponent<Player>().distance.ToString() + " km";
                justOnce = true;
            }
        }
    }
    
}
