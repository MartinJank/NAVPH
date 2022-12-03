using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject basicTowerObject;

    private GameObject dummyPlacement;
    private GameObject hoverTile;
    public Camera cam;
    public LayerMask mask;
    public LayerMask towerMask;
    public bool isBuilding;

    public void Start() {
        startBuilding();
    }
    public Vector2 getMousePosition() {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void getCurrentHoverTile() {
         Vector2 mousePosition = getMousePosition();

         RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, mask, -100, 100);

         if (hit.collider != null){
            
            if (MapGenerator.mapTiles.Contains(hit.collider.gameObject)) {
                if (!MapGenerator.pathTiles.Contains(hit.collider.gameObject)){
                    hoverTile = hit.collider.gameObject;
                }
            }
         }
    }

    public bool CheckTower() {
        bool towerOnSlot = false;
        Vector2 mousePosition = getMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -100, 100);
        if (hit.collider != null) {
            towerOnSlot = true;
        }
        return towerOnSlot;
    }

    public void PlaceBuilding(){
         if (hoverTile != null) {
            if (!CheckTower()) {
                GameObject newTowerObject = Instantiate(basicTowerObject);
                newTowerObject.layer = LayerMask.NameToLayer("Tower");
                newTowerObject.transform.position = hoverTile.transform.position;

                endBuilding();
            }
         }
    }

    public void startBuilding() {
        isBuilding = true;
        dummyPlacement = Instantiate(basicTowerObject);
        if (dummyPlacement.GetComponent<Tower>() != null) {
            Destroy(dummyPlacement.GetComponent<Tower>());
        }
        if (dummyPlacement.GetComponent<BarelRotation>() != null) {
            Destroy(dummyPlacement.GetComponent<BarelRotation>());
        }
    }

    public void endBuilding() {
        isBuilding = false;

        if (dummyPlacement != null) {
            Destroy(dummyPlacement);
        }
    }

    public void Update() {
        if (isBuilding == true) {
            if (dummyPlacement != null) {
                getCurrentHoverTile();
                if (hoverTile != null) {
                    dummyPlacement.transform.position = hoverTile.transform.position;
                }
            }
            if (Input.GetButtonDown("Fire1")) {
                PlaceBuilding();
            }
        }
    }
}
