using UnityEngine;

public class GameManagerAttachment : MonoBehaviour
{
    [Tooltip("制限時間")]
    [SerializeField] private float _timeLimit = 60f;

    private GameManager _manager = default;

    private void Awake()
    {
        _manager = GetComponent<GameManager>();

        _manager.SetTimer(_timeLimit);
        _manager.SceneNameUpdate();
        _manager.IdUpdate();

        Debug.Log("実行済");
    }
}
