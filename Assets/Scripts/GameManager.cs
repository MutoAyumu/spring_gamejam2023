using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UniRx;

public class GameManager : MonoBehaviour
{
    [Header("テスト")]
    [SerializeField] private bool _isPlaying = false;

    private static GameManager _instance = default;

    /// <summary> GameOver時に表示されるPanel </summary>
    private Image _overPanel = default;
    private Text _scoreText = default;
    private Text _timerText = default;

    private int _score = 0;
    private float _timer = 60f;
    private string _nextScene = "";
    private int _answerID = 9999;

    public event Action _gameOver;
    public event Action _gameClear;

    public static GameManager Instance => _instance;
    public int AnswerID { get => _answerID; protected set => _answerID = value; }
    /// <summary> ResultSceneで参照 </summary>
    public int Score { get => _score; protected set => _score = value; }

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

    public void Start()
    {
        //GameManagerのStart()は、最初のインスタンス時にしか呼ばれない
        //※シングルトンにすると、2回目以降に呼ばれなくなるため、publicにして別の場所で呼ぶ

        _overPanel.gameObject?.SetActive(false);

        //ゲームシーンだったら、ゲームを開始
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            _isPlaying = true;
        }
        else
        {
            _isPlaying = false;
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
                //_scorePanel.gameObject?.SetActive(true);
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        _overPanel.gameObject?.SetActive(true);
        _score = 0;

        _gameOver?.Invoke();

        _isPlaying = false;
        Debug.Log("GameOver");
    }

    public void GameClear()
    {
        //スコアの増加
        _score += 100;
        _scoreText.text = "SCORE : " + _score.ToString();

        _gameClear?.Invoke();

        _isPlaying = false;

        Observable.Timer(TimeSpan.FromSeconds(1))
    .Subscribe(_ => SceneManager.LoadScene(_nextScene));
    }

    #region GMAttachment.Awake() で実行する処理
    /// <summary> Timerの初期設定 </summary>
    public void SetTimer(float value)
    {
        _timer += value;
        Debug.Log("Timer 更新");
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
        _overPanel = result;
        _scoreText = score;
        _timerText = timer;
    }
    #endregion
}
