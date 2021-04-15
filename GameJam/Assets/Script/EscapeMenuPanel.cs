using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuPanel : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

   public void Quit()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        player.justOnceEscapMenu = false;
   }
    
   public void Continue()
   {
        Time.timeScale = 1.0f;
        player.justOnceEscapMenu = false ;
        Destroy(gameObject);
   }
}
