using UnityEngine;

public class ScreenBoundary
{
    private readonly Camera camera;

    public ScreenBoundary(Camera camera)
    {
        this.camera = camera;
    }

    public float Top
    {
        get
        {
            return camera.orthographicSize;
        }
    }

    public float Bottom
    {
        get
        {
            return -camera.orthographicSize;
        }
    }

    public float Left
    {
        get
        {
            return -camera.aspect * camera.orthographicSize;
        }
    }

    public float Right
    {
        get
        {
            return camera.aspect * camera.orthographicSize;
        }
    }

    public bool IsBelowBottom(IInteractiveObject interactiveObject)
    {
        if (interactiveObject.GetPosition.y < Bottom - interactiveObject.GetSize.y)
        {
            return false;
        }
        return true;
    }

    public bool IsOnScreen(IInteractiveObject interactiveObject)
    {
        return interactiveObject.GetPosition.y < Top;
    }
}