// ----------------------------------------------------------------------------
//  University of Pittsburgh
//  GamesEdu Workshop #2
//  19 SEPT 2018
// ----------------------------------------------------------------------------
using UnityEngine;
/// <summary>
/// GMData stores any data you would like to persist through out the life cycle of the game
/// </summary>
[CreateAssetMenu(fileName = "newGameManagerData", menuName = "GameManager/GameData")]
public class GMData : ScriptableObject
{
    public int HighScore;
}
