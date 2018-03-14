using Framework;
using Framework.Debugging;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains game logic
/// </summary>
public sealed class GameManager : SingletonAsComponent<GameManager>
{
    #region Variables

    private GAME_STATE _gameState = GAME_STATE.DEFAULT;
    private GAME_STATE _oldState = GAME_STATE.DEFAULT;

    #endregion

    #region Properties

    public static GameManager Instance
    {
        get { return (GameManager)_Instance; }
    }

    public GAME_STATE GameState
    {
        get { return _gameState; }
    }

    public GAME_STATE OldState
    {
        get { return _oldState; }
    }

    #endregion

    /// <summary>
    /// Sets a new GameState
    /// </summary>
    /// <param name="newState">New GameState</param>
    public void SetGameState(GAME_STATE newState)
    {
        if (_gameState != newState)
        {
            OnGameStateChange(newState);
            _oldState = _gameState;
            _gameState = newState;
        }
        else
        {
            Debugger.LogFormat(LOG_TYPE.LOG, "GameState is already \"{0}\"!\n",
                newState);
        }
    }

    /// <summary>
    /// Called when the GameState changes
    /// </summary>
    /// <param name="newState">The new state</param>
    private void OnGameStateChange(GAME_STATE newState)
    {
        switch (newState)
        {
            case GAME_STATE.PLAYING:
                SceneManager.LoadScene(1);
                break;

            case GAME_STATE.END_GAME:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                UnityEngine.Application.Quit();
#endif
                break;

            default:
                Debugger.LogFormat(LOG_TYPE.WARNING, "Unknown GameState: {0}\n",
                    newState);
                break;
        }
    }
}