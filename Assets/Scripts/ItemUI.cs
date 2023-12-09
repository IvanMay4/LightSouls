using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler{
    public Item item;
    public int count;

    public void OnPointerClick(PointerEventData eventData){
        if (item == null) return;
        WindowItem.instance.image.texture = item.texture;
        WindowItem.instance.image.color = Color.HSVToRGB(0, 0, 100);
        WindowItem.instance.textTitle.text = item.title;
        WindowItem.instance.textDescription.text = item.description;
    }
}
