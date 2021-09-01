using UnityEngine;

public class SwarmEnemy : Enemy
{
    [SerializeField] private int _minSwarmSize = 3;
    [SerializeField] private int _maxSwarmSize = 7;
    [SerializeField] private float _spawnInterval = 0.15f;

    public int MinSwarmSize { get => _minSwarmSize; }
    public int MaxSwarmSize { get => _maxSwarmSize; }
    public float SpawnInterval { get => _spawnInterval; }
}
