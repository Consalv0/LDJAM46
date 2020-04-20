using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform followTarget;
    public float destructionDistance;
    public float spawnDistance;
    public float maxHeight;
    public float maxHeightDistance;
    public float minHeightDistance;
    public PlatformObject[] platforms;
    public PlatformObject groundPlatform;

    private float lastMaxDistace;
    private float lastMaxGourndDistance;
    private float lastMaxHeight;
    private float deltaChance;

    private GameObject[] groundObjects;
    private int currentGroundIndex = 0;
    private GameObject[] platformObjects;
    private int currentPlatformIndex = 0;

    private const int MaxGroundIndex = 5;
    private const int MaxPlatformIndex = 10;

    private GameObject SpawnPlatform(PlatformObject platform, float position, float height)
    {
        return Instantiate(platform.visuals, new Vector3(position, height, 0.0F), Quaternion.identity);
    }

    private void CalculateNextHeight()
    {
        Debug.Log(lastMaxHeight);
        lastMaxHeight += Random.Range(minHeightDistance, maxHeightDistance);
        Debug.Log(lastMaxHeight);
        if (lastMaxHeight > maxHeight)
        {
            lastMaxHeight -= Random.Range(minHeightDistance, maxHeightDistance);
        }
    }

    private void Update()
    {
        deltaChance = Mathf.Clamp(deltaChance, 0.25F, 1.0F);
        Vector3 position = (followTarget ? followTarget : transform).position;
        if (lastMaxDistace < position.x + spawnDistance)
        {
            if (Random.value > deltaChance)
            {
                deltaChance += 0.4F; 
                CalculateNextHeight();
                if (lastMaxHeight > platforms[0].dimensions.y)
                {
                    platformObjects[currentPlatformIndex++] = SpawnPlatform(platforms[0], lastMaxDistace + platforms[0].dimensions.x * 0.5F, lastMaxHeight);
                    if (currentPlatformIndex >= MaxPlatformIndex)
                    {
                        currentPlatformIndex = 0;
                    }
                    if (platformObjects[currentPlatformIndex])
                        Destroy(platformObjects[currentPlatformIndex]);
                }
            } else
            {
                deltaChance -= 0.2F;
                lastMaxHeight -= maxHeightDistance;
            }
            lastMaxDistace += platforms[0].dimensions.x + Random.value;
            lastMaxHeight = Mathf.Clamp(lastMaxHeight, 0, maxHeight);
            Debug.Log(lastMaxHeight);
        }

        if (lastMaxGourndDistance < position.x + spawnDistance)
        {
            groundObjects[currentGroundIndex++] = SpawnPlatform(groundPlatform, lastMaxGourndDistance + groundPlatform.dimensions.x * 0.5F, 0.0F);
            if (currentGroundIndex >= MaxGroundIndex)
            {
                currentGroundIndex = 0;
            }
            if (groundObjects[currentGroundIndex])
                Destroy(groundObjects[currentGroundIndex]);

            lastMaxGourndDistance += groundPlatform.dimensions.x;
        }
    }

    private void Start()
    {
        Vector3 position = (followTarget ? followTarget : transform).position;
        lastMaxGourndDistance = position.x + spawnDistance;
        lastMaxDistace = lastMaxGourndDistance;

        groundObjects = new GameObject[MaxGroundIndex];
        platformObjects = new GameObject[MaxPlatformIndex];
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 position = (followTarget ? followTarget : transform).position;
        Gizmos.DrawLine(position, position + Vector3.right * spawnDistance);
        Gizmos.DrawLine(position + Vector3.up, position + Vector3.right * (spawnDistance + groundPlatform.dimensions.x * 0.5F) + Vector3.up);
        Gizmos.DrawLine(position, position + Vector3.left * destructionDistance);
    }
}
