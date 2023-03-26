using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("遷移先のシーン")]
    [SerializeField] private SceneNames _nextScene = SceneNames.TITLE_SCENE;
    [SerializeField] private Image _scorePanel = default;
    [Header("テスト")]
    [SerializeField] private Text _timerText = default;

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
        _scorePanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        _timerText.text = _timer.ToString("F0");

        if (_timer <= 0f)
        {
            _scorePanel.gameObject.SetActive(true);
            GameOver();
        }
    }

    private void GameOver()
    {
        _score = 0;
        //TitleSceneに戻る(Fadeとかあっても良いかも)
        SceneManager.LoadScene(_scenes[SceneNames.TITLE_SCENE]);
        Debug.Log("GameOver");
    }

    public void GameClear()
    {
        //スコアの増加
        _score = (int)(_timer * 100);
        //シーン遷移(修正あるかも)
        SceneManager.LoadScene(_scenes[_nextScene]);
    }

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
            _nextScene = SceneNames.TITLE_SCENE;
        }
        Debug.Log($"次の遷移先は {_nextScene} です");
    }

    public void IdUpdate()
    {
        //定数は良くない
        _answerID = 9999;
        Debug.Log($"Answer == {_answerID}");
    }
}

public enum SceneNames
{
    TITLE_SCENE,
    GAME_SCENE,
    RESULT_SCENE,
}
