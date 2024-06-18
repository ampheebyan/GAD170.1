using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public string[] possibleEnemies = { "Imp", "Cacodemon", "Pinky Demon" };

    public Enemy currentEnemy;

    public GameManager _gameManager;
    public LabelHandler _labelHandler;
    public PlayerHandler _player;

    public void NewEnemy()
    {
        bool _temp = false;
        currentEnemy = new Enemy();
        currentEnemy.enemyName = possibleEnemies[Random.Range(0, possibleEnemies.Length)];

        switch(currentEnemy.enemyName)
        {
            // This is not the most ideal setup for this.
            case "Imp":
                currentEnemy.level = 1;
                currentEnemy.minLevel = 1;
                currentEnemy.maxLevel = 2;
                currentEnemy.healthPoints = 10;
                currentEnemy.maxHealthPoints = 10;
                currentEnemy.attackPoints = 1;
                currentEnemy.minAttackPoints = 1;
                currentEnemy.maxAttackPoints = 3;
                break;
            case "Cacodemon":
                if(_player.playerLevel >= 3)
                {
                    currentEnemy.level = 1;
                    currentEnemy.minLevel = 2;
                    currentEnemy.maxLevel = 4;
                    currentEnemy.healthPoints = 15;
                    currentEnemy.maxHealthPoints = 15;
                    currentEnemy.attackPoints = 1;
                    currentEnemy.minAttackPoints = 2;
                    currentEnemy.maxAttackPoints = 5;
                } else
                {
                    _temp = true; // If player is not level 3 or higher, stop. New enemy.
                    NewEnemy();
                }
                break;
            case "Pinky Demon":
                if(_player.playerLevel >= 6)
                {
                    currentEnemy.level = 1;
                    currentEnemy.minLevel = 3;
                    currentEnemy.maxLevel = 6;
                    currentEnemy.healthPoints = 20;
                    currentEnemy.maxHealthPoints = 20;
                    currentEnemy.attackPoints = 1;
                    currentEnemy.minAttackPoints = 3;
                    currentEnemy.maxAttackPoints = 7;
                } else
                {
                    _temp = true; // If player is not level 6 or higher, stop. New enemy.
                    NewEnemy();
                }
                break;
            default:
                currentEnemy.enemyName = "???";
                currentEnemy.level = 1;
                currentEnemy.minLevel = 1;
                currentEnemy.maxLevel = 1;
                currentEnemy.healthPoints = 1;
                currentEnemy.maxHealthPoints = 1;
                currentEnemy.attackPoints = 0;
                currentEnemy.minAttackPoints = 0;
                currentEnemy.maxAttackPoints = 0;
                break;
        }

        if (_temp == true) return; // Don't continue.

        currentEnemy.level = Random.Range(currentEnemy.minLevel, currentEnemy.maxLevel);
        if (_gameManager.isDebug == true) Debug.Log("[debug] Enemy level " + currentEnemy.level + " rolled.");

        currentEnemy.healthPoints = currentEnemy.healthPoints + (currentEnemy.level * 3);
        currentEnemy.maxHealthPoints = currentEnemy.healthPoints;
        if (_gameManager.isDebug == true) Debug.Log("[debug] Enemy health value " + currentEnemy.healthPoints + " rolled.");

        currentEnemy.attackPoints = Random.Range(currentEnemy.minAttackPoints, currentEnemy.maxAttackPoints);
        if (_gameManager.isDebug == true) Debug.Log("[debug] Enemy attack value " + currentEnemy.attackPoints + " rolled.");

        _labelHandler.SetEnemyText(currentEnemy.healthPoints, currentEnemy.maxHealthPoints, currentEnemy.attackPoints, currentEnemy.level, currentEnemy.enemyName);

        _gameManager._cHandler.AppendToField("A new level " + currentEnemy.level + " "+ currentEnemy.enemyName+" appears from the bushes!",5,"ENEMY");
    }

    public void AttackEnemy()
    {
        float tempDmgVal = _player.playerAttackPoints;
        if (_gameManager.isDebug == true) Debug.Log("[debug] Enemy damaged by value " + tempDmgVal + ".");

        currentEnemy.healthPoints = currentEnemy.healthPoints - tempDmgVal; // Damage enemy by attack point * attack modifier.
        _labelHandler.SetEnemyText(currentEnemy.healthPoints, currentEnemy.maxHealthPoints, currentEnemy.attackPoints, currentEnemy.level, currentEnemy.enemyName);

        if (currentEnemy.healthPoints <= 0) // If enemy health below or equal to 0, increase XP and create new enemy.
        {
            _gameManager._cHandler.AppendToField("You killed the " + currentEnemy.enemyName + "!", 5, currentEnemy.enemyName);

            if (_gameManager.isDebug == true) Debug.Log("[debug] Call IncreaseXP()");
            _player.IncreaseXP();
            _player.RandomHealthPot();

            if (_player.playerXP >= _player.playerXPThreshold)
            { // This part is not in the pseudocode but I realised mid-project that this would probably be a bit vague to the user if there was no popup.
                _gameManager._cHandler.AppendToField("You have enough XP to level up. Press L to level up!", 2);
            }

            if (_gameManager.isDebug == true) Debug.Log("[debug] Call NewEnemy()");
            NewEnemy();
        }
        else
        {
            float enemyTempDmgVal = currentEnemy.attackPoints;

            _player.playerHealthPoints -= enemyTempDmgVal;
            _gameManager._cHandler.AppendToField("Damaged you by " + enemyTempDmgVal + " HP.", 5, currentEnemy.enemyName);
        }
    }
}