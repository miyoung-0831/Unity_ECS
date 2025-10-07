using UnityEngine;
using Unity.Entities;

public class PlayerAuthoring : MonoBehaviour
{
    private class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<CharacterTag>(entity);
            AddComponent<PlayerTag>(entity);
        }
    }
}