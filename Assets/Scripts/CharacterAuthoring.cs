using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public struct CharacterMoveDirection : IComponentData
{
    public float2 Value;
}

public struct CharacterMoveSpeed : IComponentData
{
    public float Value;
}

public struct CharacterLookDirection : IComponentData
{
    public float2 Value;
}

public struct CharacterRotationSpeed : IComponentData
{
    public float Value;
}

public class CharacterAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float RotationSpeed;

    private class Baker : Baker<CharacterAuthoring>
    {
        public override void Bake(CharacterAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<CharacterMoveDirection>(entity);
            AddComponent(entity, new CharacterMoveSpeed
            {
                Value = authoring.MoveSpeed
            });
            AddComponent<CharacterLookDirection>(entity);
            AddComponent(entity, new CharacterRotationSpeed
            {
                Value = authoring.RotationSpeed
            });
        }
    }
}

public partial struct CharacterMoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (velocity, direction, moveSpeed, look, rotationSpeed) in SystemAPI.Query<RefRW<PhysicsVelocity>, CharacterMoveDirection, CharacterMoveSpeed, CharacterLookDirection, CharacterRotationSpeed>())
        {
            var moveStep = direction.Value * moveSpeed.Value;
            velocity.ValueRW.Linear = new float3(moveStep.x, 0, moveStep.y);

            //var rotationAmount = look.Value.x * rotationSpeed.Value * SystemAPI.Time.DeltaTime;
            velocity.ValueRW.Angular += new float3(look.Value.x, 0, look.Value.y) * SystemAPI.Time.DeltaTime;
            //transform.ValueRW.Rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
