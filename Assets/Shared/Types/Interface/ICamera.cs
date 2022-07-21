using UnityEngine;

public interface ICamera
{
    void AssignTarget(Transform target);
    void AssignTarget(int viewID);
    Transform GetTarget();
}
