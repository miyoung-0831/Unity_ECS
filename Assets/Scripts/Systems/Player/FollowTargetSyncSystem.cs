using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup))] // 캐릭터 이동 처리 후 카메라 갱신
public partial class FollowTargetSyncSystem : SystemBase
{
    protected override void OnUpdate()
    {
        foreach (var proxy in FollowTargetProxy.All)
        {
            var targetEntity = proxy.TargetEntity;

            if (targetEntity == Entity.Null)
                continue;
            if (!SystemAPI.Exists(targetEntity))
                continue;
            if (!SystemAPI.HasComponent<LocalTransform>(targetEntity))
                continue;

            var localTransform = SystemAPI.GetComponent<LocalTransform>(targetEntity);
            proxy.transform.SetPositionAndRotation(localTransform.Position, localTransform.Rotation);
        }
    }
}