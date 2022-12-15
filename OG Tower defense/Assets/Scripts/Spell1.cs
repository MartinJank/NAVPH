using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell1 : MonoBehaviour
{
    public MoneyManager moneyManager;
    [SerializeField] private int cost;
    void Update()
    {
        if (Input.GetKeyDown("z") && moneyManager.currentPlayerMoney >= cost)
        {
            moneyManager.RemoveMoney(cost);
            GameObject[] enemyList = ClosestBasedOnTiles();
            Enemies.enemies.Remove(enemyList[0]);
            Destroy(enemyList[0].gameObject);
        }
    }

    private GameObject[] ClosestBasedOnTiles()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        return gos;
    }

}
