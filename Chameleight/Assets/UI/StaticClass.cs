using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass 
{
    public static string SelectedGameScene { get; set; }

    public static float Time { get; set; }
    public static int BallLimit { get; set; }
    public static bool TargetMovement { get; set; }
    public static string Controller { get; set; }
    public static string scoreText { get; set; }
    public static string playerId { get; set; } = "0";
}
