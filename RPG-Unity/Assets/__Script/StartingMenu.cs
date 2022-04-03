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
    private AsyncOperation sceneAsync;
    
    public void OnHoverButton()
    {
        button.transform.GetChild(1).gameObject.SetActive(true);
        button.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void OffHoverButton()
    {
        button.transform.GetChild(1).gameObject.SetActive(false);
        button.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void OnClick()
    {
        print("Clicked");
        var player = Instantiate(playerPrefab, new Vector3(-16, -5, 0), Quaternion.identity);
        DontDestroyOnLoad(player);
        SceneManager.LoadScene("Level1");
    }
}
