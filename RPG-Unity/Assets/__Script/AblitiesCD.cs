using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AblitiesCD : MonoBehaviour
{

    [Header("Dash")]
    public Image dashImage;
    public float cooldownOne = 1;
    bool isCooldownOne = false;
    public KeyCode Dash;

    [Header("FireStomp")]
    public Image fireStompImage;
    public float cooldownTwo = 3;
    bool isCooldownTwo = false;
    public KeyCode FireStomp;


    [Header("SwordSlash")]
    public Image swordSlashImage;
    public float cooldownThree = 5;
    bool isCooldownThree = false;
    public KeyCode swordSlash;


    // Start is called before the first frame update
    void Start()
    {
        dashImage.fillAmount = 0;
        fireStompImage.fillAmount = 0;
        swordSlashImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AbilityDash();
        AbilityFirestomp();
        AbilitySwordslash();
    }

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

    void AbilityFirestomp(){
        if(Input.GetKey(FireStomp) && isCooldownTwo == false){
            isCooldownTwo = true;
            fireStompImage.fillAmount = 1;
        }

        if(isCooldownTwo){
            fireStompImage.fillAmount-= 1 / cooldownTwo * Time.deltaTime;
            if(fireStompImage.fillAmount <=0){
                fireStompImage.fillAmount = 0;
                isCooldownTwo=false;
            }
        }
    }



    void AbilitySwordslash(){
        if(Input.GetKey(swordSlash) && isCooldownThree == false){
            isCooldownThree = true;
            swordSlashImage.fillAmount = 1;
        }

        if(isCooldownThree){
            swordSlashImage.fillAmount-= 1 / cooldownThree * Time.deltaTime;
            if(swordSlashImage.fillAmount <=0){
                swordSlashImage.fillAmount = 0;
                isCooldownThree=false;
            }
        }
    }
    
    
    
}
