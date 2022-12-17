using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{

    [SerializeField] private string name;
    [SerializeField] private float range;
    [SerializeField] public float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int cost;
    [SerializeField] private int costUpgrade;
    [SerializeField] private int level;
    [SerializeField] private Transform RangeCircle;
    [SerializeField] private GameObject towerMenu;

    // UI elements
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI statsLabelDamage;
    [SerializeField] private TextMeshProUGUI statsLabelRange;
    [SerializeField] private TextMeshProUGUI statsLabelAttackSpeed;
    [SerializeField] private TextMeshProUGUI upgradeLabelDamage;
    [SerializeField] private TextMeshProUGUI upgradeLabelRange;
    [SerializeField] private TextMeshProUGUI upgradeLabelAttackSpeed;


    [SerializeField] private CircleCollider2D CircleCollider2D;
    Camera cam;

    public GameObject currentTarget = null;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private float nextTimeShoot;
    
    public void HideRange() {
        RangeCircle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    public void addToEnemiesInRange(GameObject enemy) {
        enemiesInRange.Add(enemy);
    }

    public void removeEnemiesInRange() {
        if (enemiesInRange.Count > 0) {
            enemiesInRange.RemoveAt(0);
        }
    }
    private void Awake() {
        RangeCircle.localScale = new Vector3(range*2, range*2, range*2);
        CircleCollider2D.radius = range;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        nextTimeShoot = Time.time;
    }

    private void updateNearestEnemy() {
        if (enemiesInRange.Count > 0) {
            if (enemiesInRange[0] == null) {
                enemiesInRange.RemoveAt(0);
            }
            currentTarget = enemiesInRange.FirstOrDefault();
        } else {
            currentTarget = null;
        }
    }

    protected virtual void shoot() {
        Enemy enemyScript = currentTarget.GetComponent<Enemy>();
        enemyScript.takeDemage(damage);
    }

    public int GetCost() { 
        return cost;
    }

     public void closeUpgradeMenu() {
        towerMenu.SetActive(false);
    }

    public void showUpgradeMenu() {
        towerMenu.SetActive(true);
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector3 uiPos = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        towerMenu.transform.position = uiPos;

        towerName.text = ""+name;
        levelText.text = ""+level;
        costText.text = ""+costUpgrade;;

        statsLabelDamage.text = ""+damage;
        statsLabelRange.text = ""+range;
        statsLabelAttackSpeed.text = ""+attackSpeed;

        upgradeLabelDamage.text = "+ 10";
        upgradeLabelRange.text = "+ 0.1";
        upgradeLabelAttackSpeed.text = "- 0.1";
    }

    public void updateTower(int newDamage, float newRange, float newAttackSpeed) {
        damage = damage + newRange;
        range = range + newRange;
        attackSpeed = attackSpeed + newAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        updateNearestEnemy();
         
        if (Time.time >= nextTimeShoot) {
            if (currentTarget != null) {
                shoot();
                nextTimeShoot = Time.time + attackSpeed;
            }
        }
    }
}
