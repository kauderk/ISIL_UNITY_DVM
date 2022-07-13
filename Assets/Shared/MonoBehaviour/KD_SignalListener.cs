using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KD_SignalListener : MonoBehaviour, KD_ISignalListener
{
    public void OnSignal()
    {
        Debug.Log("Signal received");
    }
}