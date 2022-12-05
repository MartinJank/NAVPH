using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject MapTile;
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private int numOfCurves;
    [SerializeField] private int castleHealth;

    public static List<GameObject> mapTiles = new List<GameObject>();
    public static List<GameObject> pathTiles = new List<GameObject>();

    public static GameObject startTile;
    public static GameObject endTile;

    private bool reachedX = false;
    private bool reachedY = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;
    public Color pathColor;
    public Color startColor;
    public Color endColor;

    private void Start()
    {
        generateMap();
    }

    public int getCastleHealth() {
        return castleHealth;
    }

    public void damageCastle(int damage) {
        castleHealth -= damage;
    }

    private List<GameObject> getTopEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        for (int i = mapWidth * (mapHeight - 1); i < mapWidth * mapHeight; ++i)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private List<GameObject> getBottomEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        for (int i = 0; i < mapWidth; ++i)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private List<GameObject> getMiddleTiles()
    {
        List<GameObject> middleTiles = new List<GameObject>();

        int offset = mapHeight / (numOfCurves + 1);
        int moveWithOffset = offset;
        for (int i = 0; i < numOfCurves; ++i)
        {
            List<GameObject> tileRow = new List<GameObject>();
            for (int j = (mapWidth * (mapHeight - 1)) - moveWithOffset * mapWidth; j < (mapWidth * mapHeight) - moveWithOffset * mapWidth; ++j)
            {
                tileRow.Add(mapTiles[j]);
            }
            middleTiles.Add(tileRow[Random.Range(0, mapWidth)]);
            moveWithOffset += offset;
        }

        return middleTiles;
    }

    private void moveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }

    private void moveLeft()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - 1;
        currentTile = mapTiles[nextIndex];
    }

    private void moveRight()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex + 1;
        currentTile = mapTiles[nextIndex];
    }

    private void generateMap()
    {
        for (int y = 0; y < mapHeight; ++y)
        {
            for (int x = 0; x < mapWidth; ++x)
            {
                GameObject newTile = Instantiate(MapTile);

                mapTiles.Add(newTile);

                newTile.transform.position = new Vector2(x, y);
            }
        }


        startTile = getTopEdgeTiles()[Random.Range(0, mapWidth)]; 
        endTile = getBottomEdgeTiles()[Random.Range(0, mapWidth)];

        List<GameObject> middleTiles = getMiddleTiles();
        middleTiles.Add(endTile);

        currentTile = startTile;

        foreach (GameObject tile in middleTiles)
        {
            while (!reachedX)
            {
                if (currentTile.transform.position.x > tile.transform.position.x)
                {
                    moveLeft();
                }
                else if (currentTile.transform.position.x < tile.transform.position.x)
                {
                    moveRight();
                }
                else
                {
                    reachedX = true;
                }
            }

            while (!reachedY)
            {
                if (currentTile.transform.position.y > tile.transform.position.y)
                {
                    moveDown();
                }
                else
                {
                    reachedY = true;
                }
            }
            reachedX = false;
            reachedY = false;
        }

        pathTiles.Add(endTile);

        foreach (GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = pathColor;
        }

        startTile.GetComponent<SpriteRenderer>().color = startColor;
        endTile.GetComponent<SpriteRenderer>().color = endColor;
    }
}



