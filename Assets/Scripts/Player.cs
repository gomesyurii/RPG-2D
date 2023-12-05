using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Entity entity;
    [Header("Player regen system")]
    public bool regenHpEnabled = true;
    public bool regenManaEnabled = true;
    public bool regenStaminaEnabled = true;
    public float regenHpTime = 5f;
    public int regenHpAmount = 5;
    public float regenStaminaTime = 3f;
    public int regenStaminaAmount;

    public float regenManaTime = 2f;
    public int regenManaAmount = 5;

    [Header("Game Manager")]
    public GameManager manager;

    [Header("Player UI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;
    public Slider exp;
    void Start()
    {
        if (manager == null)
        {
            Debug.LogError("game manager not found");
            return;
        }

        entity.maxHealth = manager.CalculateHealth(entity);
        entity.maxMana = manager.CalculateMana(entity); 
        entity.maxStamina = manager.CalculateStamina(entity);

        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        health.maxValue = entity.maxHealth;
        health.value = health.maxValue;

        mana.maxValue = entity.maxMana;
        mana.value = mana.maxValue;

        stamina.maxValue = entity.maxStamina;
        stamina.value = stamina.maxValue;

        exp.value = 0;

        StartCoroutine(RegenHealth());
        StartCoroutine(RegenMana());
        StartCoroutine(RegenStamina());

    }

    void Update()
    {
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;

        
        if (entity.currentHealth <= 0)
        {
            entity.currentHealth = 0;
            entity.dead = true;
        }
        

    }

    IEnumerator RegenHealth()
    {
        regenHpAmount = ((entity.willPower + entity.resistence)/10);
        while (true)
        {
            if (regenHpEnabled)
            {
                if (entity.currentHealth < entity.maxHealth)
                {
                    entity.currentHealth += regenHpAmount;
                    yield return new WaitForSeconds(regenHpTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }

        }
    }

    IEnumerator RegenMana()
    {
        regenManaAmount = ((entity.intelligence) + (entity.level * 2) + 2)/4;
        while (true)
        {
            if (regenManaEnabled)
            {
                if (entity.currentMana < entity.maxMana)
                {
                    entity.currentMana += regenManaAmount;
                    yield return new WaitForSeconds(regenManaTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }

        }
    }

    IEnumerator RegenStamina()
    {
        regenStaminaAmount = (((entity.resistence + entity.willPower) / 2) + entity.level);
        while (true)
        {
            if (regenStaminaEnabled)
            {
                if (entity.currentStamina < entity.maxStamina)
                {
                    entity.currentStamina += regenStaminaAmount;
                    yield return new WaitForSeconds(regenStaminaTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }

        }
    }


}
