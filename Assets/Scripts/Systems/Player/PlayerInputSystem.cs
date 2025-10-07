using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.AI;

public partial class PlayerInputSystem : SystemBase
{
    private Camera camera;
    private PlayerInput input;

    protected override void OnStartRunning()
    {
        camera = Camera.main;
    }

    protected override void OnCreate()
    {
        input = new PlayerInput();
        input.Enable();
    }

    protected override void OnUpdate()
    {
        if (camera == null)
            camera = Camera.main;

        var currentInput = (float2)input.Player.Move.ReadValue<Vector2>();
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRotation = camera.transform.right;
        cameraForward.y = 0;
        cameraForward.Normalize();
        cameraRotation.y = 0;
        cameraRotation.Normalize();

        float3 moveDir = (float3)(cameraRotation * currentInput.x + cameraForward * currentInput.y);
        moveDir = math.normalizesafe(moveDir);

        foreach (var direction in SystemAPI.Query<RefRW<CharacterMoveDirection>>().WithAll<PlayerTag>())
        {
            direction.ValueRW.Value = new float2(moveDir.x, moveDir.z);
        }
    }
}