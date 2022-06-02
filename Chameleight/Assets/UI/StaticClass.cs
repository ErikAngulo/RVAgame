using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass 
{
    // This Static Class purpose is to save information across game scenes
    // so that we can acquire it again when loading other scene

    // Scene name of the selected game
    public static string SelectedGameScene { get; set; }
    // Configuration values for games
    public static float Time { get; set; }
    public static int BallLimit { get; set; }
    public static float BallFactor { get; set; }
    public static bool TargetMovement { get; set; }
    public static string Controller { get; set; }
    
    // Score text to display when game finishes
    public static string scoreText { get; set; }
    // Current game session player ID
    public static string playerId { get; set; } = "0";
}
