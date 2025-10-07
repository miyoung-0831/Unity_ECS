using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct FreezeRotationSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (mass, velocity) in SystemAPI
            .Query<RefRW<PhysicsMass>, RefRW<PhysicsVelocity>>()
            .WithAll<CharacterTag>())
        {
            mass.ValueRW.InverseInertia = float3.zero;
            velocity.ValueRW.Angular = float3.zero;
        }
    }
}
