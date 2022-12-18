using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    public static LevelCounter control;

    public int level;
    public int towerCoins;
    public int basicTowerMaxLevel;
    public int mediumTowerMaxLevel;
    public int fastTowerMaxLevel;

    private void Awake()
    {
        if (control == null)
        {
            control = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
}
