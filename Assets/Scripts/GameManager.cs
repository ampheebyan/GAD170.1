using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* 
     * GameManager.cs, 2024
     * GAD170.1, 1033478
     */

    [SerializeField]
    private bool GameRunning = true; // Variable to exit game loop when finished

    [SerializeField]
    private bool isDebug = false; // For debug, I didn't include any debug stuff in the pseudocode, because admittedly I forgot about it, but I feel like that would be clunky to include.

    [Space(20)]

    // Player variables
    public float playerHealthPoints = 20.0f;

    public float playerAttackPoints = 2.0f;

    public int playerLevel = 1;

    public float playerXP = 0.0f;
    public float playerXPModifier = 1.0f;
    public float playerXPThreshold = 5.0f;

    [Space(20)]

    // Enemy variables
    public float enemyHealthPoints = 15.0f;
    public float enemyMaxHealthPoints = 15.0f; // This is mostly there for display purposes. It just gets assigned the same value as enemyHealthPoints but doesn't get lowered.
    public float enemyAttackPoints = 2.0f;
    public int enemyLevel = 1; 
    
    #region Game start
    void Start()
    {
        Debug.Log("[GAME] Welcome to the game! Press A to attack, and press L to level up.");
        NewEnemy();    
    }
    #endregion

    #region Game loop
    void Update()
    {
        if(GameRunning == true)
        {
            /*
             * used if(GameRunning == true) in place of While gameRunning = True Do as it will do the same thing
             */

            if (playerHealthPoints >= 1) // If HP is still above or equal to 1, continue loop.
            {
                if (playerLevel >= 5) // If player level is above or equal to 5, end game.
                {
                    if (isDebug == true) Debug.Log("[debug] Player is level 5 or above.");
                    Debug.Log("[GAME] You win! Game over.");
                    GameRunning = false;
                } else // If neither conditions are met, continue loop.
                {
                    if (Input.GetKeyDown(KeyCode.A)) // If press A, attack enemy.
                    {
                        if (isDebug == true) Debug.Log("[debug] Call AttackEnemy()");
                        AttackEnemy();
                    }
                    else if (Input.GetKeyDown(KeyCode.L)) // If press L, attempt to level up.
                    {
                        if (isDebug == true) Debug.Log("[debug] Call LevelUp()");
                        LevelUp();
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        // Simple debug toggle.
                        isDebug = !isDebug;
                        Debug.Log(isDebug ? "DEGREELESSNESS MODE ON" : "DEGREELESSNESS MODE OFF");
                    }
                }
            } else
            {
                if (isDebug == true) Debug.Log("[debug] Player ran out of HP.");
                Debug.Log("[GAME] You died. Game over."); 
                GameRunning = false;
            }
        }
    }
    #endregion

    #region Enemy handling
    void NewEnemy()
    {
        // Set random variables for enemy, and let player know new enemy has shown up.
        enemyLevel = Random.Range(1, 5);
        if (isDebug == true) Debug.Log("[debug] Enemy level " + enemyLevel+ " rolled.");

        enemyHealthPoints = 10.0f + (enemyLevel * 5);
        enemyMaxHealthPoints = enemyHealthPoints;
        if (isDebug == true) Debug.Log("[debug] Enemy health value " + enemyHealthPoints + " rolled.");

        enemyAttackPoints = 2.0f * enemyLevel;
        if (isDebug == true) Debug.Log("[debug] Enemy attack value " + enemyAttackPoints + " rolled.");

        Debug.Log("[ENEMY] A new level " + enemyLevel + " imp appears from the bushes!");
    }

    void AttackEnemy()
    {
        float tempDmgVal = playerAttackPoints;
        if (isDebug == true) Debug.Log("[debug] Enemy damaged by value " + tempDmgVal + ".");

        enemyHealthPoints = enemyHealthPoints - tempDmgVal; // Damage enemy by attack point * attack modifier.

        if(enemyHealthPoints <= 0) // If enemy health below or equal to 0, increase XP and create new enemy.
        {
            Debug.Log("[ENEMY] You killed the imp!");

            if (isDebug == true) Debug.Log("[debug] Call IncreaseXP()");
            IncreaseXP();

            if (playerXP >= playerXPThreshold)
            { // This part is not in the pseudocode but I realised mid-project that this would probably be a bit vague to the user if there was no popup.
                Debug.Log("[XP] You have enough XP to level up. Press L to level up!");
            }

            if (isDebug == true) Debug.Log("[debug] Call NewEnemy()");
            NewEnemy();
        } else
        {
            Debug.Log("[IMP]: " + enemyHealthPoints + "/"+enemyMaxHealthPoints+" HP. " + enemyAttackPoints + " ATK.");
        }

    }
    #endregion

    #region Player XP/Level handling
    void IncreaseXP()
    {
        // Increase XP by 1 or 2 * xpModifier

        float tempXPVal = Random.Range(1, 2) * playerXPModifier;
        if (isDebug == true) Debug.Log("[debug] XP value "+tempXPVal+" rolled.");
        playerXP = playerXP + tempXPVal; 
        Debug.Log("[XP] You gained " + tempXPVal + " XP.");
    }

    void LevelUp()
    {
        if (playerXP >= playerXPThreshold) // If player_xp is equal to or above player_xpThreshold, continue.
        {
            float tempThresVal = playerXPThreshold + Random.Range(2, 8);
            playerXPThreshold = tempThresVal; // Increase XP threshold by 2-8 XP.
            if (isDebug == true) Debug.Log("[debug] XP threshold value " + tempThresVal + " rolled.");

            float tempXPModVal = playerXPModifier + 0.25f;
            playerXPModifier = tempXPModVal; // Increase XP Modifier by 25%.
            if (isDebug == true) Debug.Log("[debug] XP modifier value " + tempXPModVal + ".");

            float tempAttackModVal = playerAttackPoints + (playerAttackPoints * 0.25f);
            playerAttackPoints = tempAttackModVal; // Increase attack points by 25%.
            if (isDebug == true) Debug.Log("[debug] Attack points value increased to " + tempAttackModVal + ".");

            float tempHealthPointVal = playerHealthPoints + (playerLevel * 5);
            playerHealthPoints = tempHealthPointVal; // Increase health by player level * 5.
            if (isDebug == true) Debug.Log("[debug] Health value " + tempHealthPointVal + ".");

            playerLevel = playerLevel + 1; // Increase player level.
            Debug.Log("[LEVEL UP] You are now level" + playerLevel + ".");
        }
        else
        {
            if (isDebug == true) Debug.Log("[debug] Player only has " + playerXP + " when " + playerXPThreshold + " is required.");
            Debug.Log("[XP] You do not have enough XP to level up, you need "+(playerXPThreshold - playerXP) +" more XP to level up.");
        }
    }
    #endregion
}
