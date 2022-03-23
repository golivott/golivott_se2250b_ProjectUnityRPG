using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SkillTreeMenu : MonoBehaviour
{
    public GameObject skillTreeUI;
    private bool _isActive = false;

    private void Start()
    {
        skillTreeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !_isActive)
        {
            skillTreeUI.SetActive(true);
        }
        else
        {
            skillTreeUI.SetActive(false);
        }
    }
}
