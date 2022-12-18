using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class UpgradePossibleLevel : MonoBehaviour
{
    [SerializeField] private int cost;
    public GameObject towerPrefab;

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

        // LevelCounter.control.towerCoins -= cost;
        Tower tower = towerPrefab.GetComponent<Tower>();
        // tower.possibleLevel++;

        showTowerName = "" + tower.name;
        showTowerLevel = "" + tower.possibleLevel;
        showTowerCost = "" + tower.costUpgrade;

        showTowerDamage = "" + tower.damage.ToString("0.00");
        showTowerRange = "" + tower.range.ToString("0.00");
        showTowerAttackSpeed = "" + tower.attackSpeed.ToString("0.00");

        upgradeLabelDamage.text = "+ 10%";
        upgradeLabelRange.text = "+ 5%";
        upgradeLabelAttackSpeed.text = "- 3%";

        towerName.text = showTowerName;
        levelText.text = showTowerLevel;
        costText.text = showTowerCost;

        statsLabelDamage.text = showTowerDamage;
        statsLabelRange.text = showTowerRange;
        statsLabelAttackSpeed.text = showTowerAttackSpeed;
    }

    public void upgrade()
    {

        LevelCounter.control.towerCoins -= cost;
        Tower tower = towerPrefab.GetComponent<Tower>();
        tower.possibleLevel += 1;
        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(towerPrefab);
        
        updateMaxLevel();

        towerName.text = tower.GetComponent<Tower>().name;
        levelText.text = "" + tower.GetComponent<Tower>().possibleLevel;
        costText.text = "" + cost;

        upgradeLabelDamage.text = "+ 10%";
        upgradeLabelRange.text = "+ 5%";
        upgradeLabelAttackSpeed.text = "- 3%";
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
