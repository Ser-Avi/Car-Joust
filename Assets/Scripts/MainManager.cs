using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
Main manager persists between scenes and has one Instance.
It holds a single bool, isGamePaused.
On Awake it initiates the instance as this and destroys duplicates.
Other methods are to pause and to exit.
*/
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

    //Update calls the pause manager if P or Esc are pressed and the game is active.
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) 
                && SceneManager.GetActiveScene().buildIndex != 0)
        {
            PauseManager();
        }
    }

    //Pause Manager pauses and unpauses the game.
    public void PauseManager()
    {

        if (!isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            //Perhaps add a 3 second countdown timer here. -> Adjustable game resume delay time in Options?
            Time.timeScale = 1;
        }
        isGamePaused = !isGamePaused;
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
