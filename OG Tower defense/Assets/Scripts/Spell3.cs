using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell3 : MonoBehaviour
{
    public MoneyManager moneyManager;
    [SerializeField] private int cost;
    [SerializeField] private float delay;
    private List<GameObject> slowEnemies = new List<GameObject>();
    void Update()
    {
        if (Input.GetKeyDown("c") && moneyManager.currentPlayerMoney >= cost)
        {
            moneyManager.RemoveMoney(cost);
            float originalSpeed = 0f;
            foreach (GameObject item in Enemies.enemies)
            {
                Enemy enemy = item.GetComponent<Enemy>();
                originalSpeed = enemy.movementSpeed;
                Debug.Log(enemy.movementSpeed);
                enemy.movementSpeed /= 2f;
                slowEnemies.Add(item);
                // Debug.Log(enemy.movementSpeed);
            }
            StartCoroutine(SlowDownEnemy(delay, originalSpeed));
        }

    }

    IEnumerator SlowDownEnemy(float delay, float originalSpeed)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject item in slowEnemies)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.movementSpeed *= 2f;
        }
    }
}
