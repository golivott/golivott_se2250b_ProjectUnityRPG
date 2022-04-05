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

    public static bool HasHelmet;
    public static bool HasRubyRing;
    public static bool HasNecklace;
    

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
                HasHelmet = true;
                _player.SetResistance(_player.GetResistance() * 0.95f);
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Chestplate"))
        {
            if (_player.GetMoney() >= 100)
            {
                _player.SetResistance(_player.GetResistance() * 0.8f);
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Leggings"))
        {
            if (_player.GetMoney() >= 100)
            {
                _player.SetResistance(_player.GetResistance() * 0.8f);
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Boots"))
        {
            if (_player.GetMoney() >= 100)
            {
                _player.SetResistance(_player.GetResistance() * 0.95f);
                _player.SetActiveMoveSpeed(_player.GetActiveMoveSpeed() * 1.5f);
                _player.SetSpeed(_player.GetActiveMoveSpeed());
                _player.AddMoney(-100);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Ruby Ring"))
        {
            if (_player.GetMoney() >= 500)
            {
                HasRubyRing = true;
                _player.AddMoney(-500);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Emerald Ring"))
        {
            if (_player.GetMoney() >= 500)
            {
                _player.SetStrength(_player.GetStrength() + 12.5f);
                _player.AddMoney(-500);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Magical Gloves"))
        {
            if (_player.GetMoney() >= 300)
            {
                _player.GetComponent<Player>().attack1Delay = _player.GetComponent<Player>().attack1Delay * 0.75f;
                _player.GetComponent<Player>().attack2Delay = _player.GetComponent<Player>().attack2Delay * 0.75f;
                _player.GetComponent<Player>().ability1Delay = _player.GetComponent<Player>().ability1Delay * 0.75f;
                _player.GetComponent<Player>().ability2Delay= _player.GetComponent<Player>().ability2Delay * 0.75f;
                _player.AddMoney(-300);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Sapphire Necklace"))
        {
            if (_player.GetMoney() >= 500)
            {
                HasNecklace = true;
                _player.SetResistance(_player.GetResistance() * 0.8f);
                _player.AddMoney(-500);
                button.interactable = false;
            }
        }
        
        if (_text.text.Equals("Health Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
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
                GameObject.Find("ItemAmount").GetComponent<Text>().text =
                    "X " + _player.GetComponent<Player>().Inventory["HealthPotion"];
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Speed Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
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
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Strength Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
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
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Resistance Potion"))
        {
            if (_player.GetMoney() >= 75)
            {
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
                _player.AddMoney(-75);
            }
        }
        
        if (_text.text.Equals("Upgrade Sword") || _text.text.Equals("Upgrade Bow") || _text.text.Equals("Upgrade Fists"))
        {
            if (_player.GetMoney() >= 750)
            {
                _player.GetComponent<Player>().attack1Damage = _player.GetComponent<Player>().attack1Damage * 1.25f;
                _player.GetComponent<Player>().attack2Damage = _player.GetComponent<Player>().attack2Damage * 1.25f;
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
