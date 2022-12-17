using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private GameObject timeFasterButton;
    [SerializeField] private GameObject towerMenu;
    public bool isError;
    public float nextTime;
    public string errorMessage;
    public MoneyManager moneyManager;
    public MapGenerator mapGenerator;
    public bool is2Time = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        moneyText.text = "" + moneyManager.GetPlayerCurrentMoney();
        errorText.text = "";
        isError = false;
    }

    public void MakeFaster() {
        if (!is2Time) {
            is2Time = true;
            Time.timeScale = 10f;
            timeFasterButton.GetComponent<Image>().color = new Color(150,255,0);
        } else {
            is2Time = false;
            Time.timeScale = 1f;
            timeFasterButton.GetComponent<Image>().color = new Color(211,255,148);
        }
    }

    public void UpgradeTower(Tower tower) {
        tower.updateTower(10, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (moneyText != null) {
            moneyText.text = "" + moneyManager.GetPlayerCurrentMoney();
        }
        healthText.text = mapGenerator.getCastleHealth().ToString();
        if (errorText != null) {
            if (!isError && (Time.time >= nextTime)) {
                nextTime = Mathf.Infinity;
                errorText.text = "";
                isError = false;
            } else if (isError) {
                isError = false;
                errorText.text = errorMessage;
            } 
        }
    }
}
