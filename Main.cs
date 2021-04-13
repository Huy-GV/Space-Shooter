using System;
using SplashKitSDK;
using System.Collections.Generic;


namespace Space_Shooter
{
class Program
{
    static int _spaceshipChoice = 0;
    static Player _player;
    static List<Enemy> _enemies;
    static Dictionary<Type, int> _enemyAmountByClass = new Dictionary<Type, int>();
    static Window gameWindow = new Window("Space Shooter", Global.Width, Global.Height);
    static void Main(string[] args)
    {
        GameBackground.ResetScore();
        RegisterEnemies();
        _player = new Player(_spaceshipChoice);
        while (!SplashKit.QuitRequested())
        {
            Update();
            Draw();
            HandleInputs();
        }
    }
    //add enemy types and their amount so that we can spawn them easily in AddEnemies
    static void RegisterEnemies()
    {
        _enemyAmountByClass[typeof(Asteroid)] = 0;
        _enemyAmountByClass[typeof(Spacemine)] = 0;
        _enemyAmountByClass[typeof(BlueAlienship)] = 0;
        _enemyAmountByClass[typeof(PurpleAlienship)] = 0;
        _enemyAmountByClass[typeof(RedAlienship)] = 0;
        _enemyAmountByClass[typeof(KamikazeAlien)] = 0;
    }
    static void UpdateEnemyAmount(Type type, int increment) => _enemyAmountByClass[type] +=increment;
    static void ResetGame()
    {
        _enemyAmountByClass.Clear();
        RegisterEnemies();
        GameBackground.ResetScore();
    }
    static void Draw()
    {                
        GameBackground.DrawBackground();
        switch(Menu.Scene)
        {
            case Menu.GameScene.MainMenu:
                Menu.DrawMainMenu(Difficulty.Index);
                Menu.DrawPlayerOption(_spaceshipChoice);
                break;
            case Menu.GameScene.PauseMenu:
                Menu.DrawPauseMenu();
                break;
            case Menu.GameScene.GameOver:
                Menu.DrawGameOver();
                break;
            case Menu.GameScene.GameLevel:
                Menu.DrawLevels();
                break;
            default:
                Menu.DrawPauseButton();
                Menu.DrawGameInfo((int)_player.Health, GameBackground.Score);
                GameBackground.GainScore();
                GameBackground.DrawExplosions();
                DrawEnemies();
                _player.Draw();
                break;
        }
        SplashKit.RefreshScreen(60);
    }
    static void HandleInputs()
    {
        //the game has different 'scenes' that can be accessed by buttons displayed on screen
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            var option = (int)Math.Floor((SplashKit.MouseY() / 100)) - 1;
            Console.WriteLine("option is {0}", option);
            switch(Menu.Scene)
            {
                case Menu.GameScene.MainMenu:
                    ProcessMainMenu(option);
                    break;
                case Menu.GameScene.PauseMenu:
                    ProcessPauseMenu(option);
                    break;
                case Menu.GameScene.GameOver:
                    ProcessGameOver(option);
                    break;
                case Menu.GameScene.GameLevel:
                    ProcessGameLevel(option);
                    break;
                default:
                    ProcessGamePlay();
                    break;
            }
            
        }
    }
    static void ProcessMainMenu(int option)
    {
        switch(option)
        {
            case 1:
                _enemies = new List<Enemy>();
                _player = new Player(_spaceshipChoice);
                Menu.ChangeScene(Menu.GameScene.GamePlay);
                break;
            case 2:
                Menu.ChangeScene(Menu.GameScene.GameLevel);
                break;
            case 3:
                UpdatePlayerChoice();
                break;
        }
    }
    static void ProcessPauseMenu(int option)
    {
        switch(option)
        {
            case 1:
                Menu.ChangeScene(Menu.GameScene.GamePlay);
                break;
            case 2:
                ResetGame();
                Menu.ChangeScene(Menu.GameScene.MainMenu);
                break;
        }
    }
    static void ProcessGameLevel(int option)
    {
        
    }
    static void ProcessGameOver(int option)
    {
        switch(option)
        {
            case 1:
                ResetGame();
                Menu.ChangeScene(Menu.GameScene.MainMenu);
                break;
        }

    }
    static void ProcessGamePlay()
    {
        HandleKeyboardInputs();
        if (Menu.PauseOptionSelected()) Menu.ChangeScene(Menu.GameScene.PauseMenu); 
    }
    static void HandleKeyboardInputs()
    {
        if (SplashKit.KeyDown(KeyCode.LeftKey) && _player.X > 0)   _player.MoveLeft();
        if (SplashKit.KeyDown(KeyCode.RightKey) && _player.X < Global.Width)  _player.MoveRight();
        if (SplashKit.KeyDown(KeyCode.UpKey) && _player.Y > Global.Height / 2)   _player.MoveUp();
        if (SplashKit.KeyDown(KeyCode.DownKey) && _player.Y < Global.Height)  _player.MoveDown();
        if (SplashKit.KeyDown(KeyCode.SpaceKey) && _player.CoolDown == 0) _player.Shoot();
    }
    static void Update()
    {
        SplashKit.ProcessEvents();
        GameBackground.PlayMusic();
        if (Menu.Scene == Menu.GameScene.GamePlay)
        {
            _player.Update();
            if (_player.Health <= 0) Menu.ChangeScene(Menu.GameScene.GameOver);
            GameBackground.UpdateExplosions();
            UpdateEnemies();
            UpdateDifficulty(GameBackground.Score);
            AddEnemies();
        }
    }
    static void UpdateDifficulty(double score)
    {
        if (score >= 100 && Difficulty.Index == 0) Difficulty.ChangeStage();
        if (score >= 200 && Difficulty.Index == 1) Difficulty.ChangeStage();
        if (score >= 400 && Difficulty.Index == 2) Difficulty.ChangeStage();
    }
    static void UpdatePlayerChoice(){ _spaceshipChoice = _spaceshipChoice == 2 ? 0 :_spaceshipChoice + 1; }
    static void AddEnemies()
    {
        int enemyTypeAmount;
        foreach (var enemyType in _enemyAmountByClass.Keys)
        {
            enemyTypeAmount = _enemyAmountByClass[enemyType];
            //generate enemies, their amount and spawning location is dependent on the currentamount and closest enemy created
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
    static void UpdateEnemies()
    {
        foreach(Enemy enemy in _enemies.ToArray())
        {
            enemy.Update();
            enemy.CheckPlayerBullets(_player.Bullets);
            if (enemy.AdjustedY > Global.Height || enemy.IsDestroyed)
            {
                if (enemy.IsDestroyed) 
                {
                    GameBackground.CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType);
                }
                UpdateEnemyAmount(enemy.GetType(), -1);
                //because enemy spawning frequency depends on their current amount, we need toupdate it everytime an enemy is destroyed or spawned
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
    static void DrawEnemies() {foreach(Enemy enemy in _enemies.ToArray()) enemy.Draw();}
}
}