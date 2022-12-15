using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int currentPlayerMoney;
    public int starterMoney;

    public void Start() {
        currentPlayerMoney = starterMoney;
    }
    public int GetPlayerCurrentMoney(){
        return currentPlayerMoney;
    }

    public void AddMoney(int amount){
        currentPlayerMoney += amount;
    }

    public void RemoveMoney(int amount){
        currentPlayerMoney -= amount;
    }
}
