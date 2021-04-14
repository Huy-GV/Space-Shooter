using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class Data
    {
        public enum Level
        {
            Endless,
            Mission,
            Dual,
            BossRun
        }
        // public static Dictionary<GameLevel.Mode, int> HighScoreRecord = new Dictionary<GameLevel.Mode, int>();
        public static Dictionary<int, bool> LevelProgress = new Dictionary<int, bool>()
        {
            {1, false},
            {2, false},
            {3, false},
            {4, false},
            {5, false},
            {6, false},
        };
        public static void LoadLevelProgress()
        { //TODO: LOAD FROM FILE OR DB HERE } 
        }
        public static bool LevelIsComplete(int level) => LevelProgress[level];
    }
}