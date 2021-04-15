using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    GameOverPanel _gameOverPanel;

    [SerializeField]
    public GameObject _hignScoreDraw;
    public float _hignScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<GameOverPanel>();
        _hignScoreDraw = GameObject.Find("HignScoreText");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ScoreCheck()
    {
        /*if (_gameOverPanel._score > _hignScore)
        {
            _hignScore = _gameOverPanel._score;
            _hignScoreDraw.GetComponent<Text>().text = _hignScore.ToString() + " km";
        }*/
    }
}
