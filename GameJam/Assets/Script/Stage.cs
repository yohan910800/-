using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _stage = new GameObject[6];

    Player _player;

    float _scleanX;
    float _distance;

    List<GameObject> _checkStage = new List<GameObject>();

    int _checkCtr = 0;
    int _stageCtr = 3;


    // Start is called before the first frame update
    void Start()
    {
        _scleanX = 18.84f;
        _distance =_scleanX * 1.5f;

        _player = GameObject.Find("Player").GetComponent<Player>();

        //List<GameObject> stage = new List<GameObject>();


        _checkStage.Add(Instantiate(_stage[0], new Vector3(_scleanX * 0, 0, 0), Quaternion.identity));
        _checkStage.Add(Instantiate(_stage[1], new Vector3(_scleanX * 1, 0, 0), Quaternion.identity));
        _checkStage.Add(Instantiate(_stage[2], new Vector3(_scleanX * 2, 0, 0), Quaternion.identity));   
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.transform.position.x >= _distance){
            createStage();//生成、削除を行う
        }
    }

    void createStage()
    {
        Destroy(_checkStage[_checkCtr]);
        _checkStage.Add(Instantiate(_stage[Random.Range(0, 6)], new Vector3(_scleanX * _stageCtr, 0, 0), Quaternion.identity));
        _stageCtr++;//stageをインスタンスする場所を次へ次へとしていく
        _checkCtr++;//次のstageを消す
        _distance += _scleanX;//次のチェックする座標の更新
    }

}
