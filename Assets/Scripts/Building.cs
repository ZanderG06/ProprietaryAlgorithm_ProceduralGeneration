using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    private void Start()
    {
        transform.localScale = new Vector3(1, Random.Range(minHeight, maxHeight), 1);
    }
}