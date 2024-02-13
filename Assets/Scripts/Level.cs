public class Level : Singleton<Level>
{
    public Tower Tower;
    public EnemySpawner EnemySpawner;
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
