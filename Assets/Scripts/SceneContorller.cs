using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneContorller : MonoBehaviour
{
    [SerializeField, Header("‘JˆÚæƒV[ƒ“")] string _sceneName;
    public void SceneLoad()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
