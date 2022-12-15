using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell2 : MonoBehaviour
{
    public MoneyManager moneyManager;
    [SerializeField] private int cost;
    [SerializeField] private GameObject lightning; 
    void Update()
    {
        if (Input.GetKeyDown("x") && moneyManager.currentPlayerMoney >= cost)
        {
            DamageAllEnemies();
            moneyManager.RemoveMoney(cost);
        }
    }

    private void DamageAllEnemies()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject item in gos)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            Instantiate(lightning, item.transform.position, Quaternion.identity);
            enemy.takeDemage(50);
        }
    }

    public void Cast()
    {
        if (moneyManager.currentPlayerMoney >= cost)
        {
            DamageAllEnemies();
            moneyManager.RemoveMoney(cost);
        }
    }

}
