using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static List<GameObject> enemies;
    [SerializeField] private float enemyHealth; 
    [SerializeField] public float movementSpeed; 
    private int killReward = 25; // The amount of money the player gets when this enemy is killed 
    private int damage = 1; // The amount of damage the enemy does when it reaches the end 
    private GameObject targetTile; 
    private GameObject term;
    public MoneyManager moneyManager;
    public MapGenerator mapGenerator;
    public RoundController roundController;
    private void Awake() {
        moneyManager = GameObject.FindObjectsOfType<MoneyManager>()[0];
        mapGenerator = GameObject.FindObjectsOfType<MapGenerator>()[0];
        roundController = GameObject.FindObjectsOfType<RoundController>()[0];
        Enemies.enemies.Add(gameObject);
    }

    private void Start()
    {
        initializeEnemy();
        tag = "Enemy";
    }

    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Tower>() != null) {
            collision.gameObject.GetComponent<Tower>().addToEnemiesInRange(this.gameObject);
        }
        // enemiesInRange.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Tower>() != null) {
            collision.gameObject.GetComponent<Tower>().removeEnemiesInRange();
        }
    }

    public void takeDemage(float amount) {
        enemyHealth -= amount;
        if (enemyHealth <= 0) {
            moneyManager.AddMoney(killReward);
            die();
        }
    }

    private void die() {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed*Time.deltaTime);
    }

    private void checkPosition()
    {
        if (targetTile != null && targetTile != MapGenerator.endTile) {
            float distance  = (transform.position - targetTile.transform.position).magnitude;
            if (distance < 0.001f) {
                int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);
                targetTile = MapGenerator.pathTiles[currentIndex + 1];
                // name = targetTile.name;
            }
        } else if (transform.position == MapGenerator.endTile.transform.position) {
            mapGenerator.damageCastle(damage);
            if (mapGenerator.getCastleHealth() <= 0) {
                roundController.notOver = false;
            }
            die();
        }
    }

    private void Update()
    {
        checkPosition();
        moveEnemy();
    }
}
