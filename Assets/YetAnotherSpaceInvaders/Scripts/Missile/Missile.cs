using UnityEngine;

public enum MissileType
{
    ShipMissile,
    DoubleShipMissile,
    TripleShipMissle,
    EnemyMissile
}

public class Missile
{
    private readonly SpriteRenderer spriteRenderer;

    private readonly Sprite ShipMissileSprite;
    private readonly Sprite DoubleShipMissileSprite;
    private readonly Sprite TripleShipMissileSprite;
    private readonly Sprite EnemyMissileSprite;

    public Missile(
        SpriteRenderer spriteRenderer,
        Sprite ShipMissileSprite,
        Sprite DoubleShipMissileSprite,
        Sprite TripleShipMissileSprite,
        Sprite EnemyMissileSprite
    )
    {
        this.spriteRenderer = spriteRenderer;
        this.ShipMissileSprite = ShipMissileSprite;
        this.DoubleShipMissileSprite = DoubleShipMissileSprite;
        this.TripleShipMissileSprite = TripleShipMissileSprite;
        this.EnemyMissileSprite = EnemyMissileSprite;
    }


    public void ChangeType(MissileType type)
    {
        switch (type)
        {
            case MissileType.ShipMissile:
                spriteRenderer.sprite = ShipMissileSprite;
                break;
            case MissileType.DoubleShipMissile:
                spriteRenderer.sprite = DoubleShipMissileSprite;
                break;
            case MissileType.TripleShipMissle:
                spriteRenderer.sprite = TripleShipMissileSprite;
                break;
            case MissileType.EnemyMissile:
                spriteRenderer.sprite = EnemyMissileSprite;
                break;
            default:
                break;
        }
    }
}