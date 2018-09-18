using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GMData stores any data you would like to persist through out the life cycle of the game
/// </summary>
[CreateAssetMenu(fileName = "newGameManagerData", menuName = "GameManager/GameData")]
public class GMData : ScriptableObject
{
    public GameManager.GameState CurrentGameState;
}
