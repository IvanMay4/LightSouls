using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour{
    public RawImage image;
    public TMP_Text text;
    public Color defaultColor;

    private void Start(){
        defaultColor = image.color;
    }

    public void ResetItem(){
        image.texture = null;
        image.color = defaultColor;
        text.text = "";
    }
}
