using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool GameRunning = true;

    float player_healthPoints = 20.0f;
    float player_attackPoints = 2.0f;
    float player_attackModifier = 1.0f;
    float player_xp = 0.0f;
    int player_level = 1; 
    float player_xpModifier = 1.0f;
    float player_xpThreshold = 5.0f;

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

            if (player_healthPoints >= 1)
            {
                if (player_level >= 5)
                {
                    Debug.Log("You win! Game over.");
                    GameRunning = false;
                } else
                {
                    if(Input.GetKeyDown(KeyCode.A))
                    {
                        AttackEnemy();
                    } else if(Input.GetKeyDown(KeyCode.L))
                    {
                        LevelUp();
                    }
                }
            } else
            {
                Debug.Log("You died. Game over.");
            }
        }
    }
    #endregion

    #region Enemy handling
    void NewEnemy()
    {
        enemy_level = Random.Range(1, 5);
        enemy_healthPoints = 10.0f + (enemy_level * 5);
        enemy_attackPoints = 2.0f * enemy_level;
        Debug.Log("A new level " + enemy_level + " imp appears!");
    }

    void AttackEnemy()
    {
        enemy_healthPoints = enemy_healthPoints - (player_attackPoints * player_attackModifier);

        if(enemy_healthPoints <= 0)
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
        float tempXPVal = Random.Range(1, 2) * player_xpModifier;
        player_xp = player_xp + tempXPVal;
        Debug.Log("You gained " + tempXPVal + " XP!");
    }

    void LevelUp()
    {
        if (player_xp >= player_xpThreshold)
        {
            player_xpThreshold = player_xpThreshold + Random.Range(2, 5);
            player_xpModifier = player_xpModifier + 0.25f;
            player_attackModifier = player_attackModifier + 0.25f;
            player_healthPoints = player_healthPoints + (player_level * 5);
            player_level = player_level + 1;
            Debug.Log("You leveled up! You are now level" + player_level + ".");
        }
        else
        {
            Debug.Log("You do not have enough XP to level up.");
        }
    }
    #endregion
}
