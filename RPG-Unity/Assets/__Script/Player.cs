using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _health;
    private float _speed;
    private float _strength;
    private float _stamina;
    private float _resistance;
    private int _level;
    private int _experience;
    private int _money;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = 100f;
        _speed = 10f;
        _strength = 10f;
        _stamina = 100f;
        _resistance = 10f;
        _level = 0;
        _experience = 0;
        _money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
