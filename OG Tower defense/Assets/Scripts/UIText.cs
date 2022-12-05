using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI errorText;
    public bool isError;
    public float nextTime;
    public string errorMessage;
    public MoneyManager moneyManager;
    public MapGenerator mapGenerator;
    // Start is called before the first frame update
    private void Start()
    {
        moneyText.text = "" + moneyManager.GetPlayerCurrentMoney();
        errorText.text = "";
        isError = false;
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
