using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    public GameObject skeleton;
    public GameObject caution;
    private bool _canSpawnMinions;
    private bool _canSpawnMeteors;
    
    // Start is called before the first frame update
    void Start()
    {
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
    
    public override void Update()
    {
        if (MoveEnemy)     //if the enemy can move
        {
            MoveEnemy = false;
            Invoke("UnDetectedMovement", 1f);
        }
        
        if (_canSpawnMinions)
        {
            _canSpawnMinions = false;
            SummonMinions();
            StartCoroutine(CanSummonMinions());
        }

        if (_canSpawnMeteors)
        {
            _canSpawnMeteors = false;
            SummonMeteors();
            StartCoroutine(CanSummonMeteors());
        }
        
        if (Health <= 0)   //if the enemy has less than zero health it dies
        {
            KillEnemy();
        }
    }
    public void SummonMinions()
    {
        List<int[]> coordinates = new List<int[]>();
        
        for (int c = 0; c < 5; c++)
        {
            int[] coords = GenerateCoordinates();

            while(coordinates.Contains(coords))
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
            coordinates.Add(coords);
            Instantiate(skeleton, new Vector3(coords[0], coords[1], 0), Quaternion.identity);
        }
    }
    public void SummonMeteors()
    {
        StartCoroutine(CreateMeteors());
    }

    // Update is called once per frame
    public IEnumerator CanSummonMinions()
    {
        yield return new WaitForSecondsRealtime(20f);
        _canSpawnMinions = true;
    }
    
    public IEnumerator CanSummonMeteors()
    {
        yield return new WaitForSecondsRealtime(6f);
        _canSpawnMeteors = true;
    }

    public IEnumerator CreateMeteors()
    {
        List<int[]> coordinates = new List<int[]>();
        for (int c = 0; c < 3; c++)
        {
            int[] coords = GenerateCoordinates();

            while (coordinates.Contains(coords))
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
            Instantiate(caution, new Vector3(coords[0], coords[1], 0), Quaternion.identity);
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    private int[] GenerateCoordinates()
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
    
}
