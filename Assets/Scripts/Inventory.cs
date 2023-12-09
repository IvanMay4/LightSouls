using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour{
    ItemUI[] items;

    public static Inventory instance;

    private void Awake(){
        instance = this;
        items = new ItemUI[FindObjectsOfType<ItemUI>().Length];
        for (int i = 0; i < items.Length; i++)
            items[i] = FindObjectsOfType<ItemUI>()[items.Length - i - 1];
    }

    private void Start(){
        gameObject.SetActive(false);
    }

    private void FixedUpdate(){
        for (int i = 0; i < items.Length; i++)
            if (items[i].item != null){
                items[i].gameObject.GetComponentInChildren<RawImage>().texture = items[i].item.texture;
                items[i].gameObject.GetComponentInChildren<RawImage>().color = Color.HSVToRGB(0, 0, 100);
                items[i].gameObject.GetComponentInChildren<TMP_Text>().text = $"{items[i].count}";
            }
    }

    public void AddItems(string nameItem, int count = 1){
        for (int i = 0; i < items.Length; i++)
            if (items[i].item != null) {
                if (items[i].item.nameItem == nameItem){
                    items[i].count += count;
                    return;
                }
            }
        for (int i = 0; i < items.Length; i++)
            if (items[i].item == null){
                items[i].item = GetItem(nameItem);
                items[i].item.Activate();
                items[i].count = count;
                return;
            }
    }

    private Item GetItem(string nameItem){
        if (nameItem == "Cure") return gameObject.AddComponent<CureItem>();
        return null;
    }
}
