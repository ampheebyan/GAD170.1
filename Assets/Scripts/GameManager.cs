using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* 
     * GameManager.cs, 2024 -- EXTENDED!
     * GAD170.1, 1033478
     */

    [SerializeField]
    private bool GameRunning = true; // Variable to exit game loop when finished

    [SerializeField]
    public bool isDebug = false; // For debug, I didn't include any debug stuff in the pseudocode, because admittedly I forgot about it, but I feel like that would be clunky to include.

    [Space(20)]
    public PlayerHandler _player;
    public EnemyHandler _enemyHandler;
    public LabelHandler _labelHandler;
    public CanvasHandler _cHandler;

    #region Game start
    void Start()
    {
        _cHandler.AppendToField("Welcome to <size=30>KINDA TEXT DOOM!</size> Press A to attack, Press H to use health potions, and press L to level up.", 0);
        _enemyHandler.NewEnemy();    
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

            if (_player.playerHealthPoints >= 1) // If HP is still above or equal to 1, continue loop.
            {
                if (_player.playerLevel >= 8) // If player level is above or equal to 8, end game.
                {
                    if (isDebug == true) Debug.Log("[debug] Player is level 8 or above.");
                    _cHandler.AppendToField("You are now level 8, <size=45>you win!</size>", 0);
                    GameRunning = false;
                } else // If neither conditions are met, continue loop.
                {
                    _labelHandler.SetPlayerText(_player.playerHealthPoints, _player.playerMaxHealthPoints, _player.playerAttackPoints, _player.playerXP, _player.playerXPThreshold, _player.playerLevel);
                    _labelHandler.SetBackpackText(_player.player_backpack_healthPotion);

                    if (Input.GetKeyDown(KeyCode.A)) // If press A, attack enemy.
                    {
                        if (isDebug == true) Debug.Log("[debug] Call AttackEnemy()");
                        _enemyHandler.AttackEnemy();
                    }
                    else if (Input.GetKeyDown(KeyCode.L)) // If press L, attempt to level up.
                    {
                        if (isDebug == true) Debug.Log("[debug] Call LevelUp()");
                        _player.LevelUp();
                    } else if (Input.GetKeyDown(KeyCode.H))
                    {
                        if (isDebug == true) Debug.Log("[debug] Call UseHealthPot()");
                        _player.UseHealthPot();
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
                _cHandler.AppendToField("You died. Game over.", 0); 
                GameRunning = false;
            }
        }
    }
    #endregion
}
