using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public MoneyManager moneyManager;
    public GameObject basicTowerPrefab;
    public int basicTowerCost;

    public GameObject mediumTowerPrefab;
    public int mediumTowerCost;

    public int GetTowerCost(GameObject towerPrefab)
    {
        int cost = 0;
        GameObject findTower = Instantiate(towerPrefab);
        Tower dummyTower = findTower.GetComponent<Tower>();
        if (dummyTower != null) {
            cost = dummyTower.GetCost();
        }
        return cost;
    }

    public bool CanBuyTower(GameObject towerPrefab)
    {
        int cost = GetTowerCost(towerPrefab);

        bool canBuy = false;
        if (moneyManager.GetPlayerCurrentMoney() >= cost)
        {
            canBuy = true;
        }
        return canBuy;
    }

    public void BuyTower(GameObject towerPrefab)
    {
        moneyManager.RemoveMoney(GetTowerCost(towerPrefab));
    }


}
