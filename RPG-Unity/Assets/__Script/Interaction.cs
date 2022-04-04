using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interaction : MonoBehaviour //class used to manage interactions
{
    [Header("Level 1 Interactions")]
    public bool hasKey;
    public bool hasMap;

    [Header("Level 2 Interactions")] 
    public bool flippedLever;
    
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>(); //gets values from player script
    }

    public void Interact(GameObject gameObject) //interact method that uses gameobject tags to decide what is being interacted with
    {
        if (gameObject.CompareTag("Chest"))
        {
            hasKey = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found a key! Maybe it opens a door...";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);

        }

        if (gameObject.CompareTag("BasementDoor"))
        {
            if (hasKey)
            {
                _player.transform.position = new Vector3(-54, 1.5f, 0);
               
            }
            else
            {
                GameObject.Find("Text").GetComponent<Text>().text = "You need a key to open ths door";
                GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                Invoke("RemoveText", 3f);
            }
        }

        if (gameObject.CompareTag("Map"))
        {
            hasMap = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found the map! Its time to venture out into the forest";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);
        }

        if (gameObject.CompareTag("Outside"))
        {
            if (hasMap)
            {
                DontDestroyOnLoad(GameObject.FindWithTag("Player"));
                GameObject.FindWithTag("MainCamera").GetComponent<Camera>().backgroundColor =
                    new Color(113f / 255f, 170f / 255f, 52f / 255f);
                GameObject.Find("LevelOneIcon").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("LevelTwoIcon").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("Text").GetComponent<Text>().text = "I've been traveling a while, maybe I should check out that shop!";
                GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                Invoke("RemoveText", 8f); 
                SceneManager.LoadScene("Level2");
            }
            else
            {
                GameObject.Find("Text").GetComponent<Text>().text = "You can't leave until you find the map. Legend says Grandpa hid it in the basement...";
                GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                Invoke("RemoveText", 3f);    
            }
        }

        // Level 2 Interactable
        // When lever is interacted with
        if (gameObject.CompareTag("Lever"))
        {
            GameObject.Find("Lever Off").GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            GameObject.Find("Lever On").GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            GameObject.Find("Bridge Baricade").SetActive(false);
        }
    }

    public void RemoveText()    //method that removes text by making it invisible using colors
    {
        GameObject.Find("Text").GetComponent<Text>().color = new Color(0, 0, 0, 0);
    }
}