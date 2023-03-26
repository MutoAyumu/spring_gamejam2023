using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField, Tooltip("���C���[")] LayerMask _layerMask;
    [SerializeField, Tooltip("Ray�̒���")] float _rayLength;
    /// <summary>�����ƂȂ�l����ID</summary>
    int _id;
    
    void Start()
    {
        //GameManager���瓚���ƂȂ�ID���Q��
        //_id = FindObjectOfType<GameManager>().ID;
    }

   �@
    void Update()
    {
        //���N���b�N������
        if(Input.GetMouseButton(0))
        {
            //�߂�l��True�̎�
            if(Click())
            {
                //GameManager�̃N���A�̏������s���֐����Ă�
            }
        }
    }

    /// <summary>Ray���΂����������I�u�W�F�N�g(�L�����N�^�[)��ID�������ƂȂ�ID�ƈ�v���邩�ǂ����E��v������True��Ԃ��܂�</summary>
    bool Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _rayLength,_layerMask);
        if(hit.collider)
        {
            //�L�����N�^�[��ID���擾
            int ID = hit.collider.gameObject.GetComponent<CharacterController>().CharacterID;
            //�����ƂȂ�ID�ƈ�v���Ă�����
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
