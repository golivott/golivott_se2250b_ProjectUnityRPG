using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalMoney : MonoBehaviour
{
    private Text _totalMoney;
    
    private void Start()    //gets text component upon start
    {
        _totalMoney = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()   //updates the text based on how much money the player has
    {
        _totalMoney.text = "Money: " + GameObject.FindWithTag("Player").GetComponent<Player>().GetMoney() + "$";
    }
}
