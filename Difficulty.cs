using System;
using System.Collections.Generic;
namespace Space_Shooter
{   
    public static class Difficulty
    {
        public enum Stage
        {
            Easy,
            Medium,
            Hard
        }
        public static Dictionary<Type, int> Limit = new Dictionary<Type, int>()
        {
            {typeof(BlueAlienship), 0},
            {typeof(PurpleAlienship), 0},
            {typeof(RedAlienship), 0},
            {typeof(KamikazeAlien), 0},
            {typeof(Asteroid), 0},
            {typeof(Spacemine), 0}
        };
        public static int Index = 0;
        public static void SetEnemyLimitByLevel(int level)
        {
            switch(level)
            {
                case 1:
                    Limit[typeof(Asteroid)] = 5;
                    Limit[typeof(Spacemine)] = 3;  
                    break;
                case 2:
                    Limit[typeof(BlueAlienship)] = 6;
                    Limit[typeof(Asteroid)] = 5;
                    Limit[typeof(Spacemine)] = 2;
                    break;
                case 3:
                    Limit[typeof(BlueAlienship)] = 0;
                    Limit[typeof(PurpleAlienship)] = 6;
                    Limit[typeof(Asteroid)] = 5;
                    Limit[typeof(Spacemine)] = 2;
                    break;
                case 4:
                    Limit[typeof(BlueAlienship)] = 3;
                    Limit[typeof(PurpleAlienship)] = 0;
                    Limit[typeof(RedAlienship)] = 5;
                    Limit[typeof(KamikazeAlien)] = 0;
                    Limit[typeof(Asteroid)] = 4;
                    Limit[typeof(Spacemine)] = 2;
                    //TODO: ADD A BOSS TYPE
                    break;
                case 5:
                    Limit[typeof(BlueAlienship)] = 3;
                    Limit[typeof(PurpleAlienship)] = 2;
                    Limit[typeof(RedAlienship)] = 3;
                    Limit[typeof(KamikazeAlien)] = 3;
                    Limit[typeof(Asteroid)] = 5;
                    Limit[typeof(Spacemine)] = 2; 
                    //TODO: ADD A BOSS TYPE                   
                    break;
                case 6:
                    Limit[typeof(BlueAlienship)] = 4;
                    Limit[typeof(PurpleAlienship)] = 4;
                    Limit[typeof(RedAlienship)] = 4;
                    Limit[typeof(KamikazeAlien)] = 3;
                    Limit[typeof(Asteroid)] = 6;
                    Limit[typeof(Spacemine)] = 3; 
                    //TODO: ADD A BOSS TYPE 
                    break;
                default :
                    SetEnemyLimitByStage(0); 
                    break;
            }
        }
        public static void IncreaseStage() => Index += 1;
        public static void SetEnemyLimitByStage(int index)
        {
            switch((Stage)index)
            {
                case Stage.Medium:
                    Limit[typeof(BlueAlienship)] = 4;
                    Limit[typeof(PurpleAlienship)] = 3;
                    Limit[typeof(RedAlienship)] = 2;
                    Limit[typeof(KamikazeAlien)] = 1;
                    Limit[typeof(Asteroid)] = 6;
                    Limit[typeof(Spacemine)] = 2;
                    break;
                case Stage.Hard: 
                    Limit[typeof(BlueAlienship)] = 5;
                    Limit[typeof(PurpleAlienship)] = 3;
                    Limit[typeof(RedAlienship)] = 2;
                    Limit[typeof(KamikazeAlien)] = 3;
                    Limit[typeof(Asteroid)] = 7;
                    Limit[typeof(Spacemine)] = 3;
                    break;
                case Stage.Easy:
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