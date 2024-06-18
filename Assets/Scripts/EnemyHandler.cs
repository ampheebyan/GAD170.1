using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public string[] possibleEnemies = { "Imp", "Cacodemon", "Pinky Demon" };

    public Enemy currentEnemy;

    public GameManager _gameManager;
    public LabelHandler _labelHandler;

    public void NewEnemy()
    {

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
                currentEnemy.level = 1;
                currentEnemy.minLevel = 2;
                currentEnemy.maxLevel = 4;
                currentEnemy.healthPoints = 15;
                currentEnemy.maxHealthPoints = 15;
                currentEnemy.attackPoints = 1;
                currentEnemy.minAttackPoints = 2;
                currentEnemy.maxAttackPoints = 5;
                break;
            case "Pinky Demon":
                currentEnemy.level = 1;
                currentEnemy.minLevel = 3;
                currentEnemy.maxLevel = 6;
                currentEnemy.healthPoints = 20;
                currentEnemy.maxHealthPoints = 20;
                currentEnemy.attackPoints = 1;
                currentEnemy.minAttackPoints = 5;
                currentEnemy.maxAttackPoints = 7;
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
}
