using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField, Tooltip("レイヤー")] LayerMask _layerMask;
    [SerializeField, Tooltip("Rayの長さ")] float _rayLength;
    /// <summary>答えとなる人物のID</summary>
    int _id;
    
    void Start()
    {
        //GameManagerから答えとなるIDを参照
        //_id = FindObjectOfType<GameManager>().ID;
    }

   　
    void Update()
    {
        //左クリックしたら
        if(Input.GetMouseButton(0))
        {
            //戻り値がTrueの時
            if(Click())
            {
                //GameManagerのクリアの処理を行う関数を呼ぶ
            }
        }
    }

    /// <summary>Rayを飛ばし当たったオブジェクト(キャラクター)のIDが答えとなるIDと一致するかどうか・一致したらTrueを返します</summary>
    bool Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _rayLength,_layerMask);
        if(hit.collider)
        {
            //キャラクターのIDを取得
            int ID = hit.collider.gameObject.GetComponent<CharacterController>().CharacterID;
            //答えとなるIDと一致していたら
            if(ID == _id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
