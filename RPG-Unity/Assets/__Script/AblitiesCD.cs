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
    [FormerlySerializedAs("Dash")] public KeyCode dash;

    //Firestomp information
    [Header("FireStomp")]
    public Image fireStompImage;
    public float cooldownTwo = 3;
    bool _isCooldownTwo = false;
    [FormerlySerializedAs("FireStomp")] public KeyCode fireStomp;
    
    //SwordSlash information
    [Header("SwordSlash")]
    public Image swordSlashImage;
    public float cooldownThree = 5;
    bool _isCooldownThree = false;
    public KeyCode swordSlash;


    // Allows the abilities to have no grey zone (not on cooldown at start)
    void Start()
    {
        dashImage.fillAmount = 0;
        fireStompImage.fillAmount = 0;
        swordSlashImage.fillAmount = 0;
    }

    // Update to continously check if abilities are activated
    void Update()
    {
        AbilityDash();
        AbilityFirestomp();
        AbilitySwordslash();
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
    void AbilityFirestomp(){
        if(Input.GetKey(fireStomp) && _isCooldownTwo == false){
            _isCooldownTwo = true;
            fireStompImage.fillAmount = 1;
        }

        if(_isCooldownTwo){
            fireStompImage.fillAmount-= 1 / cooldownTwo * Time.deltaTime;
            if(fireStompImage.fillAmount <=0){
                fireStompImage.fillAmount = 0;
                _isCooldownTwo=false;
            }
        }
    }


    //If Q, puts ability on cooldown until cooldown is done (timer effect)
    void AbilitySwordslash(){
        if(Input.GetKey(swordSlash) && _isCooldownThree == false){
            _isCooldownThree = true;
            swordSlashImage.fillAmount = 1;
        }

        if(_isCooldownThree){
            swordSlashImage.fillAmount-= 1 / cooldownThree * Time.deltaTime;
            if(swordSlashImage.fillAmount <=0){
                swordSlashImage.fillAmount = 0;
                _isCooldownThree=false;
            }
        }
    }
}
