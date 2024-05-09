using UnityEngine;
using Zenject;

public class ShipMoveHandler : ITickable
{
    private readonly Camera camera;
    private readonly Ship ship;
    private readonly ScreenBoundary screenBoundary;

    private readonly int shipBorder = 3;

    public ShipMoveHandler(Camera camera, Ship ship, ScreenBoundary screenBoundary)
    {
        this.camera = camera;
        this.ship = ship;
        this.screenBoundary = screenBoundary;
    }

    public void Tick()
    {
        if (Time.timeScale != 0 && UIHovering.IsOverUI() == false)
        {
#if PLATFORM_STANDALONE_WIN
            if (Input.GetMouseButton(0))
#elif UNITY_IOS || UNITY_ANDROID
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
#endif
            {
                 Move();
            }
        }
    }

    private void Move()
    {
        Vector3 newPosition = Input.mousePosition;

        newPosition = camera.ScreenToWorldPoint(newPosition);

        newPosition.x = Mathf.Clamp(newPosition.x, screenBoundary.Left + (ship.GetSize.x / 2), 
            screenBoundary.Right - (ship.GetSize.x / 2));
        newPosition.y = Mathf.Clamp(newPosition.y, screenBoundary.Bottom, screenBoundary.Bottom + shipBorder);
        newPosition.z = 0;

        ship.GetPosition = newPosition;
    }
}