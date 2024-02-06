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
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = MainManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

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

    //Resumes game with countdown
    public void Resume()
    {
        PauseScreenToggler();
        mainManager.PauseManager();
    }

    //Returns game to the main menu scene (0 in the index).
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    //Toggles controls screen
    public void ControlsToggler()
    {  
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainScreen.SetActive(!mainScreen.activeInHierarchy);
        }
        else
        {
            pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
        }
        
        controlsScreen.SetActive(!controlsScreen.activeInHierarchy);
    }

    public void OptionsToggler()
    {
        mainScreen.SetActive(!mainScreen.activeInHierarchy);
        optionsScreen.SetActive(!optionsScreen.activeInHierarchy);
    }

    public void PauseScreenToggler()
    {
        pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
    }

    public void ResetGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
