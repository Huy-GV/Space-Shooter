using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;


//TODO: FIX THE BUG WHERE THE WRONG SHIP IS CHOSEN. RE-NAME THE IMAGE LINKS ACCORDING TO THE NEW STRUCTURE

namespace Space_Shooter
{
    public static class Menu
    {
        public enum GameScene
        {
            MainMenu,
            PauseMenu,
            GamePlay,
            GameOver
        }
        public static GameScene Scene = GameScene.MainMenu;
        public static void ChangeScene(GameScene newScene) => Scene = newScene;
        public static void DrawMainMenu(int difficulty)
        {
            string text;
            Color color;
            switch(difficulty)
            {
                case 1:
                    text = "MEDIUM";
                    color = Color.Orange;
                    break;
                case 2:
                    text = "HARD";
                    color = Color.Red;
                    break;
                default:
                    text = "EASY";
                    color = Color.Green;
                    break;    
            }
            SplashKit.DrawText("SPACE SHOOTER", Color.Yellow, Global.BigFont, 60, 100, 50);
            SplashKit.DrawText("Play", Color.White, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Difficulty: " + text, color, Global.MediumFont, 40, 150, 300);
            SplashKit.DrawText("Change Space Ship", Color.Blue, Global.MediumFont, 40, 150, 400);
        }
        public static void DrawPauseMenu()
        {
            SplashKit.DrawText("Resume", Color.Green, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 300);
        }
        public static void DrawGameOver()
        {
            SplashKit.DrawText("GAME OVER", Color.Yellow, Global.BigFont, 60, 150, 50);
            //TODO: DRAW PLAYER SCORE HERE AND CHANGE THE MOUSE CLICK POSITION        
            SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 200);
        }
        public static void DrawPauseButton(){ SplashKit.DrawText("Pause", Color.Red, Global.SmallFont, 24, 500, 40);}
        public static void DrawGameInfo(int playerHealth, double score){
            SplashKit.DrawText($"Health: {playerHealth}", Color.Green, Global.SmallFont, 24, 20, 40);
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
        public static bool FirstOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) &&
                SplashKit.MouseY() >= 200 && SplashKit.MouseY() <= 250; }
        public static bool SecondOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) && 
                SplashKit.MouseY() >= 300 && SplashKit.MouseY() <= 350; }
        public static bool ThirdOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) && 
                SplashKit.MouseY() >= 400 && SplashKit.MouseY() <= 450;}
        public static bool PauseOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.MouseX() >= 200 && SplashKit.MouseY() <= 55;}
    }
}
