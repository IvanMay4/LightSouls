using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowItem : MonoBehaviour{
    public RawImage image;
    public TMP_Text textTitle;
    public TMP_Text textDescription;
    Color defaultColor;

    public static WindowItem instance;

    private void Awake(){
        instance = this;
        defaultColor = image.color;
    }

    public void OnEnable(){
        ResetWindowItem();
    }

    public void ResetWindowItem(){
        image.texture = null;
        image.color = defaultColor;
        textTitle.text = "";
        textDescription.text = "";
    }
}
