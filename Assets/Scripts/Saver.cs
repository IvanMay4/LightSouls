using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Saver{
    public static string[] valuesPlayerPosition;
    public static string[] valuesPlayerStats;
    public static string[] valuesSpecifications;
    public static string[][] valuesItems;
    public static string[][] valuesEnemies;

    public static void SaveGame(GameScene gameScene){
        StreamWriter writer = new StreamWriter(Settings.filenameSaveGame);
        Player player = Player.instance;
        writer.WriteLine($"{player.transform.position.x} {player.transform.position.y} {player.transform.position.z} {player.transform.rotation.eulerAngles.y}");
        writer.WriteLine($"{player.level} {player.GetMaxHP()} {player.GetHP()} {player.GetMaxST()} {player.GetST()}");
        for(int i = 0;i < player.specifications.Values.Count - 1; i++)
            writer.Write($"{player.specifications[Player.GetNameSpecification(i)]} ");
        writer.WriteLine(player.specifications[Player.GetNameSpecification(player.specifications.Values.Count - 1)]);
        writer.WriteLine(Inventory.instance.countItems);
        for(int i = 0;i < Inventory.instance.countItems; i++)
            writer.WriteLine($"{Inventory.instance.items[i].nameItem} {Inventory.instance.items[i].count}");
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
        valuesPlayerPosition = reader.ReadLine().Split();
        valuesPlayerStats = reader.ReadLine().Split();
        valuesSpecifications = reader.ReadLine().Split();
        int countItems = Convert.ToInt32(reader.ReadLine());
        valuesItems = new string[countItems][];
        for (int i = 0; i < countItems; i++)
            valuesItems[i] = reader.ReadLine().Split();
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
