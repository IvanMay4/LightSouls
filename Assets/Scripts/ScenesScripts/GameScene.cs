using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour{
    [NonSerialized] public Enemy[] enemies;
    int cooldownSave = 1 * 60;
    int timeSave = 0;

    private void Start(){
        if (Settings.isLoadGame){
            Settings.isLoadGame = false;
            Player.instance.transform.position = new Vector3((float)Convert.ToDouble(Saver.valuesPlayer[0]), (float)Convert.ToDouble(Saver.valuesPlayer[1]), (float)Convert.ToDouble(Saver.valuesPlayer[2]));
            Player.instance.transform.eulerAngles = new Vector3(0, (float)Convert.ToDouble(Saver.valuesPlayer[3]), 0);
            Player.instance.SetCurrentJumps(Convert.ToInt32(Saver.valuesPlayer[4]));
            Player.instance.level = Convert.ToInt32(Saver.valuesPlayer[5]);
            Player.instance.NewMaxXP(Convert.ToInt32(Saver.valuesPlayer[6]));
            Player.instance.NewXP(Convert.ToInt32(Saver.valuesPlayer[7]));
            Player.instance.NewMaxHP(Convert.ToInt32(Saver.valuesPlayer[8]));
            Player.instance.NewHP(Convert.ToInt32(Saver.valuesPlayer[9]));
        }
    }

    private void FixedUpdate(){
        timeSave++;
        if(timeSave >= cooldownSave){
            timeSave = 0;
            Saver.SaveGame(this);
        }
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
        if (Player.instance.GetHP() == 0)
            Settings.OpenGameOver();
        else if (enemies.Length == 0)
            Settings.OpenWin();
    }
}
