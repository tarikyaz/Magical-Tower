using System;

public static class BaseEvent
{
    internal static Action<float> OnTowerHit;
    internal static Action<float> OnTowerDamageChange;

    internal static void CallLevelFinish(bool v)
    {

    }

    internal static void CallTowerDamageChange(float currentDamage)
    {
        OnTowerDamageChange?.Invoke(currentDamage);
    }

    internal static void CallTowerHitWithDamage(float damage)
    {
        OnTowerHit?.Invoke(damage);
    }
}
