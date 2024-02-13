using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Singleton<Level>
{
    public Tower Tower;
    private void OnEnable()
    {
        BaseEvent.OnTowerHit += OnTowerHitHandler;
    }
    private void OnDisable()
    {
        BaseEvent.OnTowerHit -= OnTowerHitHandler;
    }

    private void OnTowerHitHandler(float damage)
    {
        Tower.TakeDamage(damage);
    }
}
