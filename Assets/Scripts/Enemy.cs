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
    Transform currentTarget;

    public void Set(EnemyTypesEnum type, Vector3 worldPos, Transform target)
    {
        currentSettings = enemySettingsArray.FirstOrDefault(x => x.Type == type);
        foreach (var bodies in enemySettingsArray.Select(x => x.Body))
        {
            bodies.gameObject.SetActive(bodies == currentSettings.Body);
            bodies.transform.localPosition = Vector3.zero;
            bodies.transform.localRotation = Quaternion.identity;
        }
        transform.position = worldPos;
        currentTarget = target;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        if (gameObject.activeSelf && currentSettings.Speed > 0 && currentTarget != null)
        {
            // Calculate the direction to the target
            var targetPos = currentTarget.position;
            targetPos.y = 0;
            Vector3 direction = targetPos - transform.position;
            direction.Normalize();

            // Move the object towards the target
            transform.Translate(direction * currentSettings.Speed * Time.deltaTime, Space.World);
            var pos = transform.position;
            pos.y = 0;
            transform.position = pos;
            // Rotate the object towards the moving direction
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = toRotation;
            }
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            BaseEvent.CallTowerHitWithDamage(currentSettings.Damage);
            gameObject.SetActive(false);
        }
    }
}
