using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GlobalEvents
{
    static readonly Dictionary<IDs, Action<object>> IDToEventMap = new Dictionary<IDs, Action<object>>();

    public static void Invoke(IDs ID, object payload)
    {
        IDToEventMap[ID]?.Invoke(payload);
    }
    public static void Subscribe(IDs ID, Action<object> callback)
    {
        if (!IsValid(ID))
        {
            IDToEventMap[ID] = callback; // init
        }
        else
        {
            IDToEventMap[ID] += callback;
        }
    }
    public static void Unsubscribe(IDs ID, Action<object> callback)
    {
        if (IsValid(ID))
        {
            IDToEventMap[ID] -= callback;
        }
    }
    public static void UnsubscribeAll(IDs ID)
    {
        if (!IsValid(ID))
            return;

        var callbacks = IDToEventMap[ID].GetInvocationList();
        foreach (var cb in callbacks.Cast<Action<object>>())
        {
            IDToEventMap[ID] -= cb;
        }
        IDToEventMap.Remove(ID);
    }
    private static bool IsValid(IDs ID)
    {
        return IDToEventMap.ContainsKey(ID) && IDToEventMap[ID] != null;
    }

    public static void ClanIDToEventMap()
    {
        foreach (var ID in IDToEventMap.Keys)
        {
            UnsubscribeAll(ID);
        }
    }
}
