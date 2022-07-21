using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStatsSubscriber
{
    void OnStatsChanged(PlayerStats stats);
}
// TODO: add the PlayerStats class