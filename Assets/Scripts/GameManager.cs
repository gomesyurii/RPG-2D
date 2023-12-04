using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int CalculateHealth(Player player)
    {
        // Fórmula (resistence * 10) + (level * 4) + 10
        int result = (player.entity.resistence * 10) + (player.entity.level * 4) + 10;
        return result;
    }

    public int CalculateMana(Player player)
    {
        // Fórmula (intelligence * 10) + (level * 4) + 5
        int result = (player.entity.intelligence * 10) + (player.entity.level * 4) + 5;
        return result;
    }


    public int CalculateStamina(Player player)
    {
        // Fórmula (resistence * willPower) + (level * 4) + 5
        int result = (player.entity.resistence + player.entity.willPower) + (player.entity.level * 2) + 5;
        return result;
    }

    public int CalculateDamage(Player player, int weaponDamage)
    {
        // Fórmula (strength * 2) + (weaponDamage * 2) + (level * 3) + random(1-20)
        int result = (player.entity.strength * 2) + (weaponDamage * 2) + (player.entity.level * 3) + Random.Range(1, 20);
        return result;
    }

    public int CalculateDefense(Player player, int armorDefense)
    {
        // Fórmula (resistence * 2) + (level * 3) + armorDefense;
        int result = (player.entity.resistence * 2) + (player.entity.level * 3) + armorDefense;
        return result;
    }

}
