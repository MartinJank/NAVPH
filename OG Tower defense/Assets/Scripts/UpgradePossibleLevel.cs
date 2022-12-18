using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class UpgradePossibleLevel : MonoBehaviour
{
    [SerializeField] private int cost;
    public GameObject towerPrefab;

    [SerializeField] private TextMeshProUGUI towerCoins;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI statsLabelDamage;
    [SerializeField] private TextMeshProUGUI statsLabelRange;
    [SerializeField] private TextMeshProUGUI statsLabelAttackSpeed;
    [SerializeField] private TextMeshProUGUI upgradeLabelDamage;
    [SerializeField] private TextMeshProUGUI upgradeLabelRange;
    [SerializeField] private TextMeshProUGUI upgradeLabelAttackSpeed;

    string showTowerName;
    string showTowerLevel;
    string showTowerCost;

    string showTowerDamage;
    string showTowerRange;
    string showTowerAttackSpeed;
    void Start()
    {
        towerCoins.text = "" + LevelCounter.control.towerCoins + " towercoins";

        // LevelCounter.control.towerCoins -= cost;
        Tower tower = towerPrefab.GetComponent<Tower>();
        // tower.possibleLevel++;

        showTowerName = "" + tower.name;



        if (tower.name == "BasicTower")
        {
            showTowerLevel = "" + LevelCounter.control.basicTowerMaxLevel;
        }
        else if (tower.name == "MediumTower")
        {
            showTowerLevel = "" + LevelCounter.control.mediumTowerMaxLevel;
        }
        else if (tower.name == "FastTower")
        {
            showTowerLevel = "" + LevelCounter.control.fastTowerMaxLevel;
        }
        showTowerCost = "" + tower.costUpgrade;

        showTowerDamage = "" + tower.damage.ToString("0.00");
        showTowerRange = "" + tower.range.ToString("0.00");
        showTowerAttackSpeed = "" + tower.attackSpeed.ToString("0.00");

        upgradeLabelDamage.text = "+ 10%";
        upgradeLabelRange.text = "+ 5%";
        upgradeLabelAttackSpeed.text = "- 3%";

        towerName.text = showTowerName;
        levelText.text = showTowerLevel;
        costText.text = "" + cost;

        int iter;
        int.TryParse(levelText.text, out iter);
        Debug.Log("heeeh  " + iter);

        // statsLabelDamage.text = showTowerDamage;
        // statsLabelRange.text = showTowerRange;
        // statsLabelAttackSpeed.text = showTowerAttackSpeed;

        for (int i = 1; i < iter; i++)
        {
            updateMaxLevel();
        }

    }

    public void upgrade()
    {

        if (LevelCounter.control.towerCoins - cost >= 0)
        {
            LevelCounter.control.towerCoins -= cost;
            Tower tower = towerPrefab.GetComponent<Tower>();
            // tower.possibleLevel++;

            if (tower.name == "BasicTower")
            {
                showTowerLevel = "" + ++LevelCounter.control.basicTowerMaxLevel;
                // LevelCounter.control.basicTowerMaxLevel++;
            }
            else if (tower.name == "MediumTower")
            {
                showTowerLevel = "" + ++LevelCounter.control.mediumTowerMaxLevel;
                // LevelCounter.control.mediumTowerMaxLevel++;
            }
            else if (tower.name == "FastTower")
            {
                showTowerLevel = "" + ++LevelCounter.control.fastTowerMaxLevel;
                // LevelCounter.control.fastTowerMaxLevel++;
            }

            updateMaxLevel();

            towerName.text = tower.GetComponent<Tower>().name;
            levelText.text = "" + showTowerLevel;
            costText.text = "" + cost;

            upgradeLabelDamage.text = "+ 10%";
            upgradeLabelRange.text = "+ 5%";
            upgradeLabelAttackSpeed.text = "- 3%";

            towerCoins.text = "" + LevelCounter.control.towerCoins + " towercoins";
        }
    }

    private void updateMaxLevel()
    {
        float temp;
        float result;

        float.TryParse(statsLabelDamage.text, out temp);
        result = temp + 0.1f * temp;
        statsLabelDamage.text = "" + result.ToString("0.00");
        Debug.Log(temp + 0.1f * temp);

        float.TryParse(statsLabelRange.text, out temp);
        result = temp + 0.05f * temp;
        statsLabelRange.text = "" + result.ToString("0.00");
        Debug.Log(temp + 0.05f * temp);

        float.TryParse(statsLabelAttackSpeed.text, out temp);
        result = temp + (-0.03f) * temp;
        statsLabelAttackSpeed.text = "" + result.ToString("0.00");
        Debug.Log(temp - 0.03f * temp);

    }
}
