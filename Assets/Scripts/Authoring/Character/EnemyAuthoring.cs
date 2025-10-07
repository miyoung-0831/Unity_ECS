using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(CharacterAuthoring))]
public class EnemyAuthoring : MonoBehaviour
{
    private class Baker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<CharacterTag>(entity);
            AddComponent<EnemyTag>(entity);
        }
    }
}