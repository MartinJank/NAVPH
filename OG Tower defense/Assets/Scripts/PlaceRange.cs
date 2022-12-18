using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRange : MonoBehaviour
{
    public ShopManager shopManager;
    public UIText uiText;
    public GameObject basicTowerObject;
    private GameObject currentTowerPlacing;

    private GameObject dummyPlacement;
    private Tile hoverTile;
    public Camera cam;
    public LayerMask mask;
    public LayerMask towerMask;
    public bool isBuilding = false;

    public void Start() {
     }
    public Vector2 getMousePosition() {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void getCurrentHoverTile() {
         Vector2 mousePosition = getMousePosition();

         RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, mask, -100, 100);

         if (hit.collider != null){
            
            hoverTile = hit.collider.gameObject.GetComponent<Tile>();
         } else {
            hoverTile = null;
         }
    }
    public void PlaceBuilding(){
         if (hoverTile != null) {
            if (shopManager.CanBuyTower(currentTowerPlacing) == true) {
                GameObject newTowerObject = Instantiate(currentTowerPlacing);
                newTowerObject.layer = LayerMask.NameToLayer("Tower");
                newTowerObject.transform.position = hoverTile.transform.position;
                hoverTile.towerOccupied = newTowerObject;
                shopManager.BuyTower(currentTowerPlacing);
                endBuilding();
                Destroy(newTowerObject, 3f);
            } else {
                endBuilding();
                uiText.isError = true;
                uiText.errorMessage = "Not enough money";
                uiText.nextTime = Time.time + 2f; 
            }
         }
    }

    public void startBuilding(GameObject towerToBuild) {
        if (isBuilding == false) {
            hoverTile = null;
            isBuilding = true;
            currentTowerPlacing = towerToBuild;
            dummyPlacement = Instantiate(currentTowerPlacing);
            if (dummyPlacement.GetComponent<Tower>() != null) {
                Destroy(dummyPlacement.GetComponent<Tower>());
            }
            if (dummyPlacement.GetComponent<BarelRotation>() != null) {
                Destroy(dummyPlacement.GetComponent<BarelRotation>());
            }
        }
    }

    public void endBuilding() {
        isBuilding = false;
        hoverTile = null;
        if (dummyPlacement != null) {
            Destroy(dummyPlacement);
        }
    }

    public void Update() {
        if (isBuilding) {
            if (dummyPlacement != null) {
                getCurrentHoverTile();
                if (hoverTile != null) {
                    dummyPlacement.transform.position = hoverTile.transform.position;
                }
            }
            if (Input.GetButtonUp("Fire1") && hoverTile != null) {
                PlaceBuilding();
            }
            if (Input.GetButtonUp("Fire2")) {
                endBuilding();
            }
        } 
    }
}
