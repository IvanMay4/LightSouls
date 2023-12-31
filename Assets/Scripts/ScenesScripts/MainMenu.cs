using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    [SerializeField] public Button buttonContinue;

    private void LateUpdate(){
        buttonContinue.enabled = File.Exists(Settings.filenameSaveGame);
        Settings.ButtonSetEnabled(buttonContinue);
    }

    public void DeleteProgress() => Settings.DeleteFile(Settings.filenameSaveGame);

    public void LoadGame(){
        SceneManager.LoadScene("Game");
        Settings.isLoadGame = true;
        Saver.LoadSettings();
        Saver.LoadGame();
    }
}
