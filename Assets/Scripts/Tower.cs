using UnityEngine;

public class Tower : MonoBehaviour
{
    public float CurrentDamage { get; private set; }
    
    private void Start()
    {
        CurrentDamage = 100;
    }
    public void TakeDamage(float damage)
    {
        CurrentDamage -= damage;
        Debug.Log("CurrentDamage " + CurrentDamage);
        if (CurrentDamage <= 0)
        {
            BaseEvent.CallLevelFinish(false);
            CurrentDamage = 0;
        }
        BaseEvent.CallTowerDamageChange(CurrentDamage);
    }
}
