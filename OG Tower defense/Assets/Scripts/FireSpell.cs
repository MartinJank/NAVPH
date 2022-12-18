using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Tower
{
    protected override void shoot()
    {
        foreach (GameObject enemy in enemiesInRange.ToArray()) {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.takeDemage(damage);
        }
    }
}
