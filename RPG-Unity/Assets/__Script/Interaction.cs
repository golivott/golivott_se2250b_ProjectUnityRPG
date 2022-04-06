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
    public bool openedChest;

    [Header("Level 2 Interactions")] 
    public bool flippedLever;
    public bool openedMoneyChest;
    public bool openedItemChest;
    
    private Player _player;
    private GameObject _shopUI;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>(); //gets values from player script
        _shopUI = _player.GetShopUI();
    }

    public void Interact(GameObject gameObject) //interact method that uses gameobject tags to decide what is being interacted with
    {
        // Universal Interactions
        // Open/Close shop window
        if (gameObject.CompareTag("Shop") && !_player.GetSkillTreeUI().activeSelf)
        {
            _shopUI.SetActive(!_shopUI.activeSelf);
            if (_shopUI.activeSelf)
            {
                _player.disableMovement = true;
            }
            else
            {
                _player.disableMovement = false;
            }
        }

        //if the player interacts with a chest they are given a key which lets them advance to the basement, a message is shown
        if (gameObject.CompareTag("Chest") && !openedChest)
        {
            hasKey = true;
            openedChest = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found a key! Maybe it opens a door...";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);

        }

        //if the player interacts with the item chest, they get 4 potions
        if (gameObject.CompareTag("ItemChest") && !openedItemChest)
        {
            openedItemChest = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found 4 potions, use them wisely...";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            
            if (!_player.GetComponent<Player>().Inventory.ContainsKey("HealthPotion"))
            {
                _player.GetComponent<Player>().itemList.Add("HealthPotion");
                _player.GetComponent<Player>().Inventory.Add("HealthPotion", 1);
                _player.GetComponent<Player>().differentItems++;
            }
            else
            {
                _player.GetComponent<Player>().Inventory["HealthPotion"]++;
            }
            
            if (!_player.GetComponent<Player>().Inventory.ContainsKey("SpeedPotion"))
            {
                _player.GetComponent<Player>().itemList.Add("SpeedPotion");
                _player.GetComponent<Player>().Inventory.Add("SpeedPotion", 1);
                _player.GetComponent<Player>().differentItems++;
            }
            else
            {
                _player.GetComponent<Player>().Inventory["SpeedPotion"]++;
            }
            
            if (!_player.GetComponent<Player>().Inventory.ContainsKey("StrengthPotion"))
            {
                _player.GetComponent<Player>().itemList.Add("StrengthPotion");
                _player.GetComponent<Player>().Inventory.Add("StrengthPotion", 1);
                _player.GetComponent<Player>().differentItems++;
            }
            else
            {
                _player.GetComponent<Player>().Inventory["StrengthPotion"]++;
            }
            
            if (!_player.GetComponent<Player>().Inventory.ContainsKey("ResistancePotion"))
            {
                _player.GetComponent<Player>().itemList.Add("ResistancePotion");
                _player.GetComponent<Player>().Inventory.Add("ResistancePotion", 1);
                _player.GetComponent<Player>().differentItems++;
            }
            else
            {
                _player.GetComponent<Player>().Inventory["ResistancePotion"]++;
            }
            Invoke("RemoveText", 3f);
        }

        //if the player interacts with the moneychest, they get $5000
        if (gameObject.CompareTag("MoneyChest") && !openedMoneyChest)
        {
            openedMoneyChest = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found $5000, use it at the shop!";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);
            GameObject.FindWithTag("Player").GetComponent<Player>().AddMoney(5000);
        }

        //if the player interacts with the basement door and has the key they are moved downstairs, if they dont they are prompted to find a key 
        if (gameObject.CompareTag("BasementDoor"))
        {
            if (hasKey)
            {
                _player.transform.position = new Vector3(-54, 1.5f, 0);
               
            }
            else
            {
                GameObject.Find("Text").GetComponent<Text>().text = "You need a key to open this door";
                GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                Invoke("RemoveText", 3f);
            }
        }

        //if the player finds the map a message is shown and they can advance to level 2
        if (gameObject.CompareTag("Map"))
        {
            hasMap = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found the map! Its time to venture out into the forest";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);
        }

        //if the player has the map, they can move to level 2, if they don't they are prompted to find the map
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
        if (gameObject.CompareTag("Lever") && !flippedLever)
        {
            flippedLever = true;
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