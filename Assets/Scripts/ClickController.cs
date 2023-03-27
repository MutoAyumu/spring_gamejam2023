using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField, Tooltip("���C���[")] LayerMask _layerMask;
    [SerializeField, Tooltip("Ray�̒���")] float _rayLength;

    /// <summary>�����ƂȂ�l����ID</summary>
    int _id;
    AudioSource _audioSource;
    [SerializeField, Tooltip("������")] AudioClip _clearclip;
    [SerializeField, Tooltip("�s������")] AudioClip _gameoverclip;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //GameManager���瓚���ƂȂ�ID���Q��
        _id = GameManager.Instance.AnswerID;
    }


    void Update()
    {
        //���N���b�N������
        if (Input.GetMouseButton(0))
        {
            if (Click() == "GameClear")
            {
                _audioSource.PlayOneShot(_clearclip);
                //GameManager�̃N���A�̏������s���֐����Ă�
                GameManager.Instance.GameClear();
            }
            else if (Click() == "GameOver")
            {
                _audioSource.PlayOneShot(_gameoverclip);
                //��������Ȃ��L�����N�^�[���N���b�N�������_��GameManager��GameOver���Ă�
                GameManager.Instance.GameOver();
            }
        }
    }

    /// <summary>Ray���΂����������I�u�W�F�N�g(�L�����N�^�[)��ID�������ƂȂ�ID�ƈ�v���邩�ǂ����E
    /// ��v������GameClear��Ԃ��܂��E��������Ȃ��L�����N�^�[���N���b�N�����ꍇGameOver�ɂȂ�悤�ɂ���</summary>
    string Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _rayLength, _layerMask);
        //�N���b�N���ꂽ�I�u�W�F�N�g���L�����N�^�[��������
        if (hit.collider)
        {
            //�L�����N�^�[��ID���擾
            int ID = hit.collider.gameObject.GetComponent<CharacterController>().CharacterID;
            //�����ƂȂ�ID�ƈ�v���Ă�����
            if (ID == _id)
            {
                return "GameClear";
            }
            //��v���Ă��Ȃ�������
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
