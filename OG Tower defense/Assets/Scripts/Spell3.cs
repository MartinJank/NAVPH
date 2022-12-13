using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell3 : MonoBehaviour
{
    [SerializeField] private float delay;
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            foreach (GameObject item in Enemies.enemies)
            {
                Enemy enemy = item.GetComponent<Enemy>();
                enemy.movementSpeed /= 2f;
                // Debug.Log(enemy.movementSpeed);
            }
            StartCoroutine(SlowDownEnemy(delay));
        }

    }

    IEnumerator SlowDownEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject item in Enemies.enemies)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.movementSpeed *= 2f;
        }
    }
}
