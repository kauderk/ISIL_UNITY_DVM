using System.Collections.Generic;
using UnityEngine;

public interface ICameraEvents
{
    void OnCameraAnimatorChange(Animator camera);
    //void CamShake();
    void OnCameraTriggersChange(List<string> shakes);
}