using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour{
    public Slider sliderVolume;

    private void Start(){
        if (!File.Exists(Settings.filenameSaveSettings))
            return;
        Saver.LoadSettings();
        sliderVolume.value = Settings.volume;
    }

    private void LateUpdate(){
        Settings.volume = sliderVolume.value;
        Saver.SaveSettings();
    }

    public static void ExitMain() => Settings.OpenMainMenu();
}
