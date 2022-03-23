using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour //class used to manage interactions
{
    private LevelOne _levelOne; //gets data for interactions in level 1

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _levelOne = GameObject.Find("Main Camera").GetComponent<LevelOne>(); //gets values from levelone script
        _player = GameObject.FindWithTag("Player").GetComponent<Player>(); //gets values from player script
    }

    public void
        Interact(GameObject gameObject) //interact method that uses gameobject tags to decide what is being interacted with
    {
        if (gameObject.CompareTag("Chest"))
        {
            _levelOne.hasKey = true;
            GameObject.Find("GotKey").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);

        }

        if (gameObject.CompareTag("BasementDoor"))
        {
            if (_levelOne.hasKey)
            {
                _player.transform.position = new Vector3(-54, 1.5f, 0);
                print("Entered basement");
            }
            else
            {
                print("You need a key to open the door");
            }
        }
    }

    public void RemoveText()
    {
        Destroy(GameObject.Find("GotKey"));
    }
}