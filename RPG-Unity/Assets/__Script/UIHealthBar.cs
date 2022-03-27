using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float health){
        slider.maxValue = health; //sets max value of health slider to parameter passed through
        slider.value = health; //sets current slider value to max health value
    }

    public void SetHealth(float health){ //sets the health bar to the health passed through
        slider.value = health;
    }
}
