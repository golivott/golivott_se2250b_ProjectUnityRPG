using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillPointsCount : MonoBehaviour
{
    private Text skillPointsText;
    public SkillTree skillPointsCount;

    // Start is called before the first frame update
    void Start()
    {
        skillPointsText = GetComponent<Text>(); //gets the Text component from skillPOintsText
    }

    // Update is called once per frame
    void Update()
    {
        skillPointsText.text = "Skill Points: " + GameObject.FindWithTag("Player").GetComponent<Player>().GetSkillPoints(); //changes Text to print Skill Points
    }
}
