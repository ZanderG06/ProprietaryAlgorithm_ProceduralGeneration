using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //X & Z would be theoretically be the same number, but seperating helps with my organization
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] float collisionDistance;
    [SerializeField] int maxBuildings;
    [SerializeField] GameObject buildingPrefab;

    GameObject[] buildings;

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        buildings = new GameObject[maxBuildings];

        for (int i = 0; i < maxBuildings; i++)
        {
            GameObject clone = Instantiate(buildingPrefab, new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)), Quaternion.identity);
            clone.name = $"Building {i}"; //Debugging purposes only
            buildings[i] = clone;
        }

        CheckForCollision();
    }

    private void CheckForCollision()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] == null) continue;

            Transform thisBuilding = buildings[i].transform;

            //Starts j at i + 1 to avoid checking the same pair twice, preventing buildings from destroying each other
            for (int j = i + 1; j < buildings.Length; j++) 
            {
                if (i == j) continue;
                if (buildings[j] == null) continue;

                Transform buildingToCheck = buildings[j].transform;

                if (Vector3.Distance(thisBuilding.position, buildingToCheck.position) < collisionDistance)
                {
                    Debug.Log($"Building {i} & Building {j} are colliding!");
                    Destroy(buildings[j]);
                }
            }
        }
    }

    public void ClearCity(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            for (int i = 0; i < buildings.Length; i++)
            {
                if (buildings[i] == null) continue;
                Destroy(buildings[i]);
            }
            Refresh();
        }
    }
}