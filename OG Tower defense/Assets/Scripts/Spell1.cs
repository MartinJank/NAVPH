using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell1 : MonoBehaviour
{
    public MoneyManager moneyManager;
    [SerializeField] private int cost;
    [SerializeField] public GameObject blood; 
    void Update()
    {
        if (Input.GetKeyDown("z") && moneyManager.currentPlayerMoney >= cost)
        {
            moneyManager.RemoveMoney(cost);
            GameObject[] enemyList = ClosestBasedOnTiles();
            Enemies.enemies.Remove(enemyList[0]);
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(enemyList[0].gameObject);
        }
    }

    private GameObject[] ClosestBasedOnTiles()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        return gos;
    }

    public void Cast() {
        if (moneyManager.currentPlayerMoney >= cost)
        {
            moneyManager.RemoveMoney(cost);
            GameObject[] enemyList = ClosestBasedOnTiles();
            Enemies.enemies.Remove(enemyList[0]);
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(enemyList[0].gameObject);
        }
    }

}
