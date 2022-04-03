using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement;


public class StartingMenu : MonoBehaviour
{
    public Button button;
    public GameObject playerPrefab;
    public void OnHoverButton()     //displays information and a picture about the character the user chooses
    {
        button.transform.GetChild(1).gameObject.SetActive(true);
        button.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void OffHoverButton()    //removes the information about the character when the user is no longer hovering over the button
    {
        button.transform.GetChild(1).gameObject.SetActive(false);
        button.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void OnClick()   //when the user clicks the button instantiate the correct prefab and load level 1
    {
        var player = Instantiate(playerPrefab, new Vector3(-16, -5, 0), Quaternion.identity);
        DontDestroyOnLoad(player);
        SceneManager.LoadScene("Level1");
    }
}
