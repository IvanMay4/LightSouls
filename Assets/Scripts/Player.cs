using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    [NonSerialized] public int level;
    public Dictionary<string, int> specifications;
    int maxHP;
    int currentHP;
    int maxST;
    int currentST;
    float maxWeightEquipment;
    float currentWeightEquipment;

    public float moveSpeed;
    public float jumpSpeed;
    public float rotationSpeed;
    public int maxJumps;
    int currentJumps;

    [SerializeField] TMP_Text textVigor;
    [SerializeField] TMP_Text textEndurance;
    [SerializeField] TMP_Text textVitality;
    [SerializeField] TMP_Text textStrength;
    [SerializeField] TMP_Text textDexterity;
    [SerializeField] TMP_Text textLuck;
    [SerializeField] TMP_Text textHP;
    [SerializeField] TMP_Text textST;
    [SerializeField] TMP_Text textWeightEquipment;
    [SerializeField] TMP_Text textLevel;
    [SerializeField] Scrollbar scrollbarHP;
    [SerializeField] Scrollbar scrollbarST;
    new Rigidbody rigidbody;
    [NonSerialized] public GameScene gameScene;
    Vector3 move;
    [SerializeField] new Camera camera;

    [NonSerialized] public static Player instance;

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

    private void SetHP(int value) => currentHP = Math.Min(Math.Max(currentHP + value, 0), maxHP);

    private void SetST(int value) => currentST = Math.Min(Math.Max(currentST + value, 0), maxST);

    public void NewHP(int value) => currentHP = value;

    public void NewMaxHP(int value) => maxHP = value;

    public void NewST(int value) => currentST = value;

    public void NewMaxST(int value) => maxST = value;

    public void GetDamage(int valueDamage) => SetHP(-valueDamage);

    public void GetHeal(int valueHeal) => SetHP(valueHeal);

    public int GetCurrentJumps() => currentJumps;

    public void SetCurrentJumps(int countJumps) => currentJumps = countJumps;

    public static void IndependentAction(){
        if (Input.GetKeyDown(KeyCode.Escape)) SetVisibleMenuPause();
        if (Input.GetKeyDown(KeyCode.O)) SetVisibleMenuStatus();
        if (Input.GetKeyDown(KeyCode.I)) SetVisibleInventory();
    }

    public static void SetVisibleMenuPause(){
        if (!MenuPause.instance.gameObject.activeSelf && (MenuStatus.instance.gameObject.activeSelf || Inventory.instance.gameObject.activeSelf)) return;
        MenuPause.instance.gameObject.SetActive(!MenuPause.instance.gameObject.activeSelf);
    }
    public static void SetVisibleMenuStatus(){
        if (!MenuStatus.instance.gameObject.activeSelf && (MenuPause.instance.gameObject.activeSelf || Inventory.instance.gameObject.activeSelf)) return;
        MenuStatus.instance.gameObject.SetActive(!MenuStatus.instance.gameObject.activeSelf);
    }
    public static void SetVisibleInventory(){
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
        return;
        level++;
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
        scrollbarST.size = currentST * 1f / maxST;
    }

    public static string GetNameSpecification(int index){
        switch (index){
            case 0: return "vigor";
            case 1: return "endurance";
            case 2: return "vitality";
            case 3: return "strength";
            case 4: return "dexterity";
            case 5: return "luck";
            default: return "";
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground"))
            currentJumps = maxJumps;
    }

    public static void Continue() => MenuPause.instance.gameObject.SetActive(false);

    public static void ExitMainMenu() => Settings.OpenMainMenu();

    public void Save() => Saver.SaveGame(gameScene);
}
