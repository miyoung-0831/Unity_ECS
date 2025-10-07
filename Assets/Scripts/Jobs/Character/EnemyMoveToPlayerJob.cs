using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[WithAll(typeof(EnemyTag))]
public partial struct EnemyMoveToPlayerJob : IJobEntity
{
    public float2 PlayerPosition;

    public void Execute(ref CharacterMoveDirection direction, in LocalTransform transform)
    {
        var vectorToPlayer = PlayerPosition - transform.Position.xz;
        direction.Value = math.normalizesafe(vectorToPlayer);
    }
}
