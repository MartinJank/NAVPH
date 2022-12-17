using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePossibleLevel : MonoBehaviour
{
    [SerializeField] private int cost;
    public GameObject towerPrefab;
    public void upgrade() {
        LevelCounter.control.towerCoins -= cost;
        Tower tower = towerPrefab.GetComponent<Tower>();
        tower.possibleLevel++;
    }
}
