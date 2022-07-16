using UnityEngine;

public interface ICamera
{
    void AssignTarget(Transform target);
    Transform GetTarget();
}
