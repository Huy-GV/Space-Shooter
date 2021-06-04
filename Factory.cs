using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public class EnemyFactory
    {
        public static Enemy SpawnEnemy(Type enemyType, int[] parameters)
        {
            if (enemyType == typeof(Asteroid))
                return CreateAsteroid(parameters[1]);
            else if (enemyType.IsSubclassOf(typeof(Alienship)))
                return CreateGunship(enemyType, parameters);
            else if (enemyType == typeof(KamikazeAlien))
                return CreateKamikaze();
            else if (enemyType == typeof(Spacemine))
                return CreateSpacemine(parameters);
            else
                throw new NotImplementedException($"The type {enemyType} is not implemented");
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
            var movePattern = new StraightLinePattern(3, 90);
            return (Enemy)new Spacemine(position, movePattern);
        }
        private static Enemy CreateKamikaze()
        {
            var position = new Position(Global.Width / 2, -50);
            var angle = (SplashKit.Rnd(0, 42) + 69);
            var movePattern = new StraightLinePattern(9, angle);
            return (Enemy)new KamikazeAlien(position, movePattern, angle);
        } 
        private static Enemy CreateGunship(Type type, int[] lastEnemy)
        {     
            int x, y;     
            Position position;
            Gun gun;
            IMoveStrategy movePattern;  
            if (type == typeof(BlueAlienship))
            {

                int randomX = SplashKit.Rnd(0, 6);
                if (lastEnemy[1] >= 100)
                {
                    x = (2 * randomX + 1) * 50; 
                    y = -50;
                } else
                {
                    while ( (2 * randomX + 1) * 50  == lastEnemy[0]) randomX = SplashKit.Rnd(0, 6);
                    x = (2 * randomX + 1) * 50; 
                    y = lastEnemy[1] - 100;
                } 
                gun = new Gun(3);
                movePattern = new StraightLinePattern(3, 90);
                position = new Position(x, y);
            } else if (type == typeof(PurpleAlienship))
            {
                if (lastEnemy[1] >= 100) y = -60;
                else y = lastEnemy[1] - 110;
                x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
                gun = new Gun(3);
                movePattern = new StraightLinePattern(2, 90);
                position = new Position(x, y);
            } else if (type == typeof(RedAlienship))
            {
                if (lastEnemy[1] >= 100) y = -50;
                else y = lastEnemy[1] - 110;
                x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
                position = new Position(x, y);
                gun = new Gun(2);
                movePattern = new ZigzagPattern(2, 3, y);
            } else if (type == typeof(Nightmare))
            {
                position = new Position(Global.Width / 2, -50);
                gun = new Gun(1.2, Bullet.Type.RedLaser, false);
                movePattern = new StraightLinePattern(3, 90);
            } else 
            {
                position = new Position(Global.Width / 2, -50);
                gun = new Gun(1, Bullet.Type.TripleLaser, false);
                movePattern = new ZigzagPattern(2, 2, -20, true);
            }
            var parameters = new object[]{position, movePattern, gun};
            return (Enemy)Activator.CreateInstance(type, parameters);
        }
        private static Enemy CreateAsteroid(int parameter)
        {
            var x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
            var y = (parameter > 50) ? -10 : parameter - 60;
            var position = new Position(x, y);
            var movePattern = new StraightLinePattern(4, 90);
            return (Enemy)new Asteroid(position, movePattern);
        }
    }
}