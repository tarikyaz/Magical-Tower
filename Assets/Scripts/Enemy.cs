using System;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Enemy : MonoBehaviour
{
    public enum EnemyTypesEnum
    {
        None, Default, Fast, BigAndSlow
    }
    [Serializable]
    public struct EnemySettingsStruc
    {
        public EnemyTypesEnum Type;
        public GameObject Body;
        public float Speed;
        public float Damage;
    }
    [SerializeField]
    EnemySettingsStruc[] enemySettingsArray;
    EnemySettingsStruc currentSettings;
    Transform currenttarget;

    public void Set(EnemyTypesEnum type, Vector3 worldPos, Transform target)
    {
        currentSettings = enemySettingsArray.FirstOrDefault(x => x.Type == type);
        foreach (var enemy in enemySettingsArray.Select(x => x.Body))
        {
            enemy.gameObject.SetActive(enemy == currentSettings.Body);
        }
        transform.position = worldPos;
        currenttarget = target;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        if (gameObject.activeSelf && currentSettings.Speed > 0 && currenttarget != null)
        {
            // Calculate the direction to the target
            Vector3 direction = currenttarget.position - transform.position;
            direction.Normalize();

            // Move the object towards the target
            transform.Translate(direction * currentSettings.Speed * Time.deltaTime, Space.World);

            // Rotate the object towards the moving direction
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = toRotation;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            BaseEvent.CallTowerHitWithDamage(currentSettings.Damage);
            gameObject.SetActive(false);
        }
    }
}
