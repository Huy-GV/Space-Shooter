using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    ///<summary>
    ///Spawns enemies given a concrete type and the last enemy's position
    ///</summary>
    public class EnemyFactory
    {
        public static Enemy Create(EnemyType enemyType, int[] lastEnemy)
        {

            switch(enemyType)
            {
                case EnemyType.BlueAlienship: 
                    return CreateBlueAlienship(lastEnemy[0], lastEnemy[1]);
                case EnemyType.RedAlienship:
                    return CreateRedAlienship(lastEnemy[1]);
                case EnemyType.PurpleAlienship:
                    return CreatePurpleAlienship(lastEnemy[1]);
                case EnemyType.KamikazeAlien:
                    return CreateKamikaze();
                case EnemyType.Asteroid:
                    return CreateAsteroid(lastEnemy[1]);
                case EnemyType.Spacemine:
                    return CreateSpacemine(lastEnemy);
                default: throw new NotImplementedException($"The type {enemyType} is not implemented");
            }
        }
        private static Enemy CreateSpacemine(int[] lastEnemy)
        {
            int x, y;
            if (lastEnemy[0] >= 120)
                x = (2 * SplashKit.Rnd(0,3) + 1) * 100; 
            else 
                x = (2 * SplashKit.Rnd(0,3) + 1) * 100;
        
            if (lastEnemy[1] >= 120)
                y = -140;
            else
                y = lastEnemy[1] - 240;
            var position = new Position(x, y);
            var movePattern = new StraightLinePattern(3);
            return new Spacemine(position, movePattern, 18);
        }
        private static KamikazeAlien CreateKamikaze()
        {
            var position = new Position(Global.Width / 2, -50);
            var angle = (SplashKit.Rnd(0, 42) + 69);
            var movePattern = new StraightLinePattern(9, angle);
            return new KamikazeAlien(position, movePattern, angle, 18);
        } 
        private static Nightmare CreateNightmare()
        {
            var position = new Position(Global.Width / 2, -50);
            var gun = new Gun(1.2, Bullet.Type.RedLaser, false);
            var movePattern = new StraightLinePattern(3);
            var bitmap = SplashKit.LoadBitmap("Nightmare", "Bosses/Nightmare.png");
            var image = new StaticImage(bitmap);
            return new Nightmare(position, movePattern, gun, image);
        }
        private static Phantom CreatePhantom()
        {
            var bitmap = SplashKit.LoadBitmap("Phantom", "Bosses/Phantom.png");
            var cellDetails = new int[]{300/2, 130, 2, 1, 2};
            var image = new AnimatedImage("flickerScript", "flickering", bitmap, cellDetails);
            var position = new Position(Global.Width / 2, -50);
            var gun = new Gun(1, Bullet.Type.TripleLaser, false);
            var movePattern = new ZigzagPattern(2, 2, -20, true);
            return new Phantom(position, movePattern, gun, image);
        }
        private static Alienship CreateBlueAlienship(int lastEnemyX, int lastEnemyY)
        {
            int x, y;
            int randomX = SplashKit.Rnd(0, 6);
            if (lastEnemyY >= 100)
            {
                x = (2 * randomX + 1) * 50; 
                y = -50;
            } else
            {
                while ( (2 * randomX + 1) * 50  == lastEnemyX) randomX = SplashKit.Rnd(0, 6);
                x = (2 * randomX + 1) * 50; 
                y = lastEnemyY - 100;
            } 
            var gun = new Gun(3);
            var movePattern = new StraightLinePattern(3);
            var position = new Position(x, y);
            var bitmap = SplashKit.LoadBitmap("BlueAlienship", "Alienships/BlueAlienship.png");
            var image = new StaticImage(bitmap);
            return new Alienship(position, gun, movePattern, image, EnemyType.BlueAlienship);
        }
        private static Enemy CreateRedAlienship(int lastEnemyY)
        {
            var y = (lastEnemyY >= 100) ? -50 : lastEnemyY - 110;
            var x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
            var position = new Position(x, y);
            var gun = new Gun(2);
            var movePattern = new ZigzagPattern(2, 3, y);
            var bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
            var image = new StaticImage(bitmap);
            return new Alienship(position, gun, movePattern, image, EnemyType.RedAlienship);
        }
        private static Alienship CreatePurpleAlienship(int lastEnemyY)
        {
            int x, y;
            if (lastEnemyY >= 100) y = -60;
            else y = lastEnemyY - 110;
            x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
            var gun = new Gun(3);
            var movePattern = new StraightLinePattern(2);
            var position = new Position(x, y);
            var bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
            var image = new StaticImage(bitmap);
            return new Alienship(position, gun, movePattern, image, EnemyType.PurpleAlienship);
        }
        private static Enemy CreateAsteroid(int parameter)
        {
            var x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
            var y = (parameter > 50) ? -10 : parameter - 60;
            var position = new Position(x, y);
            var movePattern = new StraightLinePattern(4);
            return (Enemy)new Asteroid(position, movePattern);
        }
    }
}