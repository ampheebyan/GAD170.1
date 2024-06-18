using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class LabelHandler : MonoBehaviour
{
    public TMP_Text _playerText;
    public TMP_Text _enemyText;

    public void SetEnemyText(float hp, float mHP, float atk, int level, string name)
    {
        _enemyText.text = "<b>" + name + "</b>\n<b>HP: </b>" + hp + "/" + mHP + "\n<b>ATK: </b>" + atk + "\n<b>Level: </b>" + level;
    }
    public void SetPlayerText(float hp, float mHP, float atk, float xp, float xpT, int level)
    {
        _playerText.text = "<b>HP: </b>" + hp + "/" + mHP + "\n<b>ATK: </b>" + atk + "\n<b>Level: </b>" + level + "\n<b>XP: </b>" + xp+"/"+xpT;
    }

}
