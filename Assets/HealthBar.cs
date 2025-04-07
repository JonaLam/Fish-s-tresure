using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField] Upgrade healthUpgrade;

    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = GameManager.instance.GetUpgradeValue(healthUpgrade);
        OnHealthChange();
    }

    private void OnEnable()
    {
        GameManager.instance.onHealthChanged += OnHealthChange;
    }

    void OnHealthChange() 
    {
        slider.value = GameManager.instance.playerHealth;
    }

    private void OnDisable()
    {
        GameManager.instance.onHealthChanged -= OnHealthChange;
    }
}
