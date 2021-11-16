using Main;
using Gameplay;
using Enum;


namespace GameModes
{
    public class BossRunMode : GameMode
    {
        private int _stage = 0;
        private readonly int _stageAmount = 3;
        public BossRunMode() : base(){}
        public override void CheckGameEnding(Player player)
        {
            base.CheckGameEnding(player);
            if (_stage == _stageAmount && _enemies.Count == 0) GameEnded = true;
        }
        public override void AddEnemies(int score)
        {
            if (_enemies.Count == 0)
            {
                switch(_stage)
                {
                    case 0:
                        _enemies.Add(EnemyFactory.Create(EnemyType.NightmareBoss, new int[]{}));   
                        _stage++;
                        break;
                    case 1:
                        _enemies.Add(EnemyFactory.Create(EnemyType.PhantomBoss, new int[]{}));
                        _stage++;
                        break;
                    case 2:
                        _enemies.Add(EnemyFactory.Create(EnemyType.PhantomBoss, new int[]{}));
                        _enemies.Add(EnemyFactory.Create(EnemyType.NightmareBoss, new int[]{}));
                        _stage++;
                        break;
                }
            }
        }  
    }
}