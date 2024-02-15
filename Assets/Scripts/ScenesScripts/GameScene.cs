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
            Player.instance.transform.position = new Vector3((float)Convert.ToDouble(Saver.valuesPlayerPosition[0]), (float)Convert.ToDouble(Saver.valuesPlayerPosition[1]), (float)Convert.ToDouble(Saver.valuesPlayerPosition[2]));
            Player.instance.transform.eulerAngles = new Vector3(0, (float)Convert.ToDouble(Saver.valuesPlayerPosition[3]), 0);
            Player.instance.level = Convert.ToInt32(Saver.valuesPlayerStats[0]);
            Player.instance.NewMaxHP(Convert.ToInt32(Saver.valuesPlayerStats[1]));
            Player.instance.NewHP(Convert.ToInt32(Saver.valuesPlayerStats[2]));
            Player.instance.NewMaxST(Convert.ToInt32(Saver.valuesPlayerStats[3]));
            Player.instance.NewST(Convert.ToInt32(Saver.valuesPlayerStats[4]));
            for (int i = 0; i < Saver.valuesSpecifications.Length; i++)
                Player.instance.specifications[Player.GetNameSpecification(i)] = Convert.ToInt32(Saver.valuesSpecifications[i]);
            for (int i = 0; i < Saver.valuesItems.Length; i++)
                Inventory.instance.AddItems(Saver.valuesItems[i][0], Convert.ToInt32(Saver.valuesItems[i][1]));
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
