using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour{
    public string nameItem;
    public string title;
    public string description;
    public Texture texture;

    public virtual void Activate(){}
}
