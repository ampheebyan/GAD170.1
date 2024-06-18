using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    // Player variables
    public float playerHealthPoints = 25.0f;
    public float playerMaxHealthPoints = 25.0f;

    public float playerAttackPoints = 3.0f;
    public float playerAttackModifier = 1.0f; // Kinda deprecated, I don't know what I was thinking with this.

    public int playerLevel = 1;

    public float playerXP = 0.0f;
    public float playerXPModifier = 1.0f;
    public float playerXPThreshold = 5.0f;

    public int player_backpack_healthPotion = 10;

    public GameManager _gameManager;

    public void RandomHealthPot()
    {
        int chance = Random.Range(1,10);
        if(chance >= 6)
        {
            _gameManager._cHandler.AppendToField("You found a health potion.", 3);
            player_backpack_healthPotion += 1;
            _gameManager._labelHandler.SetBackpackText(player_backpack_healthPotion);
        };
    }

    public void UseHealthPot()
    {
        if(player_backpack_healthPotion >= 1)
        {
            if(playerHealthPoints == playerMaxHealthPoints)
            {
                _gameManager._cHandler.AppendToField("You are at full health already.", 3);
            } else
            {
                float healAmount = Random.Range(5, 10);
                playerHealthPoints = Mathf.Clamp(playerHealthPoints + healAmount, 0, playerMaxHealthPoints);

                _gameManager._cHandler.AppendToField("You used the health potion and gained " + healAmount + " HP!", 3);
                player_backpack_healthPotion -= 1;
                _gameManager._labelHandler.SetBackpackText(player_backpack_healthPotion);
            }
        } else
        {
            _gameManager._cHandler.AppendToField("You don't have any health potions.", 3);
        }
    }

    public void IncreaseXP()
    {
        // Increase XP by 1 or 2 * xpModifier

        float tempXPVal = Random.Range(1, 2) * playerXPModifier;
        if (_gameManager.isDebug == true) Debug.Log("[debug] XP value " + tempXPVal + " rolled.");
        playerXP = playerXP + tempXPVal;
        _gameManager._cHandler.AppendToField("You gained " + tempXPVal + " XP.", 2);
    }

    public void LevelUp()
    {
        if (playerXP >= playerXPThreshold) // If player_xp is equal to or above player_xpThreshold, continue.
        {
            float tempThresVal = playerXPThreshold + Random.Range(2, 8);
            playerXPThreshold = tempThresVal; // Increase XP threshold by 2-8 XP.
            if (_gameManager.isDebug == true) Debug.Log("[debug] XP threshold value " + tempThresVal + " rolled.");

            float tempXPModVal = playerXPModifier + 0.25f;
            playerXPModifier = tempXPModVal; // Increase XP Modifier by 25%.
            if (_gameManager.isDebug == true) Debug.Log("[debug] XP modifier value " + tempXPModVal + ".");

            float tempAtk = (playerAttackPoints * 0.35f);
            playerAttackPoints = playerAttackPoints + (playerAttackPoints * 0.35f);
            if (_gameManager.isDebug == true) Debug.Log("[debug] Attack value increased by " + tempAtk + ".");

            float tempHealthPointVal = playerMaxHealthPoints + (playerLevel * 5);
            playerMaxHealthPoints = tempHealthPointVal; // Increase max health by player level * 5.
            float tempHealVal = playerHealthPoints * 0.50f;
            playerHealthPoints = Mathf.Clamp(playerHealthPoints + tempHealVal, 0, playerMaxHealthPoints);
            if (_gameManager.isDebug == true) Debug.Log("[debug] Health value " + tempHealthPointVal + ".");

            playerLevel = playerLevel + 1; // Increase player level.
            playerXP = 0f;
            _gameManager._cHandler.AppendToField("You are now level " + playerLevel + ".", 1);
        }
        else
        {
            if (_gameManager.isDebug == true) Debug.Log("[debug] Player only has " + playerXP + " when " + playerXPThreshold + " is required.");
            _gameManager._cHandler.AppendToField("You do not have enough XP to level up, you need " + (playerXPThreshold - playerXP) + " more XP to level up.", 2);
        }
    }
}
