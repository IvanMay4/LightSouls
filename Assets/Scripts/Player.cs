using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    private int maxHP;
    private int currentHP;
    public int level;
    private int maxXP;
    private int currentXP;
    public float moveSpeed;
    public float jumpSpeed;
    public float rotationSpeed;
    public int maxJumps;
    private int currentJumps;
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private Scrollbar scrollbarHP;
    [SerializeField] private TMP_Text textXP;
    [SerializeField] private Scrollbar scrollbarXP;
    [SerializeField] private TMP_Text textLevel;
    private new Rigidbody rigidbody;
    public GameScene gameScene;
    private Vector3 move;
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject menuPause;

    private void Awake(){
        Initialize();
        rigidbody = GetComponent<Rigidbody>();
        this.AddComponent<GameScene>();
        gameScene = GetComponent<GameScene>();
        gameScene.menuPause = menuPause;
    }

    private void Initialize(){
        maxHP = 100;
        maxXP = 10;
        level = 1;
        moveSpeed = 4f;
        jumpSpeed = 4f;
        rotationSpeed = 10f;
        maxJumps = 1;
        SetHP(maxHP);
        currentJumps = maxJumps;
    }

    public int GetHP() => currentHP;

    public int GetMaxHP() => maxHP;

    public int GetXP() => currentXP;

    public int GetMaxXP() => maxXP;

    private void SetHP(int value) => currentHP = Math.Min(Math.Max(currentHP + value, 0), maxHP);

    private void SetXP(int value) => currentXP += value;

    public void NewHP(int value) => currentHP = value;

    public void NewMaxHP(int value) => maxHP = value;
    public void NewXP(int value) => currentXP = value;

    public void NewMaxXP(int value) => maxXP = value;

    public void GetDamage(int valueDamage) => SetHP(-valueDamage);

    public void GetHeal(int valueHeal) => SetHP(valueHeal);

    public void GetExperience(int valueExperience) => SetXP(valueExperience);

    public int GetCurrentJumps() => currentJumps;

    public void SetCurrentJumps(int countJumps) => currentJumps = countJumps;

    public void IndependentAction(){
        if (Input.GetKeyDown(KeyCode.Escape))
            gameScene.SetMenuPause();
    }

    public void Update(){
        IndependentAction();
        Move();
        Jump();
        Rotation();
        UseAbility();
        LevelUp();
        Display();
        rigidbody.velocity = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * move;
    }


    private void Move(){
        move = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rigidbody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
    }

    private void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) && currentJumps > 0){
            currentJumps--;
            move.y = (jumpSpeed - Physics.gravity.y);
        }
    }

    private void Rotation(){
        transform.rotation *= Quaternion.Euler(0, (Input.GetKey(KeyCode.Q)? -1: Input.GetKey(KeyCode.E)? 1: 0) * Time.deltaTime * rotationSpeed, 0);
    }

    private void UseAbility(){
        if (Input.GetKeyDown(KeyCode.H))
            GetHeal(10);
    }

    private void LevelUp(){
        if (currentXP < maxXP)
            return;
        level++;
        currentXP -= maxXP;
        maxXP += 5;
        GetExperience(0);
        NewMaxHP(maxHP + 10);
    }

    private void Display(){
        textHP.text = $"HP: {Convert.ToString(currentHP)}/{Convert.ToString(maxHP)}";
        scrollbarHP.size = currentHP * 1f / maxHP;
        textXP.text = $"XP: {Convert.ToString(currentXP)}/{Convert.ToString(maxXP)}";
        scrollbarXP.size = currentXP * 1f / maxXP;
        textLevel.text = $"Level: {level}";
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground"))
            currentJumps = maxJumps;
    }

    public void Continue() => gameScene.HiddenMenuPause();

    public void ExitMainMenu() => Settings.OpenMainMenu();

    public void Save() => Saver.SaveGame(gameScene);
}
