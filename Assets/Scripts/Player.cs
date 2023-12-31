using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    public int level;
    private Dictionary<string, int> specifications;
    private int maxHP;
    private int currentHP;
    private int maxST;
    private int currentST;
    private int maxXP;
    private int currentXP;
    private float maxWeightEquipment;
    private float currentWeightEquipment;

    public float moveSpeed;
    public float jumpSpeed;
    public float rotationSpeed;
    public int maxJumps;
    private int currentJumps;

    [SerializeField] private TMP_Text textVigor;
    [SerializeField] private TMP_Text textEndurance;
    [SerializeField] private TMP_Text textVitality;
    [SerializeField] private TMP_Text textStrength;
    [SerializeField] private TMP_Text textDexterity;
    [SerializeField] private TMP_Text textLuck;
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private TMP_Text textST;
    [SerializeField] private TMP_Text textWeightEquipment;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private Scrollbar scrollbarHP;
    private new Rigidbody rigidbody;
    public GameScene gameScene;
    private Vector3 move;
    [SerializeField] private new Camera camera;

    public static Player instance;

    private void Awake(){
        instance = this;
        InitializeSpecifications();
        InitializeMovement();
        rigidbody = GetComponent<Rigidbody>();
        gameScene = this.AddComponent<GameScene>();
    }

    private void InitializeSpecifications(){
        level = 1;
        specifications = new Dictionary<string, int>();
        specifications["vigor"] = 10;
        specifications["endurance"] = 10;
        specifications["vitality"] = 10;
        specifications["strength"] = 10;
        specifications["dexterity"] = 10;
        specifications["luck"] = 10;
        maxHP = 100;
        maxST = 100;
        maxXP = 10;
        maxWeightEquipment = 22.2f;
        currentWeightEquipment = 0;
        SetHP(maxHP);
        SetST(maxST);
    }

    private void InitializeMovement(){
        moveSpeed = 4f;
        jumpSpeed = 4f;
        rotationSpeed = 10f;
        maxJumps = 1;
        currentJumps = maxJumps;
    }

    public int GetHP() => currentHP;

    public int GetMaxHP() => maxHP;

    public int GetST() => currentST;

    public int GetMaxST() => maxST;

    public int GetXP() => currentXP;

    public int GetMaxXP() => maxXP;

    private void SetHP(int value) => currentHP = Math.Min(Math.Max(currentHP + value, 0), maxHP);

    private void SetST(int value) => currentST = Math.Min(Math.Max(currentST + value, 0), maxST);

    private void SetXP(int value) => currentXP += value;

    public void NewHP(int value) => currentHP = value;

    public void NewMaxHP(int value) => maxHP = value;

    public void NewST(int value) => currentST = value;

    public void NewMaxST(int value) => maxST = value;

    public void NewXP(int value) => currentXP = value;

    public void NewMaxXP(int value) => maxXP = value;

    public void GetDamage(int valueDamage) => SetHP(-valueDamage);

    public void GetHeal(int valueHeal) => SetHP(valueHeal);

    public void GetExperience(int valueExperience) => SetXP(valueExperience);

    public int GetCurrentJumps() => currentJumps;

    public void SetCurrentJumps(int countJumps) => currentJumps = countJumps;

    public void IndependentAction(){
        if (Input.GetKeyDown(KeyCode.Escape)) SetVisibleMenuPause();
        if (Input.GetKeyDown(KeyCode.O)) SetVisibleMenuStatus();
        if (Input.GetKeyDown(KeyCode.I)) SetVisibleInventory();
    }

    public void SetVisibleMenuPause(){
        if (!MenuPause.instance.gameObject.activeSelf && (MenuStatus.instance.gameObject.activeSelf || Inventory.instance.gameObject.activeSelf)) return;
        MenuPause.instance.gameObject.SetActive(!MenuPause.instance.gameObject.activeSelf);
    }
    public void SetVisibleMenuStatus(){
        if (!MenuStatus.instance.gameObject.activeSelf && (MenuPause.instance.gameObject.activeSelf || Inventory.instance.gameObject.activeSelf)) return;
        MenuStatus.instance.gameObject.SetActive(!MenuStatus.instance.gameObject.activeSelf);
    }
    public void SetVisibleInventory(){
        if (!Inventory.instance.gameObject.activeSelf && (MenuPause.instance.gameObject.activeSelf || MenuStatus.instance.gameObject.activeSelf)) return;
        Inventory.instance.gameObject.SetActive(!Inventory.instance.gameObject.activeSelf);
    }

    private void Update(){
        IndependentAction();
        UseAbility();
        LevelUp();
        Display();
    }

    public void FixedUpdate(){
        Move();
        Jump();
        Rotation();
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

    private void UseAbility(){}

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
        textVigor.text = $"Жизненная сила: {specifications["vigor"]}";
        textEndurance.text = $"Стойкость: {specifications["endurance"]}";
        textVitality.text = $"Физическая мощь: {specifications["vitality"]}";
        textStrength.text = $"Сила: {specifications["strength"]}";
        textDexterity.text = $"Ловкость: {specifications["dexterity"]}";
        textLuck.text = $"Удача: {specifications["luck"]}";
        textHP.text = $"Здоровье: {Convert.ToString(currentHP)}/{Convert.ToString(maxHP)}";
        textST.text = $"Выносливость: {Convert.ToString(maxST)}";
        textWeightEquipment.text = $"Вес снаряжения: {Convert.ToString(currentWeightEquipment)}/{Convert.ToString(maxWeightEquipment)}";
        textLevel.text = $"Уровень: {level}";
        scrollbarHP.size = currentHP * 1f / maxHP;
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground"))
            currentJumps = maxJumps;
    }

    public void Continue() => MenuPause.instance.gameObject.SetActive(false);

    public void ExitMainMenu() => Settings.OpenMainMenu();

    public void Save() => Saver.SaveGame(gameScene);
}
