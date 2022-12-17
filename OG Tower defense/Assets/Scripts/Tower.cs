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

    public MoneyManager moneyManager;
    public UIText uiText;

    public GameObject currentTarget = null;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private float nextTimeShoot;
    
    public void HideRange() {
        RangeCircle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    public void showRange() {
        RangeCircle.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
    }

    public void addToEnemiesInRange(GameObject enemy) {
        enemiesInRange.Add(enemy);
    }

    public void removeEnemiesInRange(GameObject enemy) {
        if (enemiesInRange.Count > 0) {
            enemiesInRange.Remove(enemy);
        }
    }

    private void UpdateRange() {
        RangeCircle.localScale = new Vector3(range*2, range*2, range*2);
        CircleCollider2D.radius = range;
    }
    private void Awake() {
        RangeCircle.localScale = new Vector3(range*2, range*2, range*2);
        CircleCollider2D.radius = range;

        moneyManager = GameObject.FindObjectsOfType<MoneyManager>()[0];
        uiText = GameObject.FindObjectsOfType<UIText>()[0];
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

        statsLabelDamage.text = ""+damage.ToString("0.00");
        statsLabelRange.text = ""+range.ToString("0.00");
        statsLabelAttackSpeed.text = ""+attackSpeed.ToString("0.00");

        upgradeLabelDamage.text = "+ 10%";
        upgradeLabelRange.text = "+ 5%";
        upgradeLabelAttackSpeed.text = "- 3%";
    }

    public void updateTower() {
        if (moneyManager.currentPlayerMoney >= costUpgrade) {
            moneyManager.RemoveMoney(costUpgrade);
            damage = damage + 0.1f*damage;
            range = range + 0.05f*range;
            attackSpeed = attackSpeed - 0.03f*attackSpeed;
            level += 1;

            UpdateRange();
        } else {
            uiText.isError = true;
            uiText.errorMessage = "Not enough money";
            uiText.nextTime = Time.time + 2f; 
        }
        
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
