using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour{
    [SerializeField] Enemy enemy;
    [SerializeField] int countEnemy = 0;

    void Start(){
        GameScene gameScene = FindAnyObjectByType<GameScene>();
        if (Settings.isLoadGame){
            gameScene.enemies = new Enemy[Saver.valuesEnemies.Length];
            for (int i = 0; i < gameScene.enemies.Length; i++){
                gameScene.enemies[i] = Instantiate(enemy, new Vector3((float)Convert.ToDouble(Saver.valuesEnemies[i][0]), 1, (float)Convert.ToDouble(Saver.valuesEnemies[i][1])), new Quaternion());
                gameScene.enemies[i].NewHP(Convert.ToInt32(Saver.valuesEnemies[i][2]));
            }
            return;
        }
        gameScene.enemies = new Enemy[countEnemy];
        for (int i = 0; i < countEnemy; i++)
            gameScene.enemies[i] = Instantiate(enemy, new Vector3(UnityEngine.Random.Range(0, 50), 1, UnityEngine.Random.Range(0, 50)), new Quaternion());
    }
}
