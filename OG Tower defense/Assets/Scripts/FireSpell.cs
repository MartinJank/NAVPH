using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Tower
{
    protected override void shoot()
    {
            GameObject max = enemiesInRange[0];
            foreach (GameObject enemy in enemiesInRange) {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.takeDemage(damage);
            }
    }
}
