using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneName;
    public void LoadB(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
