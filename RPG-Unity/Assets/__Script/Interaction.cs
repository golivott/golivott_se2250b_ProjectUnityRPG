using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private LevelOne _levelOne;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _levelOne = GameObject.Find("Main Camera").GetComponent<LevelOne>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject gameObject)
    {
        if(gameObject.CompareTag("Chest"))
        {
            _levelOne.hasKey = true;
            print("Got Key");
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
}

