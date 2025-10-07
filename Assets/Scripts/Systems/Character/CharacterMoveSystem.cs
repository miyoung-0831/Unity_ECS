using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct CharacterMoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (velocity, direction, moveSpeed) in SystemAPI.Query<RefRW<PhysicsVelocity>, CharacterMoveDirection, CharacterMoveSpeed>())
        {
            var moveStep = direction.Value * moveSpeed.Value;
            velocity.ValueRW.Linear = new float3(moveStep.x, 0, moveStep.y);
        }
    }
}