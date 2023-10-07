using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.P))
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
}
