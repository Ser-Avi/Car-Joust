using System.Collections;
using UnityEngine.SceneManagement;
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
    public int countdownTime;
    
    //Variables that can be changed in settings
   [Range(0, 10)] public float forceSetting = 5;
   public void SetLanceForce(float force){forceSetting = force;}
    public float motorForceSetting;
    public float breakForceSetting;
    [Range(0, 10)] public float spawnRateSetting;
    public void SetSpawnRate(float rate){spawnRateSetting = rate;}
    public int propNumberSetting = 8;

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

        GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuManager>().PauseScreenToggler();

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
