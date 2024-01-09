using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour{
    [NonSerialized] public static MenuPause instance;

    private void Awake(){
        instance = this;
    }

    private void Start(){
        gameObject.SetActive(false);
    }
}
