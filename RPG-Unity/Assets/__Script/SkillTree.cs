using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Button button; //UI Button for upgrade
    public int id; //id for each skill
    public static bool[] Bought = {false, false, false, false, false, false, false, false, false, false}; //sets all skills to not bought
    public int skillPoints; //skillPoints int
    public GameObject insufficientFunds; //InsufficientFund prefab
    public GameObject insufficientSkills; //InsufficientSkill prefab
    public GameObject playerr;

    public void Start() //Start method
    {
        for (int i = 0; i < button.transform.childCount; i++) //iterates through all children of the button
        {
            button.transform.GetChild(i).gameObject.SetActive(false); //hides children
        }
        
        playerr = GameObject.FindWithTag("Player"); //finds player
    }

    public void Update(){ //Update method
        skillPoints = GameObject.FindWithTag("Player").GetComponent<Player>().GetSkillPoints(); //gets skillPoints from player GameObject
    }

    public void BuyAbility1() //buys ability 1
    {
        if (skillPoints >= 1) //if player has sufficient skill points, then continue
        {
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            Bought[id] = true; //bought turns to true

            playerr.GetComponent<Player>().unlockAbilityOne = true;
        }
        else if(skillPoints < 1) //if player has insufficient skill points, then continue
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFunds message
        }
    }

    public void BuyAbility1Damage() //buys ability 1 damage increase
    {
        if (skillPoints >= 1 && Bought[0]) //if player has sufficient skill points, then continue
        {
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            Bought[id] = true; //bought turns to true

            playerr.GetComponent<Player>().ability1Damage *= 1.5f;
        }
        else if(skillPoints < 1 && Bought[0]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[0]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }

    public void BuyAbility1Cooldown() //buys ability 1 cooldown reduction
    {
        if (skillPoints >= 1 && Bought[0]) //if player has sufficient skill points
        {
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);  //decreases player skillPoints by 1
            Bought[id] = true; //bought turns to true

            playerr.GetComponent<Player>().ability1Delay *= 0.9f;
        }
        else if(skillPoints < 1 && Bought[0]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[0])  //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }

    public void BuyAbility1Damage2() //buys ability 1 damage increase 2
    {
        if (skillPoints >= 1 && Bought[1])
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);
            Bought[id] = true;
            
            playerr.GetComponent<Player>().ability1Damage *= 2f;
        }
        else if(skillPoints < 1 && Bought[1]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[1]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f);  //prints insufficientSkills method
        }
    }

    public void BuyAbility1Cooldown2() //buys ability 1 cooldown reduction 2
    {
        if (skillPoints >= 1 && Bought[2]) //if player has sufficient skill points
        {
            button.interactable = false; //decreases player skillPoints by 1
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            Bought[id] = true; //bought turns to true
            
            playerr.GetComponent<Player>().ability1Delay *= 0.8f;
        }
        else if(skillPoints < 1 && Bought[2]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[2]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }

    public void BuyAbility2() //buys dash
    {
        if (skillPoints >= 1) //if player has sufficient skill points, then does same as above method
        {
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);
            Bought[id] = true;
            
            playerr.GetComponent<Player>().unlockAbilityTwo = true;
        }
        else if(skillPoints < 1)
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 1)
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }

    public void BuyAbility2Damage() //buys ability 2 damage increase
    {
        if (skillPoints >= 1 && Bought[5]) //if player has sufficient skill points, then continue
        {
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            Bought[id] = true; //bought turns to true
            
            playerr.GetComponent<Player>().ability2Damage *= 1.5f;
        }
        else if(skillPoints < 1 && Bought[5]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[5]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }

    public void BuyAbility2Cooldown() //buys ability 2 cooldown reduction
    {
        if (skillPoints >= 1 && Bought[5]) //if player has sufficient skill points
        {
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);  //decreases player skillPoints by 1
            Bought[id] = true; //bought turns to true
            
            playerr.GetComponent<Player>().ability2Delay *= 0.9f;
        }
        else if(skillPoints < 1 && Bought[5]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[5])  //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }

    public void BuyAbility2Damage2() //buys ability 2 damage increase 2
    {
        if (skillPoints >= 1 && Bought[6])
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);
            Bought[id] = true;
            
            playerr.GetComponent<Player>().ability2Damage *= 2f;
        }
        else if(skillPoints < 1 && Bought[6]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[6]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f);  //prints insufficientSkills method
        }
    }

    public void BuyAbility2Cooldown2() //buys ability 2 cooldown reduction 2
    {
        if (skillPoints >= 1 && Bought[7]) //if player has sufficient skill points
        {
            button.interactable = false; //decreases player skillPoints by 1
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            Bought[id] = true; //bought turns to true
            
            playerr.GetComponent<Player>().ability2Delay *= 0.8f;
        }
        else if(skillPoints < 1 && Bought[7]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !Bought[7]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }

    public void OnHoverButton()
    {
        for (int i = 0; i < button.transform.childCount; i++) //when mouse is hovering over button, info about skill appears
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OffHoverButton() //when mouse is not hovering over button, info about skill disappears
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void InsufficientFunds()
    {
        var clone = Instantiate(insufficientFunds, new Vector3(500, 40, 0), Quaternion.identity);
        Destroy(clone,3);
    }

    public void InsufficientSkills()
    {
        var clone1 = Instantiate(insufficientSkills, new Vector3(500, 40, 0), Quaternion.identity);
        Destroy(clone1,3);
    }

}
