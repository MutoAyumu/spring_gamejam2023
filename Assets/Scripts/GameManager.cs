using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerAttachment _attachment = default;
    [Header("テスト")]
    [SerializeField] private bool _isPlaying = false;

    private static GameManager _instance = default;

    private Image _scorePanel = default;
    private Text _scoreText = default;
    private Text _timerText = default;

    private int _score = 0;
    private float _timer = 0f;
    private string _nextScene = "";
    private int _answerID = 9999;

    public GameManagerAttachment Attachment { get => _attachment; set => _attachment = value; }
    public static GameManager Instance => _instance;
    public int AnswerID { get => _answerID; protected set => _answerID = value; }

    private void Awake()
    {
        //シングルトン
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _scorePanel.gameObject?.SetActive(false);

        //ゲームシーンだったら、ゲームを開始
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            _isPlaying = true;
        }
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _timer -= Time.deltaTime;
            _timerText.text = _timer.ToString("F0");

            //時間切れ
            if (_timer <= 0f)
            {
                _scorePanel.gameObject?.SetActive(true);
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        _score = 0;

        _isPlaying = false;
        Debug.Log("GameOver");
    }

    public void GameClear()
    {
        //スコアの増加
        _score = (int)(_timer * 100);
        _scoreText.text = "SCORE : " + _score.ToString();

        _isPlaying = false;
    }

    /// <summary> シーン遷移 </summary>
    public void SceneLoad()
    {
        SceneManager.LoadScene(_nextScene);
    }

    #region GMAttachment.Awake() で実行する処理
    /// <summary> Timerの初期設定 </summary>
    public void SetTimer(float value)
    {
        _timer = value;
        Debug.Log("Timer 初期設定");
    }

    /// <summary> 遷移先のシーンの変更 </summary>
    public void SceneNameUpdate(string sceneName)
    {
        _nextScene = sceneName;
        Debug.Log($"次の遷移先は {_nextScene} です");
    }

    /// <summary> 答えのIDを設定する </summary>
    public void IdUpdate(int answer)
    {
        _answerID = answer;
        Debug.Log($"AnswerID == {_answerID}");
    }

    /// <summary> UIの初期設定 </summary>
    public void UISetting(Image result, Text score, Text timer)
    {
        _scorePanel = result;
        _scoreText = score;
        _timerText = timer;
    }
    #endregion
}
