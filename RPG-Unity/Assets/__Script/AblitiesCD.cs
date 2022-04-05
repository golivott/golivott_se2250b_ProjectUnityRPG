using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AblitiesCD : MonoBehaviour
{

    //Dash information
    [Header("Dash")]
    public Image dashImage;
    public float cooldownOne = 1;
    bool _isCooldownOne = false;
    public KeyCode dash;

    //AbilityOne information
    [Header("AbilityOne")]
    public Image abilityOneImage;
    public float cooldownTwo = 3;
    bool _isCooldownTwo = false;
    public KeyCode abilityOne;
    
    //AbilityTwo information
    [Header("AbilityTwo")]
    public Image abilityTwoImage;
    public float cooldownThree = 5;
    bool _isCooldownThree = false;
    public KeyCode abilityTwo;


    // Allows the abilities to have no grey zone (not on cooldown at start)
    void Start()
    {
        dashImage.fillAmount = 0;
        abilityOneImage.fillAmount = 0;
        abilityTwoImage.fillAmount = 0;
    }

    // Update to continously check if abilities are activated
    void Update()
    {
        AbilityDash();
        AbilityOne();
        AbilityTwo();
    }


    //If space is pressed, puts ability on cooldown until cooldown is done (timer effect)
    void AbilityDash(){
        if(Input.GetKey(dash) && _isCooldownOne == false){
            _isCooldownOne = true;
            dashImage.fillAmount = 1;
        }

        if(_isCooldownOne){
            dashImage.fillAmount-= 1 / cooldownOne * Time.deltaTime;
            if(dashImage.fillAmount <=0){
                dashImage.fillAmount = 0;
                _isCooldownOne=false;
            }
        }
    }       
    
    //If E is pressed, puts ability on cooldown until cooldown is done (timer effect)
    void AbilityOne(){
        if(Input.GetKey(abilityOne) && _isCooldownTwo == false){
            _isCooldownTwo = true;
            abilityOneImage.fillAmount = 1;
        }

        if(_isCooldownTwo){
            abilityOneImage.fillAmount-= 1 / cooldownTwo * Time.deltaTime;
            if(abilityOneImage.fillAmount <=0){
                abilityOneImage.fillAmount = 0;
                _isCooldownTwo=false;
            }
        }
    }


    //If Q, puts ability on cooldown until cooldown is done (timer effect)
    void AbilityTwo(){
        if(Input.GetKey(abilityTwo) && _isCooldownThree == false){
            _isCooldownThree = true;
            abilityTwoImage.fillAmount = 1;
        }

        if(_isCooldownThree){
            abilityTwoImage.fillAmount-= 1 / cooldownThree * Time.deltaTime;
            if(abilityTwoImage.fillAmount <=0){
                abilityTwoImage.fillAmount = 0;
                _isCooldownThree=false;
            }
        }
    }

        
    
}
