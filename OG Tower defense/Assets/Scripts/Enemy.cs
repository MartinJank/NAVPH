using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static List<GameObject> enemies;
    [SerializeField] private float enemyHealth; 
    [SerializeField] public float movementSpeed; 
    [SerializeField] public GameObject blood; 
    private int killReward = 15; // The amount of money the player gets when this enemy is killed 
    private int damage = 1; // The amount of damage the enemy does when it reaches the end 
    public GameObject targetTile; 
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

    public void setStartTile(GameObject target) {
        targetTile = target;
    }

    private void Start()
    {
        initializeEnemy();
        tag = "Enemy";
    }

    private void initializeEnemy()
    {
        if (targetTile == null) {
            targetTile = MapGenerator.startTile;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Tower>() != null) {
            collision.gameObject.GetComponent<Tower>().addToEnemiesInRange(this.gameObject);
        }
        // enemiesInRange.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Tower>() != null) {
            collision.gameObject.GetComponent<Tower>().removeEnemiesInRange(this.gameObject);
        }
    }

    public void takeDemage(float amount) {
        enemyHealth -= amount;
        if (enemyHealth <= 0) {
            moneyManager.AddMoney(killReward);
            die();
        }
    }

    protected virtual void die() {
        Enemies.enemies.Remove(gameObject);
        Instantiate(blood, transform.position, Quaternion.identity);
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
            }
        } else if (transform.position == MapGenerator.endTile.transform.position) {
            mapGenerator.damageCastle(damage);
            if (mapGenerator.getCastleHealth() <= 0) {
                roundController.notOver = false;
            }
            die();
        }
    }

    private void rotateAccordingToDirection()
    {
        // Vector3 relativePos = targetTile.transform.position - transform.position;

        // // the second argument, upwards, defaults to Vector3.up
        // Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        // transform.rotation = rotation;

        Vector3 moveDirection = targetTile.transform.position - transform.position; 
        if (moveDirection != Vector3.zero) 
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Update()
    {
        checkPosition();
        rotateAccordingToDirection();
        moveEnemy();
    }
}
