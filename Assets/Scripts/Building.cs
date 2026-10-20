using UnityEngine;

public class Building : MonoBehaviour
{
    float minWidth = .5f;
    float maxWidth = 2;
    float minHeight = .5f;
    float maxHeight = 20f;

    private void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        float width = Random.Range(minWidth, maxWidth);
        float height = Random.Range(minHeight, maxHeight);

        transform.localScale = new Vector3(width, height, width);
    }
}