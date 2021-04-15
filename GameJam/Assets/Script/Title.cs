using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject explanationPanel;

    void Start()
    {
        titlePanel.SetActive(true);
        explanationPanel.SetActive(false);
    }

    public void nextPlay()//ゲームスタート
    {
        SceneManager.LoadScene("Play");
        Time.timeScale = 1.0f;
    }
    public void nextExplanation()//操作説明へ
    {
        titlePanel.SetActive(false);
        explanationPanel.SetActive(true);
    }
    public void Quit()//ゲーム終了
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
              Application.Quit();
        #endif
    }
    public void back()//タイトルに戻る
    {
        titlePanel.SetActive(true);
        explanationPanel.SetActive(false);
    }
}
