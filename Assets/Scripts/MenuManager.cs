using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
 //StartNew starts the first scene of the game.
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //Exit quits the game. If statement is for quitting editor vs game.
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
