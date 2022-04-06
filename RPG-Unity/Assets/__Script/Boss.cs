using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    //prefabs for bosses moves
    public GameObject skeleton;
    public GameObject caution;
    
    //bools that control the bosses moves
    private bool _canSpawnMinions;
    private bool _canSpawnMeteors;
    
    // Start is called before the first frame update
    void Start()
    {
        //bosses stats
        Health = 5000f;
        Speed = 4f;
        Radius = 100f;
        Power = 25f;
        Experience = 200;
        EnemyName = "GrandpaBoss";
        Money = 500;
        MoveEnemy = true;
        _canSpawnMinions = true;
        _canSpawnMeteors = true;
    }
    
    
    public override void Update()   //overrided update method that that uses the same undetected movement but also controls when the boss can use his enemies
    {
        if (MoveEnemy)     //if the enemy can move
        {
            MoveEnemy = false;
            Invoke("UnDetectedMovement", 1f);
        }
        
        if (_canSpawnMinions)   //if the bosses attack cooldown is over he can spawn more enemies
        {
            _canSpawnMinions = false;
            SummonMinions();
            StartCoroutine(CanSummonMinions());
        }

        if (_canSpawnMeteors)   //if the bosses attack cooldown is over he can spawn meteors
        {
            _canSpawnMeteors = false;
            SummonMeteors();
            StartCoroutine(CanSummonMeteors());
        }
        
        if (Health <= 0)   //if the bosses health is less than zero he dies
        {
            GameObject.Find("Text").GetComponent<Text>().text = "Congratulations! You Win!";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);
            KillEnemy();
        }
    }
    public void SummonMinions()     //method that spawns skeletons in the boss arena
    {
        List<int[]> coordinates = new List<int[]>();    //creates a list that stores coordinates where skeletons spawn
        
        for (int c = 0; c < 5; c++)     //spawns 5 skeletons
        {
            int[] coords = GenerateCoordinates();   //generates an array with index 0 as x and index 1 as y

            while(coordinates.Contains(coords)) //if the coordinate is taking, use linear probing to find a coordinate that isn't taken
            {
                if (coords[0] == -48)
                {
                    coords[0] = -76;
                }
                if (coords[1] == 24)
                {
                    coords[1] = 3;
                }
                
                coords[0] += 4;
                coords[1] += 3;
            }
            coordinates.Add(coords);    //add the coordinate to the list of taken spots
            Instantiate(skeleton, new Vector3(coords[0], coords[1], 0), Quaternion.identity);   //instantiates the skeleton in the arena
        }
    }
    public void SummonMeteors()     //method that summons meteors
    {
        StartCoroutine(CreateMeteors());
    }

    // Update is called once per frame
    public IEnumerator CanSummonMinions()   //allows boss to spawn skeletons again after 20 seconds
    {
        yield return new WaitForSecondsRealtime(20f);
        _canSpawnMinions = true;
    }
    
    public IEnumerator CanSummonMeteors()   //allows boss to spawn 3 meteors after 6 seconds
    {
        yield return new WaitForSecondsRealtime(6f);
        _canSpawnMeteors = true;
    }

    public IEnumerator CreateMeteors()  //Method that generates 3 random locations for meteors to spawn, a caution marker is placed
    {
        List<int[]> coordinates = new List<int[]>();
        for (int c = 0; c < 3; c++)
        {
            int[] coords = GenerateCoordinates();

            while (coordinates.Contains(coords))    //using linear probing to generate a new coordinate if a coordinate is already taken
            {
                print("Entered");
                if (coords[0] == -52)
                {
                    coords[0] = -72;
                }

                if (coords[1] == 28)
                {
                    coords[1] = 6;
                }

                coords[0] += 4;
                coords[1] += 3;
            }
            Instantiate(caution, new Vector3(coords[0], coords[1], 0), Quaternion.identity);    //places the marker
            yield return new WaitForSecondsRealtime(2f);    //spawn another marker after 2 seconds
        }
    }

    private int[] GenerateCoordinates() //method that generates a random x and y coordinate
    {
        int x = 0;
        int y = 0;
        x = Random.Range(-68, -53);
            while (x % 4 != 0)
            {
                x++;
            }

        y = Random.Range(9, 22);
            while (y % 3 != 0)
            {
                y++;
            }

            return new int[]{x, y};
    }
    
    public void RemoveText()    //method that removes text by making it invisible using colors
    {
        GameObject.Find("Text").GetComponent<Text>().color = new Color(0, 0, 0, 0);
    }
    
}
