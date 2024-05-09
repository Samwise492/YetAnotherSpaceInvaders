public class GameStateChangedSignal
{
    public GameState state;

    public string GetGameStateText(GameState _state)
    {
        switch (_state)
        {
            case GameState.Defeat:
                return "You lost";
        }

        return "";
    }
}

public enum GameState
{
    None,
    Defeat
}