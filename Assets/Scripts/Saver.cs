using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Saver{
    public static string[] valuesPlayer;
    public static string[][] valuesEnemies;

    public static void SaveGame(GameScene gameScene){
        StreamWriter writer = new StreamWriter(Settings.filenameSaveGame);
        Player player = Player.instance;
        writer.WriteLine($"{player.transform.position.x} {player.transform.position.y} {player.transform.position.z} {player.transform.rotation.eulerAngles.y} " +
            $"{player.GetCurrentJumps()} {player.level} {player.GetMaxXP()} {player.GetXP()} {player.GetMaxHP()} {player.GetHP()}");
        writer.WriteLine(gameScene.enemies.Length);
        foreach (Enemy enemy in gameScene.enemies)
            writer.WriteLine(enemy.transform.position.x + " " + enemy.transform.position.z + " " + enemy.GetHP());
        writer.Close();
    }

    public static void SaveSettings(){
        StreamWriter writer = new StreamWriter(Settings.filenameSaveSettings);
        writer.WriteLine(Settings.volume);
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
        Settings.volume = (float)Convert.ToDouble(valuesSettings[0]);
        reader.Close();
    }
}
