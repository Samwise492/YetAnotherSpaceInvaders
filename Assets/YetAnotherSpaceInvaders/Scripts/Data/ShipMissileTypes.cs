using System;
using UnityEngine;

//[CreateAssetMenu(menuName = "Create ShipMissileTypes")]
public class ShipMissileTypes : ScriptableObject
{
	public MissileData[] MissileData => missileData;

    [SerializeField]
	private MissileData[] missileData;
}

[Serializable]
public class MissileData
{
	public bool IsDefault => isDefault;
    public Sprite Sprite => sprite;
    public MissileType MissileType => missileType;
    public int Chance => chance;
    public float Delay => delay;

    [SerializeField]
    private bool isDefault;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private MissileType missileType;
    [SerializeField]
    private int chance;
    [SerializeField]
    private float delay;
}
