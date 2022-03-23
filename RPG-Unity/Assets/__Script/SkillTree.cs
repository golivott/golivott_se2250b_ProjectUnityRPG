using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Button button; //UI Button for upgrade
    public int id; //id for each skill
    public static bool[] bought = {false, false, false, false, false, false, false, false, false, false, false}; //sets all skills to not bought
    public int skillPoints; //skillPoints int
    public Text SkillPointsText; //Text for skillPoints 
    public GameObject insufficientFunds; //InsufficientFund prefab
    public GameObject insufficientSkills; //InsufficientSkill prefab
    public GameObject playerr;

    public void Start() //Start method
    {
        for (int i = 0; i < button.transform.childCount; i++) //iterates through all children of the button
        {
            button.transform.GetChild(i).gameObject.SetActive(false); //hides children
        }

        SkillPointsText = GetComponent<Text>(); //gets Text component
        playerr = GameObject.Find("Player"); //finds player
    }

    public void Update(){ //Update method
        skillPoints = GameObject.FindWithTag("Player").GetComponent<Player>().GetSkillPoints(); //gets skillPoints from player GameObject
    }

    public void buySword() //buys sword
    {
        if (skillPoints >= 1) //if player has sufficient skill points, then continue
        {
            //add sword to player
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            bought[id] = true; //bought turns to true
        }
        else if(skillPoints < 1) //if player has insufficient skill points, then continue
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFunds message
        }
    }
    
    public void buyCooldown() //buys cooldown
    {
        if (skillPoints >= 1 && bought[0]) //if player has sufficient skill points, then continue
        {
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            bought[id] = true; //bought turns to true
        }
        else if(skillPoints < 1 && bought[0]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !bought[0]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }
    
    public void buyRegen() //buys regen
    {
        if (skillPoints >= 1 && bought[0]) //if player has sufficient skill points
        {
            button.interactable = false; //no longer allows user to interact with button
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);  //decreases player skillPoints by 1
            bought[id] = true; //bought turns to true
        }
        else if(skillPoints < 1 && bought[0]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 1 && !bought[0])  //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }
    
    public void buyFireStomp() //buys fireStomp
    {
        if (skillPoints >= 1 && bought[1])
        {
            button.interactable = false; //if player has sufficient skill points
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1); //decreases player skillPoints by 1
            bought[id] = true; //bought turns to true
            playerr.GetComponent<SkillTreeAbilities>().unlockedFireStomp = true; //unlocks fireStomp
        }
        else if(skillPoints < 2 && bought[1]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        }
        else if (skillPoints >= 2 && !bought[1]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f);  //prints insufficientSkills method
        }
    }
    
    public void buyAttack() //buys attackUpgrade
    {
        if (skillPoints >= 3 && bought[2]) //if player has sufficient skill points
        {
            button.interactable = false; //decreases player skillPoints by 1
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-3); //decreases player skillPoints by 1
            bought[id] = true; //bought turns to true
        }
        else if(skillPoints < 3 && bought[2]) //if not enough skill points but sufficient skills
        {
            Invoke("InsufficientFunds", 0.5f); //displays insufficientFund
        } 
        else if (skillPoints >= 3 && !bought[2]) //if not enough skill points or skills
        {
            Invoke("InsufficientSkills",0.5f); //prints insufficientSkills method
        }
    }

    public void buyDash() //buys dash
    {
        if (skillPoints >= 2 && bought[1]) //if player has sufficient skill points, then does same as above method
        {
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-2);
            bought[id] = true;
        }
        else if(skillPoints < 2 && bought[1])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 2 && !bought[1])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }
    
    public void buySlash()
    {
        if (skillPoints >= 3 && bought[4]) //if player has sufficient skill points, then does same as above method
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-3);
            bought[id] = true;
            playerr.GetComponent<SkillTreeAbilities>().unlockedSwordSlash = true; //swordSlash unlocks
        }
        else if(skillPoints < 3 && bought[4])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 3 && !bought[4])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }

    public void buySpeed()
    {
        if (skillPoints >= 2 && bought[6]) //if player has sufficient skill points, then does same as above method
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-2);
            bought[id] = true;
        }
        else if(skillPoints < 2 && bought[6])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 2 && !bought[6])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }
    
    public void buyCoin()
    {
        if (skillPoints >= 3 && bought[7]) //if player has sufficient skill points, then does same as above method
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-3);
            bought[id] = true;
        }
        else if(skillPoints < 3 && bought[7])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 3 && !bought[7])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }

    public void buyShield1()
    {
        if (skillPoints >= 2 && bought[6]) //if player has sufficient skill points, then does same as above method
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-2);
            bought[id] = true;
        }
        else if(skillPoints < 2 && bought[6])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 2 && !bought[6])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }
    
    public void buyShield2()
    {
        if (skillPoints >= 3 && bought[9]) //if player has sufficient skill points, then does same as above method
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-3);
            bought[id] = true;
        }
        else if(skillPoints < 3 && bought[9])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 3 && !bought[9])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }

    public void onHoverButton()
    {
        for (int i = 0; i < button.transform.childCount; i++) //when mouse is hovering over button, info about skill appears
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void offHoverButton() //when mouse is not hovering over button, info about skill disappears
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
