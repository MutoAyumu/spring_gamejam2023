using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultScoreTextUI : MonoBehaviour
{
    [SerializeField] Text _scoreText;

    private void OnEnable()
    {
        var g = GameObject.Find("Manager").GetComponent<GameManager>();
        g._gameOver += SetText;
    }
    private void OnDisable()
    {
        var g = GameObject.Find("Manager").GetComponent<GameManager>();
        g._gameOver -= SetText;
    }

    void SetText()
    {
        var score = GameManager.Instance.Score;
        _scoreText.text = $"Score:{score}";
    }
}
