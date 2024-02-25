using System.Collections;
using UnityEngine;
using TMPro;

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
    public bool isGameOver = false;
    public int countdownTime;

    public float motorForceSetting;
    public float breakForceSetting;

    //Variables that can be changed in settings
    [Range(0, 10)] public float forceSetting;
    public void SetLanceForce(float force) { forceSetting = force; }
    [Range(0, 10)] public float spawnRateSetting;
    public void SetSpawnRate(float rate) { spawnRateSetting = rate; }
    [Range(0,10)] public float propNumberSetting;
    public void SetPropNumber(float number) { propNumberSetting = number; }
    public bool isGameTimed = true;     //if false -> it is scored. Method to change is further down.
    public float gameTime = 30;
    public void SetGameTime(string time) { gameTime = int.Parse(time); }
    public int maxScore = 10;
    public void SetGameScore(string score) { maxScore = int.Parse(score); }

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

    //Pause Manager pauses and unpauses the game.
    public void PauseManager()
    {

        if (!isGamePaused)
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuManager>().PauseScreenToggler();
            isGamePaused = true;
            Time.timeScale = 0;
        }
        else
        {
            //3 second countdown timer before resuming
            IEnumerator coroutine = ResumeCountdown(countdownTime);
            StartCoroutine(coroutine);
        }
    }

    //This coroutine creates a short countdown timer when resuming the game.
    IEnumerator ResumeCountdown(int time)
    {
        //finding gameobjects for countdown screen
        GameObject countdownScreen = GameObject.Find("Countdown Screen");
        GameObject countdownText = countdownScreen.transform.GetChild(0).gameObject;
        GameObject panel = countdownScreen.transform.GetChild(1).gameObject;

        panel.SetActive(true);
        countdownText.GetComponent<TextMeshProUGUI>().text = countdownTime.ToString();
        countdownText.SetActive(true);

        //countdown by one each second and update timer
        while (time > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            time -= 1;
            countdownText.GetComponent<TextMeshProUGUI>().text = time.ToString();
        }

        //turn screen off and resume game
        panel.SetActive(false);
        countdownText.SetActive(false);
        isGamePaused = false;
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

    public void GameModeDropdown(int index)
    {
        //Spaghetti code, but this is so that options can be changed after returning to the menu
        GameObject gameTimeObj = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Options Screen").Find("Max Time").gameObject;
        GameObject gameScoreObj = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Options Screen").Find("Max Score").gameObject;
        if (index == 0)
        {
            isGameTimed = true;
            gameTimeObj.SetActive(true);
            gameScoreObj.SetActive(false);

        }
        else if (index == 1)
        {
            isGameTimed = false;
            gameTimeObj.SetActive(false);
            gameScoreObj.SetActive(true);
        }
    }
}
