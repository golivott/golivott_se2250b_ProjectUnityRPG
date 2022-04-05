using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AblitiesCD : MonoBehaviour
{

    //Dash information
    [Header("Dash")]
    public Image dashImage;
    public float cooldownOne = 1;
    bool isCooldownOne = false;
    public KeyCode Dash;

    //AbilityOne information
    [Header("AbilityOne")]
    public Image abilityOneImage;
    public float cooldownTwo = 3;
    bool isCooldownTwo = false;
    public KeyCode abilityOne;
    
    //AbilityTwo information
    [Header("AbilityTwo")]
    public Image abilityTwoImage;
    public float cooldownThree = 5;
    bool isCooldownThree = false;
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
        if(Input.GetKey(Dash) && isCooldownOne == false){
            isCooldownOne = true;
            dashImage.fillAmount = 1;
        }

        if(isCooldownOne){
            dashImage.fillAmount-= 1 / cooldownOne * Time.deltaTime;
            if(dashImage.fillAmount <=0){
                dashImage.fillAmount = 0;
                isCooldownOne=false;
            }
        }
    }       
    
    //If E is pressed, puts ability on cooldown until cooldown is done (timer effect)
    void AbilityOne(){
        if(Input.GetKey(abilityOne) && isCooldownTwo == false){
            isCooldownTwo = true;
            abilityOneImage.fillAmount = 1;
        }

        if(isCooldownTwo){
            abilityOneImage.fillAmount-= 1 / cooldownTwo * Time.deltaTime;
            if(abilityOneImage.fillAmount <=0){
                abilityOneImage.fillAmount = 0;
                isCooldownTwo=false;
            }
        }
    }


    //If Q, puts ability on cooldown until cooldown is done (timer effect)
    void AbilityTwo(){
        if(Input.GetKey(abilityTwo) && isCooldownThree == false){
            isCooldownThree = true;
            abilityTwoImage.fillAmount = 1;
        }

        if(isCooldownThree){
            abilityTwoImage.fillAmount-= 1 / cooldownThree * Time.deltaTime;
            if(abilityTwoImage.fillAmount <=0){
                abilityTwoImage.fillAmount = 0;
                isCooldownThree=false;
            }
        }
    }

        
    
}
