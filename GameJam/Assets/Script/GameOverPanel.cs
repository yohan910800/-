using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{

    Player player;
    //Score score;

    public Text recordTxt;

    [SerializeField]
    GameObject _scoreDraw;
    private static float _score = 0;

    [SerializeField]
    public GameObject _hignScoreDraw;
    private static float _hignScore = 0;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _hignScoreDraw = GameObject.Find("HignScoreText");

        _score = player.distance;

        _scoreDraw = GameObject.Find("ScoreText");
        _scoreDraw.GetComponent<Text>().text = _score.ToString() + " km";
        ScoreCheck();
        // _score.text = player.distance.ToString() + " km";

        player.enabled = false;
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {

    }

    void ScoreCheck()
    {
        if (_score > _hignScore)
        {
            _hignScore = _score;
            _hignScoreDraw.GetComponent<Text>().text = _hignScore.ToString() + " km";
        }
        else
        {
            _hignScoreDraw.GetComponent<Text>().text = _hignScore.ToString() + " km";
        }
    }
}
