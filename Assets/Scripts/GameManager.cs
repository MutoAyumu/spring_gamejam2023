using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("遷移先のシーン")]
    [SerializeField] private SceneNames _nextScene = SceneNames.TITLE_SCENE;
    [SerializeField] private Image _scorePanel = default;
    [SerializeField] private Text _scoreText = default;
    [Header("テスト")]
    [SerializeField] private Text _timerText = default;
    [SerializeField] private bool _isPlaying = false;

    private int _score = 0;
    private float _timer = 0f;
    private static GameManager _instance = default;

    private readonly Dictionary<SceneNames, string> _scenes = new()
    {
        [SceneNames.TITLE_SCENE] = "Title",
        [SceneNames.GAME_SCENE] = "MainScene",
        [SceneNames.RESULT_SCENE] = "Result",
    };
    private int _answerID = 9999;

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
        if (SceneManager.GetActiveScene().name ==
            _scenes[SceneNames.GAME_SCENE])
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
                _scorePanel.gameObject.SetActive(true);
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        _score = 0;

        _isPlaying = false;
        //ResultSceneに移行
        SceneManager.LoadScene(_scenes[SceneNames.RESULT_SCENE]);
        Debug.Log("GameOver");
    }

    public void GameClear()
    {
        //スコアの増加
        _score = (int)(_timer * 100);
        _scoreText.text = "SCORE : " + _score.ToString();

        _isPlaying = false;
        //シーン遷移
        SceneManager.LoadScene(_scenes[_nextScene]);
    }

    #region GMAttachment.Awake() で実行する処理
    /// <summary> Timerの初期設定 </summary>
    public void SetTimer(float value)
    {
        _timer = value;
        Debug.Log("Timer 初期設定");
    }

    /// <summary> 遷移先のシーンの変更 </summary>
    public void SceneNameUpdate()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene ==
            _scenes[SceneNames.TITLE_SCENE])
        {
            _nextScene = SceneNames.GAME_SCENE;
        }
        else if (currentScene ==
            _scenes[SceneNames.GAME_SCENE])
        {
            _nextScene = SceneNames.RESULT_SCENE;
        }
        else if (currentScene ==
            _scenes[SceneNames.RESULT_SCENE])
        {
            _nextScene = SceneNames.TITLE_SCENE;
        }
        //テストシーンの場合
        else
        {
            _nextScene = SceneNames.GAME_SCENE;
        }
        Debug.Log($"次の遷移先は {_nextScene} です");
    }

    /// <summary> 答えのIDを設定する </summary>
    public void IdUpdate()
    {
        //定数は良くない
        _answerID = 9999;
        Debug.Log($"AnswerID == {_answerID}");
    }
    #endregion
}

public enum SceneNames
{
    TITLE_SCENE,
    GAME_SCENE,
    RESULT_SCENE,
}
