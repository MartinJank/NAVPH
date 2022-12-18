using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell3 : MonoBehaviour
{
    public MoneyManager moneyManager;
    [SerializeField] private int cost;
    [SerializeField] private float delay;
    
    IEnumerator SlowDownEnemy(float delay, List<GameObject> slowEnemies)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject item in slowEnemies)
        {
            if (item != null){
                Enemy enemy = item.GetComponent<Enemy>();
                if (enemy != null) {
                    enemy.movementSpeed *= 2f;
                }
            }
        }
    }

    public void Cast()
    {
        if (moneyManager.currentPlayerMoney >= cost)
        {
            List<GameObject> slowEnemies = new List<GameObject>();
            moneyManager.RemoveMoney(cost);
            foreach (GameObject item in Enemies.enemies)
            {
                Enemy enemy = item.GetComponent<Enemy>();
                enemy.movementSpeed /= 2f;
                slowEnemies.Add(item);
                // Debug.Log(enemy.movementSpeed);
            }
            StartCoroutine(SlowDownEnemy(delay, slowEnemies));
        }

    }
}
