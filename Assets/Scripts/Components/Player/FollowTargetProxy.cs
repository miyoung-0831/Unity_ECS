using Unity.Entities;
using UnityEngine;
using System.Collections.Generic;

public class FollowTargetProxy : MonoBehaviour
{
    public static readonly List<FollowTargetProxy> All = new();

    public Entity TargetEntity;

    private void OnEnable()
    {
        All.Add(this);
    }

    private void OnDisable()
    {
        All.Remove(this);
    }
}
