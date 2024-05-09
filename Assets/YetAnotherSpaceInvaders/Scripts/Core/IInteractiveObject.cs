using UnityEngine;

public interface IInteractiveObject
{
    public Vector3 GetPosition { get; set; }
    public Vector3 GetSize { get; }
}