using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public UIText uiText;
    public GameObject basicEnemy;
    public float timeWaves;
    public float timeBeforeRoundStarts;
    public float timeVariable;
    public bool notOver = true;
    public bool roundGoing;
    public bool intermission;
    public bool roundStarted;
    public int round;

    private void Start() {
        roundGoing = false;
        intermission = false;
        roundStarted = true;    
        timeVariable = Time.time + timeBeforeRoundStarts;
        round = 1;
    }

    private void SpawnEnemies() {
        StartCoroutine("ISpawnEnemies");
    }

    IEnumerator ISpawnEnemies() {
        for (int i = 0; i < round;i++) {
            GameObject newEnemy = Instantiate(basicEnemy, MapGenerator.startTile.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update() {
        if (notOver) {
            if (roundStarted) {
                if (Time.time >= timeVariable) {
                    roundStarted = false;
                    roundGoing = true;

                    SpawnEnemies();
                    return;                
                }
            } else if (intermission) {
                if (Time.time >= timeVariable) {
                    intermission = false;
                    roundGoing = true;
                    SpawnEnemies();
                }
            } else if (roundGoing) {
                if (Enemies.enemies.Count > 0) { // CHECK enemies

                } else {
                    intermission = true;
                    roundGoing = false;

                    timeVariable = Time.time + timeWaves;
                    round++;
                } 

            }
        } else {
            uiText.isError = true;
            uiText.errorMessage = "Castle destroyed!";
            notOver = false;
        }
    }
}
