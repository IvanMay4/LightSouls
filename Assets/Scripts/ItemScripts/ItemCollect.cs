using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour{
    public string[] names;
    public int[] counts;

    private void OnCollisionStay(Collision collision){
        if (!collision.gameObject.CompareTag("Player")) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        for(int i = 0; i < names.Length; i++)
            Inventory.instance.AddItems(names[i], counts.Length > i? counts[i]: 1);
        Destroy(gameObject);
    }
}
