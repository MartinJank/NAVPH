using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public MoneyManager moneyManager;
    public GameObject basicTowerPrefab;
    public int basicTowerCost;

    public int GetTowerCost(GameObject towerPrefab) {
        int cost = 0;
        if (towerPrefab == basicTowerPrefab) {
            cost = basicTowerCost;
        }
        return cost;
    }

    public bool CanBuyTower(GameObject towerPrefab) {
        int cost = GetTowerCost(towerPrefab);

        bool canBuy = false;
        if (moneyManager.GetPlayerCurrentMoney() >= cost) {
            canBuy = true;
        }
        return canBuy;
    }

    public void BuyTower(GameObject towerPrefab) {
        moneyManager.RemoveMoney(GetTowerCost(towerPrefab));
    }


}