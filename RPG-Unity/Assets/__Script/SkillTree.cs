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
    public static bool[] bought = {false,false,false,false,false,false,false,false,false,false,false};
    public static int skillPoints = 10;
    public Text SkillPointsText;
    public GameObject insufficientFunds;
    public GameObject insufficientSkills;

    public void Start()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(false);
        }
        SkillPointsText = GetComponent<Text>();
    }

    public void buySword()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 1)
        {
            //add sword to player
            button.interactable = false;
            
            skillPoints--;
            bought[id] = true;
        }
        else if(skillPoints < 1)
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        Invoke("Delay", 3f);
    }
    
    public void buyCooldown()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 1 && bought[0])
        {
            //add sword to player
            button.interactable = false;
            skillPoints--;
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
        Invoke("Delay", 3f);
    }
    
    public void buyRegen()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 1 && bought[0])
        {
            //add sword to player
            button.interactable = false;
            skillPoints--;
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
        Invoke("Delay", 3f);
    }
    
    public void buyAttack1()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 2 && bought[1])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 2;
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
        Invoke("Delay", 3f);
    }
    
    public void buyAttack2()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 3 && bought[2])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 3;
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
        Invoke("Delay", 3f);
    }

    public void buyDash()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 2 && bought[1])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 2;
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
        Invoke("Delay", 3f);
    }
    
    public void buySlash()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 3 && bought[4])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 3;
            bought[id] = true;
        }
        else if(skillPoints < 3 && bought[4])
        {
            Invoke("InsufficientFunds", 0.5f);
        }
        else if (skillPoints >= 3 && !bought[4])
        {
            Invoke("InsufficientSkills",0.5f);
        }
        Invoke("Delay", 3f);
    }

    public void buySpeed()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 2 && bought[6])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 2;
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
        Invoke("Delay", 3f);
    }
    
    public void buyCoin()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        
        if (skillPoints >= 3 && bought[7])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 3;
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
        Invoke("Delay", 3f);
    }

    public void buyShield1()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (skillPoints >= 2 && bought[6])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 2;
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
        Invoke("Delay", 3f);
    }
    
    public void buyShield2()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(true);
        }
        
        if (skillPoints >= 3 && bought[9])
        {
            //add sword to player
            button.interactable = false;
            skillPoints -= 3;
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
        Invoke("Delay", 3f);
    }

    public void InsufficientFunds()
    {
        var clone = Instantiate(insufficientFunds, new Vector3(770, 40, 0), Quaternion.identity);
        Destroy(clone,3);
    }

    public void InsufficientSkills()
    {
        var clone1 = Instantiate(insufficientSkills, new Vector3(770, 40, 0), Quaternion.identity);
        Destroy(clone1,3);
    }
    
    public void Delay()
    {
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    public static int SkillPoints
    {
        get => skillPoints;
        set => skillPoints = value;
    }
}
