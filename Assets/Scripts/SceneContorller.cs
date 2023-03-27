using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneContorller : MonoBehaviour
{
    [SerializeField, Header("遷移先シーン")] string _sceneName;
    public void SceneLoad()
    {
        SceneManager.LoadScene(_sceneName);
    }

    /// <summary> シーン遷移 </summary>
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
