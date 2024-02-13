using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    protected Vector3 targetPos;
    public virtual void Set(Enemy enemy)
    {
        targetPos = enemy.transform.position;
    }
}
