using System.Linq;
using UnityEngine;

public class Spell : MonoBehaviour
{
    protected Vector3[] enemiesPosArray;
    public virtual void Set(Enemy[] enemies)
    {
        enemiesPosArray = enemies.Select(x => x.transform.position).ToArray();
    }
}
