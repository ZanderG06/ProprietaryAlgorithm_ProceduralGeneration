using UnityEngine;

public class GameManager : MonoBehaviour
{
    //X & Z would be theoretically be the same number, but seperating helps with my organization
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] int maxBuildings;
    [SerializeField] GameObject buildingPrefab;

    GameObject[] buildings;

    private void Start()
    {
        buildings = new GameObject[maxBuildings];

        for (int i = 0; i < maxBuildings; i++)
        {
            GameObject clone = Instantiate(buildingPrefab, new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)), Quaternion.identity);
            clone.name = $"Building {i}"; //Debugging purposes only
            buildings[i] = clone;
        }
    }
}