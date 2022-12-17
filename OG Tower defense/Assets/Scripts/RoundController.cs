using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    private MapGenerator mapGenerator;
    public UIText uiText;
    public GameObject basicEnemy;
    public GameObject fastEnemy;
    public GameObject bigEnemy;
    public float timeWaves;
    public float timeBeforeRoundStarts;
    public float timeVariable;
    public bool notOver = true;
    public bool roundGoing;
    public bool intermission;
    public bool roundStarted;
    public int round;

    private void Start()
    {
        mapGenerator = GameObject.FindObjectsOfType<MapGenerator>()[0];
        roundGoing = false;
        intermission = false;
        roundStarted = true;
        timeVariable = Time.time + timeBeforeRoundStarts;
        round = 1;
    }

    private void SpawnEnemies()
    {
        StartCoroutine("ISpawnEnemies");
    }

    IEnumerator ISpawnEnemies()
    {
        for (int i = 0; i < round * 3; i++)
        {
            if (round > 3)
            {
                if (Random.Range(0, 2) == 0)
                {
                    GameObject newEnemy = Instantiate(fastEnemy, MapGenerator.startTile.transform.position, Quaternion.identity);
                }
                else
                {
                    GameObject newEnemy = Instantiate(basicEnemy, MapGenerator.startTile.transform.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject newEnemy = Instantiate(basicEnemy, MapGenerator.startTile.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (notOver)
        {
            if (roundStarted)
            {
                if (Time.time >= timeVariable)
                {
                    roundStarted = false;
                    roundGoing = true;

                    SpawnEnemies();
                    return;
                }
            }
            else if (intermission)
            {
                if (Time.time >= timeVariable)
                {
                    intermission = false;
                    roundGoing = true;
                    SpawnEnemies();
                }
            }
            else if (roundGoing)
            {
                if (Enemies.enemies.Count > 0)
                { // CHECK enemies

                }
                else
                {
                    intermission = true;
                    roundGoing = false;

                    timeVariable = Time.time + timeWaves;
                    round++;
                    if (round > 6 && Enemies.enemies.Count == 0)
                    {
                        LevelCounter.control.towerCoins += mapGenerator.castleHealth;
                        LevelCounter.control.level--;
                        if (LevelCounter.control.level < 0)
                        {
                            LevelCounter.control.level = 3;
                            SceneManager.LoadScene("WinGameScene");
                        }
                        else
                        {
                            SceneManager.LoadScene("WinLevelScene");
                        }

                    }
                }

            }
        }
        else
        {
            uiText.isError = true;
            uiText.errorMessage = "Castle destroyed!";
            notOver = false;
            SceneManager.LoadScene("MenuScene");
        }
    }
}
