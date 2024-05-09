using UnityEngine;

[System.Serializable]
public class EnemyCommonSettings
{
    public int Health => health;
    public int HitPoint => hitPoint;
    public int Score => score;

    [SerializeField]
    private int health;
    [SerializeField]
    private int hitPoint;
    [SerializeField]
    private int score;
}