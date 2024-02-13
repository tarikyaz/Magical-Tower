using System.Collections.Generic;
using UnityEngine;

public class Barrage : Spell
{
    [SerializeField] float speed = 10, damageArea = 5;
    [SerializeField] BarrageObj barrageObjPrefab;
    public override void Set(Enemy[] enemies)
    {
        base.Set(enemies);
        for (int i = 0; i < enemiesPosArray.Length; i++)
        {
            BarrageObj newObj = Instantiate(barrageObjPrefab, transform);
            newObj.gameObject.SetActive(true);
            newObj.Set(enemiesPosArray[i], speed, damageArea);
        }
    }
}
