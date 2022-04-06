using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillPointsCount : MonoBehaviour
{
    private Text _skillPointsText;
    void Start()
    {
        _skillPointsText = GetComponent<Text>(); //gets the Text component from skillPointsText
    }
    void Update()
    {
        _skillPointsText.text = "Skill Points: " + GameObject.FindWithTag("Player").GetComponent<Player>().GetSkillPoints(); //changes Text to print Skill Points
    }
}
