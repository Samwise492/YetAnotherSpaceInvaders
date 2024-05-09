using UnityEngine;

[System.Serializable]
public class ShipCommonSettings
{
    public int Health => health;
    public int HitPoint => hitPoint;

    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int hitPoint = 10;
}