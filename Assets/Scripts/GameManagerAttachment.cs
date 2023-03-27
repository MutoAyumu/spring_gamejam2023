using UnityEngine;
using UnityEngine.UI;

public class GameManagerAttachment : MonoBehaviour
{
    [Tooltip("次の遷移先")]
    [SerializeField] private string _nextScene = "";

    [SerializeField] private Image _scorePanel = default;
    [SerializeField] private Text _scoreText = default;
    [SerializeField] private Text _timerText = default;
    [Tooltip("制限時間")]
    [Range(20f, 200f)]
    [SerializeField] private float _timeLimit = 60f;
    [SerializeField] private int _answerID = 0;

    [SerializeField] Unmask _unmask;

    private GameManager _manager = default;

    private void Awake()
    {
        _manager = GameObject.Find("Manager").GetComponent<GameManager>();

        //値の初期化
        _manager.SetTimer(_timeLimit);
        _manager.SceneNameUpdate(_nextScene);
        _manager.IdUpdate(_answerID);
        _manager.UISetting(_scorePanel, _scoreText, _timerText);

        Debug.Log("実行済");
    }

    private void Start()
    {
        _manager.Start();

        Cursor.visible = false;
    }
    private void Update()
    {
        _unmask.transform.position = Input.mousePosition;
    }
}
