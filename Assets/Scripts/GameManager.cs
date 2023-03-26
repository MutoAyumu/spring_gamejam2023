using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("遷移先のシーン")]
    [SerializeField] private SceneNames _sceneName = SceneNames.TITLE_SCENE;
    [SerializeField] private Image _scorePanel = default;
    [Header("テスト")]
    [SerializeField] private Text _timerText = default;

    private int _score = 0;
    private float _timer = 0f;
    private readonly Dictionary<SceneNames, string> _scenes = new()
    {
        [SceneNames.TITLE_SCENE] = "Title",
        [SceneNames.GAME_SCENE] = "MainScene",
        [SceneNames.RESULT_SCENE] = "Result",
    };

    private int _enemyID = 0;
    private int _answerID = 9999;

    public int AnswerID { get => _answerID; protected set => _answerID = value; }

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
            //TODO：スコア表示のパネル表示
            _scorePanel.gameObject.SetActive(true);
            GameOver();
        }
    }

    private void GameOver()
    {
        _score = 0;
        //TitleSceneに戻る
        //SceneManager.LoadScene(_scenes[SceneNames.TITLE_SCENE]);
        Debug.Log("GameOver");
    }

    public void GameClear()
    {
        //スコアの増加
        _score = (int)(_timer * 100);
        //シーン遷移(修正あるかも)
        SceneManager.LoadScene(_scenes[_sceneName]);
    }

    public void SetTimer(float value)
    {
        _timer = value;
    }

    /// <summary> 遷移先のシーンの変更 </summary>
    public void SceneNameUpdate()
    {
        if (SceneManager.GetActiveScene().name ==
            _scenes[SceneNames.TITLE_SCENE])
        {
            _sceneName = SceneNames.GAME_SCENE;
        }
        else if (SceneManager.GetActiveScene().name ==
            _scenes[SceneNames.GAME_SCENE])
        {
            _sceneName = SceneNames.RESULT_SCENE;
        }
        else if (SceneManager.GetActiveScene().name ==
            _scenes[SceneNames.RESULT_SCENE])
        {
            _sceneName = SceneNames.TITLE_SCENE;
        }

    }

    public void IdUpdate()
    {

    }
}

public enum SceneNames
{
    TITLE_SCENE,
    GAME_SCENE,
    RESULT_SCENE,
}
