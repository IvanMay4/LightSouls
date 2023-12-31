using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowItem : MonoBehaviour{
    [SerializeField] public RawImage image;
    [SerializeField] public TMP_Text textTitle;
    [SerializeField] public TMP_Text textDescription;
    private Color defaultColor;

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
