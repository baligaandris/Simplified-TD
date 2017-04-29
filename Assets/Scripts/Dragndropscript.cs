using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Dragndropscript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private int towerPrice;
    public GameObject tower;
    GameObject hoverTower;
    public GameObject[] buildableAreas;
    private GameDataScript gameData;
    public GameObject[] allTowers;

    // Use this for initialization
    void Start()
    {
        towerPrice = tower.GetComponentInChildren<TowerShootsScript>().cost;
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
        hoverTower = Instantiate(tower);
        hoverTower.SetActive(false);
        hoverTower.GetComponentInChildren<SpriteRenderer>().enabled = true;
        RemoveFunctionFromPrefab();
    }

    void RemoveFunctionFromPrefab()
    {
        Component[] components = hoverTower.GetComponentsInChildren<TowerShootsScript>();
        foreach (Component component in components)
        {
            Destroy(component);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameData.usac >= towerPrice)
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, 50f);
            if (hits != null && hits.Length > 0)
                foreach (GameObject buildableArea in buildableAreas)
                    foreach (GameObject tower in allTowers)
                        if (hits != null)
                        {
                            MaybeShowHoverTower(hits);
                        }
        }
    }

    int GetTerrainColliderQuadIndex(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name.Equals("TerrainColliderQuad"))
                {
                    return i;
                }
        }

        return -1;
    }

    int GetBuildableAreaIndex(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name.Equals("BuildableArea"))
            {
                return i;
            }
        }
        return -1;
    }



    void MaybeShowHoverTower(RaycastHit[] hits)
    {
        int terrainBuildAreaQuadIndex = GetBuildableAreaIndex(hits);
        int terrainColliderQuadIndex = GetTerrainColliderQuadIndex(hits);
        if (terrainColliderQuadIndex != -1)
        if (terrainBuildAreaQuadIndex != -1)
        {
            hoverTower.transform.position = hits[terrainColliderQuadIndex].point;
            hoverTower.SetActive(true);
        }
        else
        {
            hoverTower.SetActive(false);
        }
    }

  

    public void OnEndDrag(PointerEventData eventData)
    {
        // If the prefab instance is active after dragging stopped, it means
        // it's in the arena so (for now), just drop it in.
        if (hoverTower.activeSelf)
        {
            Instantiate(tower, hoverTower.transform.position, Quaternion.identity);
            gameData.ChangeUsac(-towerPrice);
        }

        // Then set it to inactive ready for the next drag!
        hoverTower.SetActive(false);
    }
}