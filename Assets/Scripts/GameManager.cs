using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int CalculateHealth(Entity entity)
    {
        // Fórmula (resistence * 10) + (level * 4) + 10
        int result = (entity.resistence * 10) + (entity.level * 4) + 10;
        return result;
    }

    public int CalculateMana(Entity entity)
    {
        // Fórmula (intelligence * 10) + (level * 4) + 5
        int result = (entity.intelligence * 10) + (entity.level * 4) + 5;
        return result;
    }


    public int CalculateStamina(Entity entity)
    {
        // Fórmula (resistence * willPower) + (level * 4) + 5
        int result = (entity.resistence + entity.willPower) + (entity.level * 2) + 5;
        return result;
    }

    public int CalculateDamage(Entity entity, int weaponDamage)
    {
        // Fórmula (strength * 2) + (weaponDamage * 2) + (level * 3) + random(1-20)
        int result = (entity.strength * 2) + (weaponDamage * 2) + (entity.level * 3) + Random.Range(1, 20);
        return result;
    }

    public int CalculateDefense(Entity entity, int armorDefense)
    {
        // Fórmula (resistence * 2) + (level * 3) + armorDefense;
        int result = (entity.resistence * 2) + (entity.level * 3) + armorDefense;
        return result;
    }

}
