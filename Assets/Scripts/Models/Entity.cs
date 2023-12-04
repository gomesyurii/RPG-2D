using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Entity
{
    [Header("Name")]
    public string name;
    public int level;



    [Header("Health")]
    public int currentHealth;
    public int maxHealth;


    [Header("Mana")]
    public int currentMana;
    public int maxMana;


    [Header("Stamina")]
    public float currentStamina;
    public float maxStamina;


    [Header("Stats")]
    public int strength = 1;
    public int resistence = 1;
    public int willPower = 1;
    public int damage = 1;
    public int defense = 1;
    public int speed = 2;
    public int runSpeed = 4;

    public int walkSpeed = 2;
    
    
    public int intelligence = 1;

}
