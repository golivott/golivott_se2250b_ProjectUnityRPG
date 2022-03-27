using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Button button;
    public int id;
    public static bool[] bought = {false, false, false, false, false, false, false, false, false, false, false};
    public int skillPoints;
    public Text SkillPointsText;
    public GameObject insufficientFunds;
    public GameObject insufficientSkills;
    public GameObject playerr;

    public void Start()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(false);
        }

        SkillPointsText = GetComponent<Text>();
        playerr = GameObject.Find("Player");
    }

    public void Update(){
        skillPoints = GameObject.FindWithTag("Player").GetComponent<Player>().GetSkillPoints();
    }

    public void buySword()
    {
        if (skillPoints >= 1)
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);
            bought[id] = true;
        }
        else if(skillPoints < 1)
        {
            Invoke("InsufficientFunds", 0.5f);
        }
    }
    
    public void buyCooldown()
    {
        if (skillPoints >= 1 && bought[0])
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);
            bought[id] = true;
        }
        else if(skillPoints < 1 && bought[0])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 1 && !bought[0])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }
    
    public void buyRegen()
    {
        if (skillPoints >= 1 && bought[0])
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);
            bought[id] = true;
        }
        else if(skillPoints < 1 && bought[0])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 1 && !bought[0])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }
    
    public void buyFireStomp()
    {
        if (skillPoints >= 1 && bought[1])
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-1);
            bought[id] = true;
            playerr.GetComponent<Player>().unlockAbilityTwo = true;
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
    
    public void buyAttack()
    {
        if (skillPoints >= 3 && bought[2])
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-3);
            bought[id] = true;
        }
        else if(skillPoints < 3 && bought[2])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 3 && !bought[2])
        {
            Invoke("InsufficientSkills",0.5f);
        }
    }

    public void buyDash()
    {
        if (skillPoints >= 2 && bought[1])
        {
            //add sword to player
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
        if (skillPoints >= 3 && bought[4])
        {
            //add sword to player
            button.interactable = false;
            GameObject.FindWithTag("Player").GetComponent<Player>().AddSkillPoints(-3);
            bought[id] = true;
            playerr.GetComponent<PlayerOne>().unlockAbilityOne = true;
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
        if (skillPoints >= 2 && bought[6])
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
        if (skillPoints >= 3 && bought[7])
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
        if (skillPoints >= 2 && bought[6])
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
        if (skillPoints >= 3 && bought[9])
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
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void offHoverButton()
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
