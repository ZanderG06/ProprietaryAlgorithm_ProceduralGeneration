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
    [SerializeField] int maxRoads;
    [SerializeField] int maxBuildings;
    [SerializeField] GameObject roadPrefab;
    [SerializeField] GameObject buildingPrefab;

    bool[] isRoadRotated;
    GameObject[] roads;
    GameObject[] buildings;

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        isRoadRotated = new bool[maxRoads];
        roads = new GameObject[maxRoads];
        for(int i = 0; i < maxRoads; i++)
        {
            int[] roadRotations = { 0, 90 };
            int random = Random.Range(0, roadRotations.Length);

            switch (random)
            {
                case 0:
                    GameObject clone = Instantiate(roadPrefab, new Vector3(Random.Range(minX, maxX), 0, 0), Quaternion.Euler(0, 0, 0));
                    clone.name = $"Road {i}"; //Debugging purposes only
                    roads[i] = clone;
                    isRoadRotated[i] = false;
                    break;
                case 1:
                    GameObject clone2 = Instantiate(roadPrefab, new Vector3(0, 0, Random.Range(minZ, maxZ)), Quaternion.Euler(0, 90, 0));
                    clone2.name = $"Road {i}"; //Debugging purposes only
                    roads[i] = clone2;
                    isRoadRotated[i] = true;
                    break;
                default:
                    Debug.Log($"Switch Case Error: {random}");
                    break;
            }
        }
        
        buildings = new GameObject[maxBuildings];
        for (int i = 0; i < maxBuildings; i++)
        {
            GameObject clone = Instantiate(buildingPrefab, new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)), Quaternion.identity);
            clone.name = $"Building {i}"; //Debugging purposes only
            buildings[i] = clone;
        }
        CheckForBuildingCollision();
        CheckForRoadCollisions();
    }

    private void CheckForBuildingCollision()
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

    private void CheckForRoadCollisions()
    {
        for(int i = 0; i < roads.Length; i++)
        {
            Transform thisRoad = roads[i].transform;

            for(int j = 0; j < buildings.Length; j++)
            {
                if (buildings[j] == null) continue;

                Transform buildingToCheck = buildings[j].transform;

                //Using Mathf.Abs to get a positive value and measure the difference between buildings and roads
                if (isRoadRotated[i] == false)
                {
                    if (Mathf.Abs(thisRoad.position.x - buildingToCheck.position.x) < collisionDistance)
                    {
                        Debug.Log($"Road {i} & Building {j} are colliding!");
                        Destroy(buildings[j]);
                    }
                }
                else
                {
                    if (Mathf.Abs(thisRoad.position.z - buildingToCheck.position.z) < collisionDistance)
                    {
                        Debug.Log($"Road {i} & Building {j} are colliding!");
                        Destroy(buildings[j]);
                    }
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
            for (int i = 0; i < roads.Length; i++)
            {
                if (roads[i] == null) continue;
                Destroy(roads[i]);
            }
            Refresh();
        }
    }
}