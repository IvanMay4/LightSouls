using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureItem : Item{
    public override void Activate(){
        nameItem = "Cure";
        title = "���������";
        description = "����� �������, ��� ��� ��������� �������� �������� �� ���� ��������. ��� ������������� ������� ��� ���������� �������";
        texture = Resources.Load("Cure") as Texture;
    }
}
