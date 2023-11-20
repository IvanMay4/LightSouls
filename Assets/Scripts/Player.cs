using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    [SerializeField] public int maxHP = 100;
    private int currentHP = 0;
    [SerializeField] public float speed = 4f;
    [SerializeField] public float jumpSpeed = 4f;
    [SerializeField] public float rotationSpeed = 10f;
    [SerializeField] public int maxJumps = 1;
    private int currentJumps;
    [SerializeField] private TMP_Text textValueHP;
    [SerializeField] private Scrollbar scrollbarHP;
    private new Rigidbody rigidbody;
    private GameScene gameScene;
    private Vector3 move;
    [SerializeField] private new Camera camera;
    [SerializeField] private Canvas menuPause;

    private void Awake(){
        SetHP(maxHP);
        currentJumps = maxJumps;
        rigidbody = GetComponent<Rigidbody>();
        this.AddComponent<GameScene>();
        gameScene = GetComponent<GameScene>();
        gameScene.menuPause = menuPause;
    }

    public int GetHP() => currentHP;

    private void SetHP(int value){
        currentHP = Math.Min(Math.Max(currentHP + value, 0), maxHP);
        textValueHP.text = Convert.ToString(currentHP) + "/" + Convert.ToString(maxHP);
        scrollbarHP.size += value * 1f / maxHP;
    }

    public void NewHP(int value){
        currentHP = value;
        textValueHP.text = Convert.ToString(currentHP) + "/" + Convert.ToString(maxHP);
        scrollbarHP.size = value * 1f / maxHP;
    }

    public void GetDamage(int valueDamage) => SetHP(-valueDamage);

    public void GetHeal(int valueHeal) => SetHP(valueHeal);

    public int GetCurrentJumps() => currentJumps;

    public void SetCurrentJumps(int countJumps) => currentJumps = countJumps;

    public void IndependentAction(){
        if (Input.GetKeyDown(KeyCode.Escape))
            gameScene.SetMenuPause();
    }

    public void Update(){
        IndependentAction();
        if (!gameScene.GetIsPlayGame()){
            rigidbody.velocity = new Vector3(0, 0, 0);
            return;
        }
        move = new Vector3(Input.GetAxis("Horizontal") * speed, rigidbody.velocity.y, Input.GetAxis("Vertical") * speed);
        transform.rotation *= Quaternion.Euler(0, (Input.GetKey(KeyCode.Q)? -1: Input.GetKey(KeyCode.E)? 1: 0) * Time.deltaTime * rotationSpeed, 0);
        if (Input.GetKeyDown(KeyCode.H))
            GetHeal(10);
        if (Input.GetKeyDown(KeyCode.Space) && currentJumps > 0){
            currentJumps--;
            move.y = (jumpSpeed - Physics.gravity.y);
        }
        rigidbody.velocity = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * move;
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground"))
            currentJumps = maxJumps;
    }

    public void Continue() => gameScene.HiddenMenuPause();

    public void ExitMainMenu() => Settings.OpenMainMenu();

    public void Save() => Saver.SaveGame(gameScene);
}
