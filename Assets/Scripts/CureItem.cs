using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CureItem : Item{
    public override void Activate(){
        nameItem = "Cure";
        title = "Лекарство";
        description = "Ходят легенды, что это лекарство способно исцелить от всех болезней";
        texture = Resources.Load("Cure") as Texture;
    }
}
