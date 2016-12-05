using UnityEngine;

public class GameValues
{
    public static int HighScore;

    public static readonly Vector2 EnemyAttackZonePosition_Level_1 = new Vector2(1.8f, 0);
    public static readonly Vector2 EnemyAttackZonePosition_Level_2 = new Vector2(2.4f, 0);
    public static readonly Vector2 EnemyAttackZonePosition_Level_3 = new Vector2(3.0f, 0);
    public static readonly Vector2 EnemyAttackZonePosition_Level_4 = new Vector2(4.2f, 0);

    public static readonly float RunAnimationSpeed = 0.42f;

    public static readonly float RespawnTime_Level_1 = 1.5f;
    public static readonly float RespawnTime_Level_2 = 1.25f;
    public static readonly float RespawnTime_Level_3 = 1.0f;
    public static readonly float RespawnTime_Level_4 = 0.75f;

    public static readonly float EnemyMoveSpeed_Level_1 = 0.05f;
    public static readonly float EnemyMoveSpeed_Level_2 = 0.075f;
    public static readonly float EnemyMoveSpeed_Level_3 = 0.1f;
    public static readonly float EnemyMoveSpeed_Level_4 = 0.15f;

    public static readonly float DifficultyLevelIncreaseSpawnDelay = 5f;
}
