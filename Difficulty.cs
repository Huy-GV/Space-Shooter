using System;
using System.Collections.Generic;
namespace Space_Shooter
{   
    public static class Difficulty
    {
        public enum Level
        {
            Easy,
            Medium,
            Hard
        }
        public static Dictionary<Type, int> Limit = new Dictionary<Type, int>()
        {
            {typeof(BlueAlienship), 3},
            {typeof(PurpleAlienship), 2},
            {typeof(RedAlienship), 1},
            {typeof(KamikazeAlien), 0},
            {typeof(Asteroid), 7},
            {typeof(Spacemine), 1}
        };
        public static int Index = 0;
        public static void ChangeLevel()
        {
            Index = Index == 2 ? 0 : Index + 1;
            UpdateEnemyLimit();
        }
        public static void UpdateEnemyLimit()
        {
            switch((Level)Index)
            {
                case Level.Medium:
                    Limit[typeof(BlueAlienship)] = 4;
                    Limit[typeof(PurpleAlienship)] = 3;
                    Limit[typeof(RedAlienship)] = 2;
                    Limit[typeof(KamikazeAlien)] = 1;
                    Limit[typeof(Asteroid)] = 6;
                    Limit[typeof(Spacemine)] = 2;
                    break;
                case Level.Hard: 
                    Limit[typeof(BlueAlienship)] = 5;
                    Limit[typeof(PurpleAlienship)] = 3;
                    Limit[typeof(RedAlienship)] = 2;
                    Limit[typeof(KamikazeAlien)] = 3;
                    Limit[typeof(Asteroid)] = 7;
                    Limit[typeof(Spacemine)] = 3;
                    break;
                default:
                    Limit[typeof(BlueAlienship)] = 3;
                    Limit[typeof(PurpleAlienship)] = 2;
                    Limit[typeof(RedAlienship)] = 1;
                    Limit[typeof(KamikazeAlien)] = 0;
                    Limit[typeof(Asteroid)] = 5;
                    Limit[typeof(Spacemine)] = 1;
                    break;
            }
        }
    }
}