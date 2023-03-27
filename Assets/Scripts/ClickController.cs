using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField, Tooltip("レイヤー")] LayerMask _layerMask;
    [SerializeField, Tooltip("Rayの長さ")] float _rayLength;

    /// <summary>答えとなる人物のID</summary>
    int _id;
    AudioSource _audioSource;
    [SerializeField, Tooltip("正解音")] AudioClip _clearclip;
    [SerializeField, Tooltip("不正解音")] AudioClip _gameoverclip;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //GameManagerから答えとなるIDを参照
        _id = GameManager.Instance.AnswerID;
    }


    void Update()
    {
        //左クリックしたら
        if (Input.GetMouseButton(0))
        {
            if (Click() == "GameClear")
            {
                _audioSource.PlayOneShot(_clearclip);
                //GameManagerのクリアの処理を行う関数を呼ぶ
                GameManager.Instance.GameClear();
            }
            else if (Click() == "GameOver")
            {
                _audioSource.PlayOneShot(_gameoverclip);
                //答えじゃないキャラクターをクリックした時点でGameManagerのGameOverを呼ぶ
                GameManager.Instance.GameOver();
            }
        }
    }

    /// <summary>Rayを飛ばし当たったオブジェクト(キャラクター)のIDが答えとなるIDと一致するかどうか・
    /// 一致したらGameClearを返します・答えじゃないキャラクターをクリックした場合GameOverになるようにした</summary>
    string Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _rayLength, _layerMask);
        //クリックされたオブジェクトがキャラクターだった時
        if (hit.collider)
        {
            //キャラクターのIDを取得
            int ID = hit.collider.gameObject.GetComponent<CharacterController>().CharacterID;
            //答えとなるIDと一致していたら
            if (ID == _id)
            {
                return "GameClear";
            }
            //一致していなかったら
            else
            {
                return "GameOver";
            }
        }
        else
        {
            return "None";
        }
    }
}
