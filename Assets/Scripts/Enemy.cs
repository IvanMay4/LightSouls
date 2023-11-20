using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    [SerializeField] public int maxHP = 100;
    private int currentHP = 0;
    [SerializeField] float speed = 4f;
    private int colldown = 60;
    private int time = 0;
    private GameScene gameScene;
    private new Rigidbody rigidbody;
    private Vector3 move = new Vector3(0, 0, 0);

    private void Awake(){
        currentHP = maxHP;
        rigidbody = GetComponent<Rigidbody>();
        gameScene = FindAnyObjectByType<GameScene>();
    }

    public int GetHP() => currentHP;

    public int NewHP(int value) => currentHP = value;

    public void Update(){
        if (!gameScene.GetIsPlayGame()){
            rigidbody.velocity = new Vector3(0, 0, 0);
            return;
        }
        time++;
        if (time == colldown){
            time = 0;
            move = new Vector3(Random.Range(-2, 2) * speed, 0, Random.Range(-2, 2) * speed);
        }
        rigidbody.velocity = move;
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            currentHP -= 100;
            collision.gameObject.GetComponent<Player>().GetDamage(10 * Settings.levelComplexity);
        }
    }
}
