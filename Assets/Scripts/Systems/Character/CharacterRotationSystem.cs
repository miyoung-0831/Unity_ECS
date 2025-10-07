using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct CharacterRotationSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (direction, localTransform) in SystemAPI.Query<RefRO<CharacterMoveDirection>, RefRW<LocalTransform>>().WithAll<PlayerTag>())
        {
            float2 dir = direction.ValueRO.Value;
            if (math.lengthsq(dir) < 0.0001f)
                continue;

            // 이동 방향에서 Yaw 계산
            float yaw = math.atan2(dir.x, dir.y); // 라디안

            // 현재 로컬트랜스폼
            var transform = localTransform.ValueRO;
            var targetRot = quaternion.Euler(0, yaw, 0);

            // 보간 회전 (부드럽게 회전)
            transform.Rotation = math.slerp(transform.Rotation, targetRot, deltaTime * 10f);

            localTransform.ValueRW = transform;
        }
    }
}