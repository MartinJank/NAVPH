using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            DamageAllEnemies();
        }
    }

    private void DamageAllEnemies()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject item in gos)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.takeDemage(50);
        }
    }


}
