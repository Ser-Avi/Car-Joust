using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    [SerializeField] TMP_Dropdown gameModeDropdown;
    [SerializeField] Slider trafficSlider;
    [SerializeField] Slider lanceSlider;
    MainManager mainManager;
    
    void OnEnable()
    {
        mainManager = MainManager.Instance;
        
        if (mainManager.isGameTimed)
        {
            gameModeDropdown.value = 0;
        }
        else
        {
            gameModeDropdown.value = 1;
        }

        trafficSlider.value = mainManager.spawnRateSetting;
        lanceSlider.value = mainManager.forceSetting;
    }
}
