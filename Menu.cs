using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Space_Shooter
{
    public static class Menu
    {
        public enum GameScene
        {
            MainMenu,
            PauseMenu,
            GamePlay,
            GameLevel,
            GameOver,
            LevelCompleted,
        }
        public static GameScene Scene = GameScene.MainMenu;
        public static void ChangeScene(GameScene newScene) => Scene = newScene;
        public static void DrawEndOfLevel(int level)
        {
            SplashKit.DrawText("CONGRATULATIONS", Color.Yellow, Global.BigFont, 60, 150, 50);
            SplashKit.DrawText("You have completed level " + level , Color.Green, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 300);
        }
        public static void DrawMainMenu()
        {
            SplashKit.DrawText("SPACE SHOOTER", Color.Yellow, Global.BigFont, 60, 100, 50);
            SplashKit.DrawText("Play", Color.White, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Choose level" , Color.Orange, Global.MediumFont, 40, 150, 300);
            SplashKit.DrawText("Change Space Ship", Color.Blue, Global.MediumFont, 40, 150, 400);
        }
        public static void DrawPauseMenu()
        {
            SplashKit.DrawText("Resume", Color.Green, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 300);
        }
        public static void DrawGameOver(string message)
        {
            SplashKit.DrawText(message.ToUpper(), Color.Yellow, Global.BigFont, 60, 150, 50);      
            SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 200);
        }
        public static void DrawPauseButton(){ SplashKit.DrawText("Pause", Color.Red, Global.SmallFont, 24, 500, 40);}
        public static void DrawGameInfo(int playerHealth, double score){
            SplashKit.DrawText($"Health: {(int)playerHealth}", Color.Green, Global.SmallFont, 24, 20, 40);
            SplashKit.DrawText($"Score: {(int)score}", Color.Yellow, Global.SmallFont, 24, 20, 70);
        }
        public static void DrawPlayerOption(int option)
        {
            Bitmap bitmap;
            switch(option)
            {
                case 1: 
                    bitmap = SplashKit.LoadBitmap("option2", "options/option2.png");
                    break;
                case 2:
                    bitmap = SplashKit.LoadBitmap("option3", "options/option3.png");
                    break;
                default:
                    bitmap = SplashKit.LoadBitmap("option1", "options/option1.png");
                    break;
            }
            bitmap.Draw(150, 500);
        }
        public static void DrawLevels()
        {
            for (int i = 1; i <= 4; i++)
            {
                Color color;
                if (Data.LevelIsComplete(i)) color = Color.Green;
                else color = Color.White;
                SplashKit.DrawText("Level " + i, color, Global.MediumFont, 40, 150, 100 * i);
            }
            SplashKit.DrawText("Endless Mine Field", Color.Orange, Global.MediumFont, 40, 150, 500);
            SplashKit.DrawText("Boss Run", Color.Orange, Global.MediumFont, 40, 150, 600);
            SplashKit.DrawText("Survival", Color.Red, Global.MediumFont, 40, 150, 700);
        }

        //TODO: write the logic for top right buttons
    }
}
