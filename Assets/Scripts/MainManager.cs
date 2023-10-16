using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; } //Can be accessed, but not changed.

    public bool isGamePaused;

    //On awake create instance that persists between scenes
    private void Awake()
    {
        //Destroys extra copies
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (!isGamePaused){
                Pause();
            } else{
                UnPause();
            }
            isGamePaused = !isGamePaused;
        }
    }


    //Pause stops the time
    //CREATE pause screen
    public void Pause()
    {
        Time.timeScale = 0;
    }

    //Unpause sets timescale back to one.
    //CREATE removing pause screen
    public void UnPause()
    {
        Time.timeScale = 1;
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
