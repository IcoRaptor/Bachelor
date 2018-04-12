/// <summary>
/// Global access
/// </summary>
public static class Global { }

/// <summary>
/// The state the game is in
/// </summary>
public enum GAME_STATE
{
    DEFAULT,
    SHUTDOWN,
    FADING,
    MAIN_SCENE,
    TOWN_1,
}

/// <summary>
/// The type of log written to the console/file
/// </summary>
public enum LOG_TYPE
{
    LOG,
    WARNING,
    ERROR,
    ASSERT
}

/// <summary>
/// Indicates whether the seed is used in the build
/// </summary>
public enum SEED_TYPE
{
    NOT_IN_BUILD,
    IN_BUILD
}