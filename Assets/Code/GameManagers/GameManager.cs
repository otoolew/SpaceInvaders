// ----------------------------------------------------------------------------
//  University of Pittsburgh  
//  GamesEdu Workshop #2
//  19 SEPT 2018
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/// <summary>
/// GameManager manages all game systems
/// </summary>
public class GameManager : Singleton<GameManager>
{
    
    /// <summary>
    /// This is our ScriptableObject that serves as a container for our GameManager
    /// </summary>
    [Header("GameManager ScriptedAsset")]
    public GMData GMData;

    /// <summary>
    /// Enum to store our game manager instances state
    /// </summary>
    public enum GameState
    {
        RUNNING,
        PAUSED,
        SCENECHANGE,
        GAMEOVER
    }
    public SceneController sceneController;
    [SerializeField] private GameState gameState;
    public GameState CurrentGameState { get => gameState; set => gameState = value; }
    
    
    public UnityEvent onGameOver;

    
    /// <summary>
    /// Execute after Awake and before first Update. Init Here!
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(gameObject); // Tells Unity that we do not want to destroy this GameObject on Scene Load
        CurrentGameState = GameState.RUNNING; // Set the GameManager state to the stores state in GMData
    }
    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update()
    {
        // If scene is changing return
        if (CurrentGameState == GameState.SCENECHANGE)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    /// <summary>
    /// Toggles between the PAUSED State and the RUNNING State
    /// </summary>
    public void TogglePause()
    {
        //The ? conditional operator commonly known as the ternary conditional operator, returns one of two values depending on the value of a Boolean expression.
        CurrentGameState = CurrentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING;
        /*if (CurrentGameState == GameState.RUNNING)
        {
            CurrentGameState = GameState.PAUSED;
        }
        else
        {
            CurrentGameState = GameState.RUNNING;
        }*/
    }
    /// <summary>
    /// Loads the first game level
    /// </summary>
    public void StartGame()
    {
        // This can be implemented with a Dictionary, or many other more robust ways.
        sceneController.FadeAndLoadScene(sceneController.Scenes[1].sceneInfo.sceneName);
        // <-----> Simple way using stored variable declared above, prone to error if string value is a typo
        // sceneController.FadeAndLoadScene(gameScene);

        // <-----> Hard coded way prone to error if string value is a typo
        // sceneController.FadeAndLoadScene("gameScene");

    }
    /// <summary>
    /// Invokes the onGameOver event
    /// </summary>
    public void GameOver()
    {
        onGameOver.Invoke();
    }
    /// <summary>
    /// Restarts the current level
    /// </summary>
    public void RestartLevel()
    {
        sceneController.FadeAndLoadScene(sceneController.CurrentScene);
    }
    /// <summary>
    /// Quits the game and changes the scene to the TitleMenu
    /// </summary>
    public void QuitToTitle()
    {
        sceneController.FadeAndLoadScene(sceneController.Scenes[0].sceneInfo.sceneName);

        // <-----> Simple way using stored variable declared above, prone to error if string value is a typo
        // sceneController.FadeAndLoadScene(titleScene);

        // <-----> Hard coded way prone to error if string value is a typo
        // sceneController.FadeAndLoadScene("TitleScene");
    }
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        //These are called regions. When the C# compiler encounters an #if directive, 
        //  followed eventually by an #endif directive, it compiles the code between the 
        //  directives only if the specified symbol is defined. 
#if UNITY_STANDALONE //If we are running in a standalone build of the game
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene.
        UnityEditor.EditorApplication.isPlaying = false;
#endif      
    }
    
}
