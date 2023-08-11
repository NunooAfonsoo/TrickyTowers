using UnityEngine;

public static class Constants
{
    public const string FLOOR_TAG = "Floor";
    public const int PIECES_PATH_HEIGHT = 100;


    public const float INITIAL_PIECE_MASS = 1;

    public static readonly Vector2 SPAWN_POSITION = new Vector2(Screen.width / 2, 0.85f * Screen.height);
    public static readonly Vector3 PIECES_POSITION_LIMITS = Helpers.GetWorldPosition(new Vector3(Screen.width * 0.90f, 0, 0));
}

public static class CameraConsts
{
    public const float LOWEST_Y_POSITION = 18;

}

public static class Layers
{
    public const int PIECES_LAYER = 6;
    public const int TOWER_BASE_LAYER = 7;
    public const int LANDED_PIECES_LAYER = 8;
    public const int FLOOR_LAYER = 9;
}

public static class UITexts
{
    public const string HIGH_SCORE_TEXT = "Highest Score: ";
    public const string LIVES_LEFT_TEXT = "Lives left: ";
}

public static class SceneNames
{
    public const string MAIN_MENU = "MainMenu";
    public const string SINGLE_PLAYER_SCENE = "SinglePlayer";
    public const string AI_MODE_SCENE = "AIMode";
}