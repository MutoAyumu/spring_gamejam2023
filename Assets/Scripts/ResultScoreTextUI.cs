using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultScoreTextUI : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    int _score;
    private void Awake()
    {
        _score = GameManager.Instance.Score;
        _scoreText.text = $"Score : {_score}";
    }
}
