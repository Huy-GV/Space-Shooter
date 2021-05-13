﻿using System;
using SplashKitSDK;
using System.Collections.Generic;


namespace SpaceShooter
{
    public class Program
    {
        static void Main(string[] args)
        {
            var SpaceShooter = new Game();
            while (!SplashKit.QuitRequested())
            {
                SpaceShooter.Update();
                SpaceShooter.ProcessInputs();
                SpaceShooter.Draw();
            }  
        }
    }
}