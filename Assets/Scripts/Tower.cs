using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform shootingPos;
    [Serializable]
    struct SpellSettingsStruc
    {
        public float CooldownDuration;
        public Spell Prefab;
    }
    public float CurrentDamage { get; private set; }

    [SerializeField] SpellSettingsStruc fireBall_Spell, barrage_Spell;

    private void Start()
    {
        CurrentDamage = 100;
        StartCoroutine(Shoot(fireBall_Spell.CooldownDuration, fireBall_Spell.Prefab));
        StartCoroutine(Shoot(barrage_Spell.CooldownDuration, barrage_Spell.Prefab));
    }
    IEnumerator Shoot(float delay, Spell obj)
    {
        while (CurrentDamage > 0)
        {
            yield return new WaitForSeconds(delay);
            if (CurrentDamage > 0 && Level.Instance.EnemySpawner.GetActiveActiveEnemies(out Enemy[] _enemies))
            {
                var newSpell = Instantiate(obj, shootingPos.position, Quaternion.identity);
                newSpell.Set(_enemies);
            }
        }
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
