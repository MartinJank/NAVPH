using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI errorText;
    public bool isError;
    public float nextTime;
    public string errorMessage;
    public MoneyManager moneyManager;
    // Start is called before the first frame update
    private void Start()
    {
        moneyText.text = "Money: " + moneyManager.GetPlayerCurrentMoney();
        errorText.text = "ffff";
        isError = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moneyText != null) {
            moneyText.text = "Money: " + moneyManager.GetPlayerCurrentMoney();
        }
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
