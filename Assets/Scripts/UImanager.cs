using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField] Image health_Image;
    private void OnEnable()
    {
        BaseEvent.OnTowerDamageChange += OnTowerHitHandler;
    }
    private void OnDisable()
    {
        BaseEvent.OnTowerDamageChange -= OnTowerHitHandler;
    }

    private void OnTowerHitHandler(float damage)
    {
        health_Image.fillAmount = Mathf.Lerp(0, 1, damage * 0.01f);
    }
}
