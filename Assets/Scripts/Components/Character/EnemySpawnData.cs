using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public struct EnemySpawnData : IComponentData
{
    public Entity EnemyPrefab;
    public float SpawnInterval;
    public float SpawnDistance;
}

public struct EnemySpawnState : IComponentData
{
    public float SpawnTimer;
    public Random Random;
}
