using Unity.Entities;
using Unity.Mathematics;

public struct CharacterTag : IComponentData
{
}

public struct CharacterMoveDirection : IComponentData
{
    public float2 Value;
}

public struct CharacterMoveSpeed : IComponentData
{
    public float Value;
}
