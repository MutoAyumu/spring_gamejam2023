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
    [SerializeField] private float _timeLimit = 5f;
    [SerializeField] private int _answerID = 0;

    [SerializeField] Unmask _unmask;
    [SerializeField] Animator _addScoreText;

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

    private void OnEnable()
    {
        _manager._gameClear += SetScoreText;
    }
    private void OnDisable()
    {
        _manager._gameClear -= SetScoreText;
    }

    void SetScoreText()
    {
        _addScoreText.gameObject.SetActive(true);
        _addScoreText.transform.position = Input.mousePosition;
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
