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
    public float speed = 0.5f;
    public float runSpeed = 0.8f;
    public float walkSpeed = 0.5f;
    public int intelligence = 1;


    [Header("Combat")]
    public float attackDistance = 0.5f;
    public float attackTimer = 1;
    public float cooldown = 2f;
    public bool inCombat = false;
    public GameObject target;
    public bool comabatCoroutine = false;
    public bool dead = false;



}
