using UnityEngine;
using System.Collections;

public class GamePreferences : MonoBehaviour {

    public static string FirstGuide = "IsFirstGuide";
    public static string FirstSlide = "IsFirstSlide";
    public static string FirstPlay = "IsfirstPlay";
    public static string MusicOnOff = "MusicOnOff";
    public static string SoundOnOff = "SoundOnOff";
    public static string Coins = "Coins";
    public static string Credit = "Credit";
    public static string LevelsStars = "LevelsStars";

    //Variable to Show When Game Data Initialization Has Been Completed from "GameDataManager" Script
    public static bool InitializationCompleted = false;
    public static bool InitializationCompleted2 = false;


    public static bool IsProcessing = false;


    public static int[] SeasonsStarLimit = { 0, 0 ,0,0 };
    public static int EachSeasonLevel;
}
