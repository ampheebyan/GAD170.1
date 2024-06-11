using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool GameRunning = true; // Variable to exit game loop when finished

    // Player variables
    float player_healthPoints = 20.0f;
    
    float player_attackPoints = 2.0f;
    float player_attackModifier = 1.0f;

    int player_level = 1; 

    float player_xp = 0.0f;
    float player_xpModifier = 1.0f;
    float player_xpThreshold = 5.0f;

    // Enemy variables
    float enemy_healthPoints = 15.0f;
    float enemy_attackPoints = 2.0f;
    int enemy_level = 1; 
    
    #region Game start
    void Start()
    {
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

            if (player_healthPoints >= 1) // If HP is still above or equal to 1, continue loop.
            {
                if (player_level >= 5) // If player level is above or equal to 5, end game.
                {
                    Debug.Log("You win! Game over.");
                    GameRunning = false;
                } else // If neither conditions are met, continue loop.
                {
                    if(Input.GetKeyDown(KeyCode.A)) // If press A, attack enemy.
                    {
                        AttackEnemy();
                    } else if(Input.GetKeyDown(KeyCode.L)) // If press L, attempt to level up.
                    {
                        LevelUp();
                    }
                }
            } else
            {
                Debug.Log("You died. Game over."); 
                GameRunning = false;
            }
        }
    }
    #endregion

    #region Enemy handling
    void NewEnemy()
    {
        // Set random variables for enemy, and let player know new enemy has shown up.
        enemy_level = Random.Range(1, 5);
        enemy_healthPoints = 10.0f + (enemy_level * 5);
        enemy_attackPoints = 2.0f * enemy_level; 
        Debug.Log("A new level " + enemy_level + " imp appears!");
    }

    void AttackEnemy()
    {
        enemy_healthPoints = enemy_healthPoints - (player_attackPoints * player_attackModifier); // Damage enemy by attack point * attack modifier.

        if(enemy_healthPoints <= 0) // If enemy health below or equal to 0, increase XP and create new enemy.
        {
            Debug.Log("You killed the imp!");
            IncreaseXP();
            NewEnemy();
        } else
        {
            Debug.Log("IMP: " + enemy_healthPoints + " hp, " + enemy_attackPoints + " attack.");
        }

    }
    #endregion

    #region Player XP/Level handling
    void IncreaseXP()
    {
        // Increase XP by 1 or 2 * xpModifier

        float tempXPVal = Random.Range(1, 2) * player_xpModifier;
        player_xp = player_xp + tempXPVal; 
        Debug.Log("You gained " + tempXPVal + " XP!");
    }

    void LevelUp()
    {
        if (player_xp >= player_xpThreshold) // If player_xp is equal to or above player_xpThreshold, continue.
        {
            player_xpThreshold = player_xpThreshold + Random.Range(2, 8); // Increase XP threshold by 2-8 XP.
            player_xpModifier = player_xpModifier + 0.25f; // Increase XP Modifier by 25%.
            player_attackModifier = player_attackModifier + 0.25f; // Increase attack modifier by 25%.
            player_healthPoints = player_healthPoints + (player_level * 5); // Increase health by player level * 5.
            player_level = player_level + 1; // Increase player level.
            Debug.Log("You leveled up! You are now level" + player_level + ".");
        }
        else
        {
            Debug.Log("You do not have enough XP to level up.");
        }
    }
    #endregion
}
