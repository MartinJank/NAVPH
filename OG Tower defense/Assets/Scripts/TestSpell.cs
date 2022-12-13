using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            GameObject[] enemyList = ClosestBasedOnTiles();
            Enemies.enemies.Remove(enemyList[0]);
            Destroy(enemyList[0].gameObject);
        }
    }

    private GameObject[] ClosestBasedOnTiles()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        return gos;
    }

}
