using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;

    public GameObject currentTarget;

    private float nextTimeShoot;
    // Start is called before the first frame update
    void Start()
    {
        nextTimeShoot = Time.time;
    }

    private void updateNearestEnemy() {
        GameObject currentNearestEnemy = null;
        float distance = Mathf.Infinity;
        foreach(GameObject enemy in Enemies.enemies) {
            if (enemy != null) {
                float _distance = (transform.position - enemy.transform.position).magnitude;
                if (_distance < distance) {
                    distance = _distance;
                    currentNearestEnemy = enemy;
                }
            }
        }

        if (distance <= range) {
            currentTarget = currentNearestEnemy;
        } else {
            currentTarget = null;
        }
    }

    protected virtual void shoot() {
        Enemy enemyScript = currentTarget.GetComponent<Enemy>();
        enemyScript.takeDemage(damage);
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
