using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Saver{
    public static float volume;
    public static int levelComplexity;
    public static string[] valuesPlayer;
    public static string[][] valuesEnemies;

    public static void SaveGame(GameScene gameScene){
        StreamWriter writer = new StreamWriter(Settings.filenameSaveGame);
        Player player = gameScene.GetComponentInParent<Player>();
        writer.WriteLine(player.transform.position.x + " " + player.transform.position.y + " " + player.transform.position.z + " " + player.transform.rotation.eulerAngles.y + " " + player.GetCurrentJumps() + " " + player.GetHP());
        writer.WriteLine(gameScene.enemies.Length);
        foreach (Enemy enemy in gameScene.enemies)
            writer.WriteLine(enemy.transform.position.x + " " + enemy.transform.position.z + " " + enemy.GetHP());
        writer.Close();
    }

    public static void SaveSettings(){
        StreamWriter writer = new StreamWriter(Settings.filenameSaveSettings);
        writer.WriteLine(Settings.volume + " " + Settings.levelComplexity);
        writer.Close();
    }

    public static void LoadGame() {
        StreamReader reader = new StreamReader(Settings.filenameSaveGame);
        valuesPlayer = reader.ReadLine().Split();
        int countEnemies = Convert.ToInt32(reader.ReadLine());
        valuesEnemies = new string[countEnemies][];
        for (int i = 0; i < countEnemies; i++)
            valuesEnemies[i] = reader.ReadLine().Split();
        reader.Close();
    }

    public static void LoadSettings(){
        StreamReader reader = new StreamReader(Settings.filenameSaveSettings);
        string[] valuesSettings = reader.ReadLine().Split();
        volume = (float)Convert.ToDouble(valuesSettings[0]);
        levelComplexity = Convert.ToInt32(valuesSettings[1]);
        reader.Close();
    }
}
