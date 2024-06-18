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

    [Space(20)]

    // Enemy variables
    public float enemyHealthPoints = 15.0f;
    public float enemyMaxHealthPoints = 15.0f; // This is mostly there for display purposes. It just gets assigned the same value as enemyHealthPoints but doesn't get lowered.
    public float enemyAttackPoints = 2.0f;
    public int enemyLevel = 1;

    [Space(20)]

    public CanvasHandler _cHandler;

    #region Game start
    void Start()
    {
        _cHandler.AppendToField("Welcome to the game! Press A to attack, and press L to level up.", 0);
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
                if (_player.playerLevel >= 5) // If player level is above or equal to 5, end game.
                {
                    if (isDebug == true) Debug.Log("[debug] Player is level 5 or above.");
                    _cHandler.AppendToField("You win! Game over.", 0);
                    GameRunning = false;
                } else // If neither conditions are met, continue loop.
                {
                    _labelHandler.SetPlayerText(_player.playerHealthPoints, _player.playerMaxHealthPoints, _player.playerAttackPoints, _player.playerXP, _player.playerXPThreshold, _player.playerLevel);
                    if (Input.GetKeyDown(KeyCode.A)) // If press A, attack enemy.
                    {
                        if (isDebug == true) Debug.Log("[debug] Call AttackEnemy()");
                        AttackEnemy();
                    }
                    else if (Input.GetKeyDown(KeyCode.L)) // If press L, attempt to level up.
                    {
                        if (isDebug == true) Debug.Log("[debug] Call LevelUp()");
                        _player.LevelUp();
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

    #region Enemy handling
    void AttackEnemy()
    {
        float tempDmgVal = _player.playerAttackPoints * _player.playerAttackModifier;
        if (isDebug == true) Debug.Log("[debug] Enemy damaged by value " + tempDmgVal + ".");

        _enemyHandler.currentEnemy.healthPoints = _enemyHandler.currentEnemy.healthPoints - tempDmgVal; // Damage enemy by attack point * attack modifier.
        _labelHandler.SetEnemyText(_enemyHandler.currentEnemy.healthPoints, _enemyHandler.currentEnemy.maxHealthPoints, _enemyHandler.currentEnemy.attackPoints, _enemyHandler.currentEnemy.level, _enemyHandler.currentEnemy.enemyName);

        if (_enemyHandler.currentEnemy.healthPoints <= 0) // If enemy health below or equal to 0, increase XP and create new enemy.
        {
            _cHandler.AppendToField("[ENEMY] You killed the "+_enemyHandler.currentEnemy.enemyName+"!", 5, "ENEMY");

            if (isDebug == true) Debug.Log("[debug] Call IncreaseXP()");
            _player.IncreaseXP();

            if (_player.playerXP >= _player.playerXPThreshold)
            { // This part is not in the pseudocode but I realised mid-project that this would probably be a bit vague to the user if there was no popup.
                _cHandler.AppendToField("[XP] You have enough XP to level up. Press L to level up!", 2);
            }

            if (isDebug == true) Debug.Log("[debug] Call NewEnemy()");
            _enemyHandler.NewEnemy();
        }
        else
        {
            _cHandler.AppendToField(_enemyHandler.currentEnemy.healthPoints + "/"+_enemyHandler.currentEnemy.maxHealthPoints+" HP. " + _enemyHandler.currentEnemy.attackPoints + " ATK.", 5, _enemyHandler.currentEnemy.enemyName);
        }

    }
    #endregion
}
