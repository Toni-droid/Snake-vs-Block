using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] PlatformPrefabs;
    [SerializeField] private int MinPlatforms;
    [SerializeField] private int MaxPlatforms;
    [SerializeField] private float DistanceBetweenPlatforms;
    [SerializeField] private GameObject FinishPlatform;
    [SerializeField] private Game Game;
    public float finishZ;
    private void Awake()

    {
        int levelIndex = Game.LevelIndex;
        Random random = new Random(levelIndex);
        int platformsCount = RandomRange(random, MinPlatforms, MaxPlatforms + 1);

        for (int i = 0; i < platformsCount + 1; i++)
        {
            int prefabIndex = RandomRange(random, 0, PlatformPrefabs.Length);
            //GameObject platformPrefab = i == platformsCount ? FinishPlatform : PlatformPrefabs[prefabIndex];
            GameObject platform;
            if(i == platformsCount)
            {
                platform = Instantiate(FinishPlatform, transform);
                platform.transform.localPosition = CalculatePlatformPosition(i);
                finishZ = platform.transform.position.z;
                
            }
            else
            {
                platform = Instantiate(PlatformPrefabs[prefabIndex], transform);
            }
        
            platform.transform.localPosition = CalculatePlatformPosition(i);
            
        }


    }

    private int RandomRange(Random random, int min, int maxExclusive)
    {
        int number = random.Next();
        int length = maxExclusive - min;
        number %= length;
        return min + number;
    }

    private float RandomRange(Random random, float min, float max)
    {
        float t = (float)random.NextDouble();
        return Mathf.Lerp(min, max, t);
    }
    private Vector3 CalculatePlatformPosition(int platformIndex)
    {
        return new Vector3(0, 0, DistanceBetweenPlatforms * platformIndex);
    }
}
