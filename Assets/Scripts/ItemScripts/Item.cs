using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerClickHandler{
    public string nameItem;
    public string title;
    public string description;
    public Texture texture;
    public int count;

    public virtual void Activate() {}

    public void Show(){
        gameObject.GetComponent<ItemUI>().image.texture = texture;
        gameObject.GetComponent<ItemUI>().image.color = Color.HSVToRGB(0, 0, 100);
        gameObject.GetComponent<ItemUI>().text.text = $"{count}";
    }

    public void OnPointerClick(PointerEventData eventData){
        WindowItem.instance.image.texture = texture;
        WindowItem.instance.image.color = Color.HSVToRGB(0, 0, 100);
        WindowItem.instance.textTitle.text = title;
        WindowItem.instance.textDescription.text = description;
    }
}
