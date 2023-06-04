using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "MainWorldTest";

    public void ReturnToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
