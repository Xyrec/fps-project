using UnityEngine;

public class PlaneSpikes : MonoBehaviour
{
    [Header("Spike Settings")]
    [SerializeField] private int numberOfSpikes = 20;
    [SerializeField] private float minHeight = 1f;
    [SerializeField] private float maxHeight = 3f;
    [SerializeField] private Material spikeMaterial;

    void Start()
    {
        // Get the plane size (10x10)
        float planeWidth = 10f;
        float planeLength = 10f;

        // Create spikes
        for (int i = 0; i < numberOfSpikes; i++)
        {
            // Calculate random position within plane bounds
            float randomX = Random.Range(-planeWidth / 2f + 0.5f, planeWidth / 2f - 0.5f);
            float randomZ = Random.Range(-planeLength / 2f + 0.5f, planeLength / 2f - 0.5f);
            float randomHeight = Random.Range(minHeight, maxHeight);

            // Create cube
            GameObject spike = GameObject.CreatePrimitive(PrimitiveType.Cube);
            spike.transform.parent = transform;

            // Position and scale the spike
            spike.transform.localPosition = new Vector3(randomX, randomHeight / 2f, randomZ);
            spike.transform.localScale = new Vector3(1f, randomHeight, 1f);

            // Apply material if assigned
            if (spikeMaterial != null)
            {
                spike.GetComponent<MeshRenderer>().material = spikeMaterial;
            }

            // Name the spike for organization
            spike.name = $"Spike_{i}";
        }
    }
}