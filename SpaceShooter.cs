using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class SpaceShooter
    {
        private Player _player;
        private List<Enemy> _enemies;
        private Dictionary<Type, int> _enemyAmountByClass = new Dictionary<Type, int>();
        private Window gameWindow = new Window("Space Shooter", Global.Width, Global.Height);
        public SpaceShooter()
        {
            GameSession.ResetScore();
            RegisterEnemies();
            _player = new Player(GameSession.SpaceshipChoice);
        }
        private void RegisterEnemies()
        {
            _enemyAmountByClass[typeof(Asteroid)] = 0;
            _enemyAmountByClass[typeof(Spacemine)] = 0;
            _enemyAmountByClass[typeof(BlueAlienship)] = 0;
            _enemyAmountByClass[typeof(PurpleAlienship)] = 0;
            _enemyAmountByClass[typeof(RedAlienship)] = 0;
            _enemyAmountByClass[typeof(KamikazeAlien)] = 0;
        }
        private void UpdateEnemyAmount(Type type, int increment) => _enemyAmountByClass[type] += increment;
        private void ResetEnemyAmount()
        {
            _enemyAmountByClass.Clear();
            RegisterEnemies();
        }
        public void Draw()
        {                
            GameSession.DrawBackground();
            switch(Menu.Scene)
            {
                case Menu.GameScene.MainMenu:
                    Menu.DrawMainMenu(Difficulty.Index);
                    Menu.DrawPlayerOption(GameSession.SpaceshipChoice);
                    break;
                case Menu.GameScene.PauseMenu:
                    Menu.DrawPauseMenu();
                    break;
                case Menu.GameScene.GameOver:
                    Menu.DrawGameOver();
                    break;
                default:
                    Menu.DrawPauseButton();
                    Menu.DrawGameInfo((int)_player.Health, GameSession.Score);
                    GameSession.GainScore();
                    GameSession.DrawExplosions();
                    DrawEnemies();
                    _player.Draw();
                    break;
            }
            SplashKit.RefreshScreen(60);
        }
        public void HandleInputs()
        {
            switch(Menu.Scene)
            {
                case Menu.GameScene.MainMenu:
                    if (Menu.FirstOptionSelected())
                    {
                        _enemies = new List<Enemy>();
                        _player = new Player(GameSession.SpaceshipChoice);
                        Menu.ChangeScene(Menu.GameScene.GamePlay);
                    }
                    else if (Menu.SecondOptionSelected())
                    {
                        Difficulty.ChangeLevel();
                    }
                    else if (Menu.ThirdOptionSelected())
                    {
                        GameSession.UpdatePlayerChoice();
                    }
                    break;
                case Menu.GameScene.PauseMenu:
                    if (Menu.FirstOptionSelected()) 
                        Menu.ChangeScene(Menu.GameScene.GamePlay);
                    else if (Menu.SecondOptionSelected())
                    {
                        ResetEnemyAmount();
                        GameSession.ResetScore();
                        Menu.ChangeScene(Menu.GameScene.MainMenu);
                    }
                    break;
                case Menu.GameScene.GameOver:
                    if (Menu.FirstOptionSelected())
                    {
                        ResetEnemyAmount();
                        GameSession.ResetScore();
                        Menu.ChangeScene(Menu.GameScene.MainMenu);
                    }
                    break;
                default:
                    HandleKeyboardInputs();
                    if (Menu.PauseOptionSelected()) Menu.ChangeScene(Menu.GameScene.PauseMenu); 
                    break;
            }
        }
        public void HandleKeyboardInputs()
        {
            if (SplashKit.KeyDown(KeyCode.LeftKey) && _player.X > 0)   _player.MoveLeft();
            if (SplashKit.KeyDown(KeyCode.RightKey) && _player.X < Global.Width)  _player.MoveRight();
            if (SplashKit.KeyDown(KeyCode.UpKey) && _player.Y > Global.Height / 2)   _player.MoveUp();
            if (SplashKit.KeyDown(KeyCode.DownKey) && _player.Y < Global.Height)  _player.MoveDown();
            if (SplashKit.KeyDown(KeyCode.SpaceKey) && _player.CoolDown == 0) _player.Shoot();
        }
        public void Update()
        {
            SplashKit.ProcessEvents();
            GameSession.PlayMusic();
            if (Menu.Scene == Menu.GameScene.GamePlay)
            {
                _player.Update();
                if (_player.Health <= 0) Menu.ChangeScene(Menu.GameScene.GameOver);
                GameSession.UpdateExplosions();
                UpdateEnemies();
                AddEnemies();
            }
        }
        private void AddEnemies()
        {
            int enemyTypeAmount;
            foreach (var enemyType in _enemyAmountByClass.Keys)
            {
                enemyTypeAmount = _enemyAmountByClass[enemyType];
                //generate enemies, their amount and spawning location is dependent on the current amount and closest enemy created
                if (SplashKit.Rnd(0, 70) == 0 && enemyTypeAmount < Difficulty.Limit[enemyType])
                {
                    if (enemyTypeAmount == 0) _enemies.Add((Enemy)Activator.CreateInstance(enemyType));
                    else
                    {
                        var lastEnemy = _enemies[_enemies.Count - 1];
                        Object[] parameters = {lastEnemy.X, lastEnemy.Y};
                        _enemies.Add((Enemy)Activator.CreateInstance(enemyType, parameters));
                    }
                    UpdateEnemyAmount(enemyType, 1);
                }
            }
        }
        private void UpdateEnemies()
        {
            foreach(Enemy enemy in _enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_player.Bullets);
                if (enemy.AdjustedY > Global.Height || enemy.IsDestroyed)
                {
                    if (enemy.IsDestroyed) 
                    {
                        GameSession.CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType);
                    }
                    UpdateEnemyAmount(enemy.GetType(), -1);
                    _enemies.Remove(enemy);                     
                }
                if (_player.CollideWith(enemy))
                { 
                    enemy.GetDestroyed();
                    _player.LoseHealth(enemy.Damage);
                }
                if (enemy is IHaveGun) _player.CheckEnemyBullets(((IHaveGun)enemy).Bullets);   
            }
        }
        private void DrawEnemies() {foreach(Enemy enemy in _enemies.ToArray()) enemy.Draw();}
    }
}