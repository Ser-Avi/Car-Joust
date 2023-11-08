using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
MenuManager should be attached to the Canvas.
Used for managing the menu button functions.
Has variables for the main manager and the pause screen.
Has methods for setting pause screen, resuming, starting game scene, returning to menu scene, and exiting.
*/
public class MenuManager : MonoBehaviour
{
    MainManager mainManager;

    [SerializeField] GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = MainManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainManager.isGamePaused)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }
    }

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

    //Resumes game
    public void Resume()
    {
        mainManager.PauseManager();
    }

    //Returns game to the main menu scene (0 in the index).
    public void MainMenu()
    {
        mainManager.PauseManager();
        SceneManager.LoadScene(0);
    }
}
