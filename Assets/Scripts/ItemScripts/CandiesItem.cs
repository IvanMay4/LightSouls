using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiesItem : Item{
    public override void Activate(){
        nameItem = "Candies";
        title = "�������";
        description = "� ������� ����� ������� ������������ ������������� ����� �����. ��� ������������� ��������������� ������� ��������";
        texture = Resources.Load("Candies") as Texture;
    }

    protected override void UseItem(){
        Player.instance.GetHeal(10);
        count--;
    }
}
