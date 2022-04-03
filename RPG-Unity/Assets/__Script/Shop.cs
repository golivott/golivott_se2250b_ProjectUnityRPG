using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Button button;
    private Player _player;
    private Text _text;

    private void Start()    //upon start get the player component and text component
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _text = button.transform.GetChild(0).GetComponent<Text>();
    }

    public void BuyItem()   //method used to buy items by checking the text of the button and comparing it in if statements
    {
        
        //Some items can be bought multiple times (potions) other items are a one time buy
        
        if (_text.text.Equals("Helmet"))
        {
            
            if (_player.GetMoney() >= 100)
            {
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Chestplate"))
        {
            if (_player.GetMoney() >= 100)
            {
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Leggings"))
        {
            if (_player.GetMoney() >= 100)
            {
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Boots"))
        {
            if (_player.GetMoney() >= 100)
            {
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Ruby Ring"))
        {
            if (_player.GetMoney() >= 500)
            {
                _player.AddMoney(-500);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Emerald Ring"))
        {
            if (_player.GetMoney() >= 500)
            {
                _player.AddMoney(-500);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Magical Gloves"))
        {
            if (_player.GetMoney() >= 300)
            {
                _player.AddMoney(-300);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Sapphire Necklace"))
        {
            if (_player.GetMoney() >= 500)
            {
                _player.AddMoney(-500);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Health Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Speed Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Strength Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Resistance Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Upgrade Sword"))
        {
            if (_player.GetMoney() >= 750)
            {
                _player.AddMoney(-750);
                button.interactable = false;
            }
        }
    }

    public void OnHover()   //method that displays item info when the user hovers over it
    {
        button.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void OffHover()  //removes the item info
    {
        button.transform.GetChild(2).gameObject.SetActive(false);
    }
    
    
    
}
