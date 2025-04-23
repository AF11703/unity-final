using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;


    public void SetMaxHealthValue(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealthValue(float health)
    {
        slider.value = health;
    }
}
