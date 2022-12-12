using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float range;
    [SerializeField] public float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int cost;
    [SerializeField] private Transform RangeCircle;

    public GameObject currentTarget;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private float nextTimeShoot;
    
    public void HideRange() {
        RangeCircle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    private void Awake() {
        RangeCircle.localScale = new Vector3(range/2, range/2, range/2);
    }
    // Start is called before the first frame update
    void Start()
    {
        nextTimeShoot = Time.time;
    }

    private void updateNearestEnemy() {
        if (enemiesInRange.Count > 0) {
            if (enemiesInRange[0] == null) {
                enemiesInRange.RemoveAt(0);
            }
            currentTarget = enemiesInRange[0];
        }
        // GameObject currentNearestEnemy = null;
        // float distance = Mathf.Infinity;
        // foreach(GameObject enemy in Enemies.enemies) {
        //     if (enemy != null) {
        //         float _distance = (transform.position - enemy.transform.position).sqrMagnitude;
        //         if (_distance < distance) {
        //             distance = _distance;
        //             currentNearestEnemy = enemy;
        //         }
        //     }
        // }

        // if (distance <= range) {
        //     currentTarget = currentNearestEnemy;
        // } else {
        //     currentTarget = null;
        // }
    }

    protected virtual void shoot() {
        Enemy enemyScript = currentTarget.GetComponent<Enemy>();
        enemyScript.takeDemage(damage);
    }

    public int GetCost() { 
        return cost;
    }

    // private void OnCollisionEnter2D(Collision2D collision) {
    //     Debug.Log("Enemies added to list");
    //     enemiesInRange.Add(collision.gameObject);
    // }

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
