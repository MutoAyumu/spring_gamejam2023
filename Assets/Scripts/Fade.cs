using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    [SerializeField] Unmask _unmaskImage;
    [Space(15)]
    [SerializeField] Vector2 _maxScale = new Vector2(1.8f, 1.8f);
    [SerializeField] float _duration = 0.5f;
    [SerializeField] Ease _ease;
    [SerializeField]SceneContorller _sceneContorller;

    private void Start()
    {
        FadeIn();
    }

    void FadeIn()
    {
        _unmaskImage.transform.DOScale(_maxScale, _duration).SetEase(_ease);
    }
    public void FadeOut()
    {
        _unmaskImage.transform.DOScale(Vector2.zero, _duration).SetEase(_ease)
            .OnComplete(() =>
            {
                _sceneContorller.SceneLoad();
            });
    }
}
