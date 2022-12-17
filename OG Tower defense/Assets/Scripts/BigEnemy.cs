using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    // Start is called before the first frame update

    protected override void die(){
        for (int i = 0; i < 2; i++)
        {
            GameObject newEnemy = Instantiate(roundController.basicEnemy);
            newEnemy.transform.position = new Vector3(transform.position.x - i*0.25f, transform.position.y + i*0.25f, transform.position.z);
            newEnemy.GetComponent<Enemy>().setStartTile(targetTile);
            // newEnemy.GetComponent<Enemy>().targetTile = this.GetComponent<Enemy>().targetTile;
        }

        Enemies.enemies.Remove(this.gameObject);
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
