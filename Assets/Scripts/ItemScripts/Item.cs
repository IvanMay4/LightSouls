using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        if (eventData.button == PointerEventData.InputButton.Left) WindowItemShow();
        if (eventData.button == PointerEventData.InputButton.Right) UseItem();
    }

    private void WindowItemShow(){
        WindowItem.instance.image.texture = texture;
        WindowItem.instance.image.color = Color.HSVToRGB(0, 0, 100);
        WindowItem.instance.textTitle.text = title;
        WindowItem.instance.textDescription.text = description;
    }

    protected virtual void UseItem(){}

    public void Delete(){
        gameObject.GetComponent<ItemUI>().ResetItem();
        WindowItem.instance.ResetWindowItem();
        Destroy(this);
    }
}
