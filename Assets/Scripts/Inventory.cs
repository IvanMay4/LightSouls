using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour{
    public GameObject[] itemsObjects;
    private Item[] items;
    private byte countItems;

    public static Inventory instance;

    private void Awake(){
        instance = this;
        items = new Item[itemsObjects.Length];
        countItems = 0;
    }

    private void Start(){
        gameObject.SetActive(false);
    }

    private void FixedUpdate(){
        for (int i = 0; i < countItems; i++) items[i].Show();
    }

    public void AddItems(string nameItem, int count = 1){
        for (int i = 0; i < countItems; i++)
            if (items[i].nameItem == nameItem){
                items[i].count += count;
                return;
            }
        for (int i = 0; i < itemsObjects.Length; i++)
            if (!itemsObjects[i].GetComponent<Item>()) {
                items[countItems] = GetItem(itemsObjects[i], nameItem);
                items[countItems].Activate();
                items[countItems].count = count;
                countItems++;
                return;
            }
    }

    private Item GetItem(GameObject itemObject, string nameItem){
        switch (nameItem){
            case "Cure": return itemObject.AddComponent<CureItem>();
            case "Candies": return itemObject.AddComponent<CandiesItem>();
            default: return null; 
        }
    }
}
