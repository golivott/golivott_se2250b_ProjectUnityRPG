using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillPointsCount : MonoBehaviour   //script used to update the skill points value in the skilltree scene
{
    private Text skillPointsText;

    // Start is called before the first frame update
    void Start()
    {
        skillPointsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))  
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        }
        skillPointsText.text = "Skill Points: " + Player.SkillPoints;
    }
}
