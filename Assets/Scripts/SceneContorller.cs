using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneContorller : MonoBehaviour
{
    [SerializeField, Header("�J�ڐ�V�[��")] string _sceneName;
    public void SceneLoad()
    {
        SceneManager.LoadScene(_sceneName);
    }

    /// <summary> �V�[���J�� </summary>
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
