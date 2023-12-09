using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    [SerializeField] public int maxHP = 100;
    private int currentHP = 0;
    [SerializeField] float speed = 4f;
    [SerializeField] public int damagePower = 10;
    private int colldown = 60;
    private int time = 0;
    private new Rigidbody rigidbody;
    private Vector3 move = new Vector3(0, 0, 0);

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
            Player.instance.GetExperience(5);
        }
    }
}
