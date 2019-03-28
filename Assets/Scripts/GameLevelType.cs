using System;

public enum GameLevelType
{
    RANDOM,
    BLANK,
    DICE,
    CROSS
}

[Serializable]
public struct GameLevelAndType
{
    public GameLevelType Type;
    public GameWallHolder Level;
}

