using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SpawnInterval;
    public float SpawnDistance;
    public uint RandomSeed;

    private class Baker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new EnemySpawnData
            {
                EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
                SpawnInterval = authoring.SpawnInterval,
                SpawnDistance = authoring.SpawnDistance
            });

            AddComponent(entity, new EnemySpawnState
            {
                SpawnTimer = 0f,
                Random = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
    
}

