using UnityEngine;

public class FireBall : Spell
{
    [SerializeField] float damageArea = 3;
    [SerializeField] float speed = 4;


    private void Update()
    {
        if (targetPos != Vector3.zero)
        {
            // Calculate the direction to the target
            Vector3 direction = targetPos - transform.position;
            direction.Normalize();

            // Move the object towards the target
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

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
        if (other.gameObject.layer != 6)
        {
            Explode();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 6)
        {
            Explode();
            Destroy(gameObject);
        }
    }
    private void Explode()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, damageArea,1<< 7);
        if (hitEnemies.Length > 0)
        {
            foreach (var enemy in hitEnemies)
            {
                Debug.Log(enemy.gameObject.name);
                enemy.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
