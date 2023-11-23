using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
MenuManager should be attached to the Canvas.
Used for managing the menu button functions.
Main screen is set either to the menu buttons or the pause screen.
Has variables for the main manager and the main screen.
Has methods for setting main screen, resuming, starting game scene, returning to menu scene, and exiting.
*/
public class MenuManager : MonoBehaviour
{
    MainManager mainManager;

    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject controlsScreen;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = MainManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        MainScreenManager();
    }

    //MainScreenManager manages when the main screen should be active.
    //In the first scene this is when the controls aren't active.
    //In the rest it is when the game is paused and the controls aren't active.
    private void MainScreenManager()
    {
        if (!controlsScreen.activeInHierarchy && (SceneManager.GetActiveScene().buildIndex == 0 || mainManager.isGamePaused))
        {
            mainScreen.SetActive(true);
        }
        else
        {
            mainScreen.SetActive(false);
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

    //Toggles controls screen
    public void ControlsToggler()
    {
        controlsScreen.SetActive(!controlsScreen.activeInHierarchy);
    }
}
