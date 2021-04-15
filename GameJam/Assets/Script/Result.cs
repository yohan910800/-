using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public void nextPlay()//ゲームスタート
    {
        SceneManager.LoadScene("Play");
    }

    public void Quit()//ゲーム終了
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
              Application.Quit();
        #endif
    }
}
