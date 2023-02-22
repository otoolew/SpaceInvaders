// ----------------------------------------------------------------------------
//  University of Pittsburgh  
//  GamesEdu Workshop #2
//  19 SEPT 2018
// ----------------------------------------------------------------------------
using UnityEngine;
/// <summary>
/// Manages various UI Menus
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameOverMenu _gameOverMenu;

    private void Start()
    {       
        // Subscribe / Listen to the GameManager EventGameState OnGameStateChanged event  
        GameManager.Instance.onGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        _gameOverMenu.gameObject.SetActive(true);
    }
    
}
