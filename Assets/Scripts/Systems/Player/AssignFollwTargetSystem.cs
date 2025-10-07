using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class AssignFollowTargetSystem : SystemBase
{
    private bool assigned;

    protected override void OnUpdate()
    {
        if (assigned)
            return;

        // PlayerTag�� �޸� ù ��° ��ƼƼ ã��
        Entity player = Entity.Null;
        foreach (var (localTransform, entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<PlayerTag>().WithEntityAccess())
        {
            player = entity;
            break;
        }

        if (player == Entity.Null)
            return;

        // ���� ��� ���Ͻÿ� TargetEntity ����
        var proxies = FollowTargetProxy.All;
        if (proxies.Count == 0)
            return;

        foreach (var p in proxies)
            p.TargetEntity = player;

        assigned = true;
    }
}