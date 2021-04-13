using System;
using SplashKitSDK;
using System.Collections.Generic;


namespace Space_Shooter
{
class Program
{
    static int spaceshipChoice = 0;
    static Player player;
    static SoloGame game;
    static int level;
    static Dictionary<Type, int> enemyAmountByClass = new Dictionary<Type, int>();
    static Window gameWindow = new Window("Space Shooter", Global.Width, Global.Height);
    static void Main(string[] args)
    {
        // GameBackground.ResetScore();
        RegisterEnemies();
        level = 7;
        // player = new Player(spaceshipChoice);
        while (!SplashKit.QuitRequested())
        {
            Update();
            Draw();
            HandleInputs();
        }
    }
    static void RegisterEnemies()
    {
        enemyAmountByClass[typeof(Asteroid)] = 0;
        enemyAmountByClass[typeof(Spacemine)] = 0;
        enemyAmountByClass[typeof(BlueAlienship)] = 0;
        enemyAmountByClass[typeof(PurpleAlienship)] = 0;
        enemyAmountByClass[typeof(RedAlienship)] = 0;
        enemyAmountByClass[typeof(KamikazeAlien)] = 0;
    }
    // static void UpdateEnemyAmount(Type type, int increment) => enemyAmountByClass[type] +=increment;
    // static void ResetGame()
    // {
    //     enemyAmountByClass.Clear();
    //     RegisterEnemies();
    //     // GameBackground.ResetScore();
    // }
    static void Draw()
    {                
        GameBackground.DrawBackground();
        switch(Menu.Scene)
        {
            case Menu.GameScene.MainMenu:
                Menu.DrawMainMenu(Difficulty.Index);
                Menu.DrawPlayerOption(spaceshipChoice);
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
            case Menu.GameScene.GamePlay:
                game.Draw();
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
                    level = option;
                    Menu.ChangeScene(Menu.GameScene.MainMenu);
                    break;
            }
        }
    }
    static void ProcessMainMenu(int option)
    {
        switch(option)
        {
            case 1:
               
                game = new SoloGame( level, spaceshipChoice, enemyAmountByClass);
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
                // ResetGame();
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
                // ResetGame();
                Menu.ChangeScene(Menu.GameScene.MainMenu);
                break;
        }
    }
    static void Update()
    {
        SplashKit.ProcessEvents();
        GameBackground.PlayMusic();
        if (Menu.Scene == Menu.GameScene.GamePlay)
        {
            if (game.Status == SoloGame.GameStates.PlayerAlive) game.Update();
            else Menu.ChangeScene(Menu.GameScene.GameOver); //TODO: FIX THIS TO DISPLAY APPROPRIATE MESSAGE
        }
    }
    static void UpdatePlayerChoice(){ spaceshipChoice = spaceshipChoice == 2 ? 0 :spaceshipChoice + 1; }

}
}