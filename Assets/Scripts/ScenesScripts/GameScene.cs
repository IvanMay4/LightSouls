using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour{
    public Enemy[] enemies;
    public GameObject menuPause;
    private Player player;
    private int cooldownSave = 1 * 60;
    private int timeSave = 0;

    private void Start(){
        player = GetComponent<Player>();
        if (Settings.isLoadGame){
            Settings.isLoadGame = false;
            player.transform.position = new Vector3((float)Convert.ToDouble(Saver.valuesPlayer[0]), (float)Convert.ToDouble(Saver.valuesPlayer[1]), (float)Convert.ToDouble(Saver.valuesPlayer[2]));
            player.transform.eulerAngles = new Vector3(0, (float)Convert.ToDouble(Saver.valuesPlayer[3]), 0);
            player.SetCurrentJumps(Convert.ToInt32(Saver.valuesPlayer[4]));
            player.level = Convert.ToInt32(Saver.valuesPlayer[5]);
            player.NewMaxXP(Convert.ToInt32(Saver.valuesPlayer[6]));
            player.NewXP(Convert.ToInt32(Saver.valuesPlayer[7]));
            player.NewMaxHP(Convert.ToInt32(Saver.valuesPlayer[8]));
            player.NewHP(Convert.ToInt32(Saver.valuesPlayer[9]));
        }
    }

    private void FixedUpdate(){
        timeSave++;
        if(timeSave >= cooldownSave){
            timeSave = 0;
            Saver.SaveGame(this);
        }
    }

    public void ShowMenuPause() => menuPause.SetActive(true);
    
    public void HiddenMenuPause() => menuPause.SetActive(false);

    public void SetMenuPause(){
        if (menuPause.activeSelf)
            HiddenMenuPause();
        else
            ShowMenuPause();
    }

    public void DeleteEnemy(int indexEnemy){
        Destroy(enemies[indexEnemy].gameObject);
        Enemy[] newEnemies = new Enemy[enemies.Length - 1];
        for (int i = 0; i < indexEnemy; i++)
            newEnemies[i] = enemies[i];
        for (int i = indexEnemy + 1; i < enemies.Length; i++)
            newEnemies[i - 1] = enemies[i];
        enemies = newEnemies;
    }

    private void LateUpdate(){
        for (int i = 0; i < enemies.Length; i++)
            if (enemies[i].GetHP() <= 0)
                DeleteEnemy(i);
        if (player.GetHP() == 0)
            Settings.OpenGameOver();
        else if (enemies.Length == 0)
            Settings.OpenWin();
    }
}
