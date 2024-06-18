using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    // Player variables
    public float playerHealthPoints = 20.0f;
    public float playerMaxHealthPoints = 20.0f;

    public float playerAttackPoints = 2.0f;
    public float playerAttackModifier = 1.0f;

    public int playerLevel = 1;

    public float playerXP = 0.0f;
    public float playerXPModifier = 1.0f;
    public float playerXPThreshold = 5.0f;

    public GameManager _gameManager;

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

            float tempAttackModVal = playerAttackModifier + 0.25f;
            playerAttackModifier = tempAttackModVal; // Increase attack modifier by 25%.
            if (_gameManager.isDebug == true) Debug.Log("[debug] Attack modifier value " + tempAttackModVal + ".");

            float tempHealthPointVal = playerMaxHealthPoints + (playerLevel * 5);
            playerMaxHealthPoints = tempHealthPointVal; // Increase max health by player level * 5.
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
