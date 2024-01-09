using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public int maxHP = 100;
    int currentHP = 0;
    float speed = 4f;
    public int damagePower = 10;
    int colldown = 60;
    int time = 0;
    new Rigidbody rigidbody;
    Vector3 move = new Vector3(0, 0, 0);

    private void Awake(){
        currentHP = maxHP;
        rigidbody = GetComponent<Rigidbody>();
    }

    public int GetHP() => currentHP;

    public int NewHP(int value) => currentHP = value;

    public void Update(){
        Move();
        rigidbody.velocity = move;
    }

    private void Move(){
        time++;
        if (time == colldown){
            time = 0;
            move = new Vector3(Random.Range(-2, 2) * speed, 0, Random.Range(-2, 2) * speed);
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            currentHP -= 100;
            Player.instance.GetDamage(10);
        }
    }
}
