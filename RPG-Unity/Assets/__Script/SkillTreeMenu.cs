using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SkillTreeMenu : MonoBehaviour
{
    public GameObject skillTreeUI; //Skilltree UI

    private void Start()
    {
        skillTreeUI.SetActive(false); //Disables overlay
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) //Turns on overlay
        {
            skillTreeUI.SetActive(true);

        }
        if(Input.GetKeyDown(KeyCode.Backspace)){ //Turns off overlay
            skillTreeUI.SetActive(false);
        }

    }
}
