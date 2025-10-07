using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct EnemyMoveToPlayerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position.xz;


        var moveToPlayerJob = new EnemyMoveToPlayerJob
        {
            PlayerPosition = playerPosition
        };

        state.Dependency = moveToPlayerJob.ScheduleParallel(state.Dependency);
    }
}
