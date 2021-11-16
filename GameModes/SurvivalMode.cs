using Enum;
using System;
using SplashKitSDK;

namespace GameModes
{
    public class SurvivalMode : GameMode
    {
        private int _stage;
        public SurvivalMode() : base()
        {
            _stage = 0;
            SetEnemyLimitsByStage();
        }
        private void SetEnemyLimitsByStage()
        {
            switch(_stage)
            {
                case 0:
                    SpawnRate = SplashKit.Rnd(0,80);                
                    _limits[EnemyType.BlueAlienship] = 3;
                    _limits[EnemyType.PurpleAlienship] = 2;
                    _limits[EnemyType.RedAlienship] = 1;
                    _limits[EnemyType.KamikazeAlien] = 0;
                    _limits[EnemyType.Asteroid] = 5;
                    _limits[EnemyType.Spacemine] = 1;
                    break;
                case 1:
                    SpawnRate = SplashKit.Rnd(0,70);
                    _limits[EnemyType.BlueAlienship] = 3;
                    _limits[EnemyType.PurpleAlienship] = 3;
                    _limits[EnemyType.RedAlienship] = 2;
                    _limits[EnemyType.KamikazeAlien] = 2;
                    _limits[EnemyType.Asteroid] = 6;
                    _limits[EnemyType.Spacemine] = 2;
                    break;
                case 2: 
                    SpawnRate = SplashKit.Rnd(0,65);
                    _limits[EnemyType.BlueAlienship] = 3;
                    _limits[EnemyType.PurpleAlienship] = 3;
                    _limits[EnemyType.RedAlienship] = 2;
                    _limits[EnemyType.KamikazeAlien] = 2;
                    _limits[EnemyType.Asteroid] = 5;
                    _limits[EnemyType.Spacemine] = 3;
                    break;
                default: throw new IndexOutOfRangeException($"The stage index exceeds 2");
            }
        }      
        public override void AddEnemies(int score)
        {
            if ((score > 10 && _stage == 0) || (score > 20 && _stage == 1))
            {
                _stage++;
                SetEnemyLimitsByStage();
            } 
            // Console.WriteLine(_stage);
            base.AddEnemies(score);
        } 
    }
}