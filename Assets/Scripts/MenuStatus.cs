using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStatus : MonoBehaviour{
    [NonSerialized] public static MenuStatus instance;

    private void Awake(){
        instance = this;
    }

    private void Start(){
        gameObject.SetActive(false);
    }
}
