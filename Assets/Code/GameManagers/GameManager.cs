using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// This is our ScriptableObject that serves as a container for our GameManager
    /// </summary>
    [Header("GameManager ScriptedAsset")]
    public GMData GMData;

    public string titleScene;
    public string startingScene;
    public string currentScene;

    /// <summary>
    /// Enum to store our game manager instances state
    /// </summary>
    public enum GameState
    {
        STARTMENU,
        RUNNING,
        PAUSED,
        SCENECHANGE,
        GAMEOVER
    }
    public SceneController sceneController;

    /// <summary>
    /// Event that will listen for Methods that trigger a game state change. This is the "Hook" other componets will be looking for.
    /// </summary>
    public EventGameState OnGameStateChanged;
    /// <summary>
    /// GameState enum to store GameState
    /// </summary>
    GameState _currentGameState = GameState.RUNNING;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }
    /// <summary>
    /// Execute after Awake and before first Update. Init Here!
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(gameObject); // Tells Unity that we do not want to destroy this GameObject on Scene Load
        GMData.CurrentGameState = _currentGameState; // Set the GameManager state to the stores state in GMData
        sceneController.OnSceneChangeStart.AddListener(HandleSceneChangeStart); // Subscribe or Listen for EventSceneChangeStart
        sceneController.OnSceneChangeComplete.AddListener(HandleSceneChangeComplete);// Subscribe or Listen for EventSceneChangeComplete
        OnGameStateChanged.Invoke(GMData.CurrentGameState, _currentGameState); // Let ALL other components listening for EventGameStateChange that the GameState has changed
    }
    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update()
    {
        // If scene is changing return
        if (_currentGameState == GameState.SCENECHANGE)
            return;
        
    }

    /// <summary>
    /// Updates GameState. Is call when another script triggers a state change.
    /// </summary>
    /// <param name="state"></param>
    public void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState; // store previous state
        _currentGameState = state; // set current state to the new state
        GMData.CurrentGameState = _currentGameState; // store the current state in our persistent data container

        // Switch to handle what needs to execute for each state 
        switch (CurrentGameState)
        {
            case GameState.SCENECHANGE:
                // Initialize any systems that need to be reset
                Debug.Log("Scene Changing");
                Time.timeScale = 1.0f; // Time.timeScale will slow down time or bring it to a complete stop. 
                break;

            case GameState.RUNNING:
                //  Unlock player, enemies and input in other systems, update tick if you are managing time
                Debug.Log("Game Running");
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                // Pause player, enemies etc, Lock other input in other systems
                Debug.Log("Game Paused");
                Time.timeScale = 0.0f; // Time.timeScale will stop the game. Paused!
                break;
            case GameState.GAMEOVER:
                // Implement Actions like - Pause player, enemies etc, Lock other input in other systems. 
                Debug.Log("Game OVER");
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }
        // When this executes it will Notify ALL Scripts that are listening for the EventGameState
        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    public void TogglePause()
    {
        //UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
        if (_currentGameState == GameState.RUNNING)
            UpdateState(GameState.PAUSED);
        else
            UpdateState(GameState.RUNNING);
    }

    public void StartGame()
    {
        sceneController.FadeAndLoadScene(sceneController.Scenes[1].sceneName);
    }

    public void RestartLevel()
    {
        sceneController.FadeAndLoadScene(sceneController.CurrentScene);
    }

    public void QuitToTitle()
    {
        sceneController.FadeAndLoadScene(sceneController.Scenes[0].sceneName);
    }

    public void QuitGame()
    {
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif      
    }
    /// <summary>
    /// When the event EventSceneChangeStart fires, this method is executed.
    /// </summary>
    /// <param name="started"></param>
    public void HandleSceneChangeStart(bool started)
    {
        Debug.Log("[GameManager] Scene Change Start.");
        UpdateState(GameState.SCENECHANGE);
    }
    /// <summary>
    /// When the event EventSceneChangeComplete fires, this method is executed.
    /// </summary>
    public void HandleSceneChangeComplete(bool complete)
    {
        Debug.Log("[GameManager] Scene Change Complete."); 
        UpdateState(GameState.RUNNING); // The Scene change is complete so set the game state to run.
    }
    /// <summary>
    /// Take 2 Game State Parameters 
    /// </summary>
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
}
