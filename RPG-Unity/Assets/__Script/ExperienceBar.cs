using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider sliderr; // slider object

    public void ResetExperience(){
        sliderr.value = 0; //sets initial exp value to zero
    }

    public void SetMaxExperience(float exp){
        sliderr.maxValue = exp; // sets max value to max experience allowed
    }

    public void SetExperience(float exp){
        sliderr.value = exp; // sets experience equal to parameter passed through
    }
}
