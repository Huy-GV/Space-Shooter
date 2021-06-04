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
                return SpawnAsteroid(parameters[1]);
            else if (enemyType.IsSubclassOf(typeof(Alienship)))
                return CreateGunships(enemyType, parameters);
            else if (enemyType == typeof(KamikazeAlien))
                return new KamikazeAlien();
            else
                throw new NotImplementedException($"The type {enemyType} is not implemented");
        }
        private static Enemy CreateGunships(Type type, int[] lastEnemy)
        {     
            int x, y;     
            Position position;
            Gun gun;
            ICanMove movePattern;  
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
            } else
            {
                if (lastEnemy[1] >= 100) y = -50;
                else y = lastEnemy[1] - 110;
                x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
                position = new Position(x, y);
                gun = new Gun(2);
                movePattern = new ZigzagPattern(2, 3, y);
            }
            var parameters = new object[]{position, gun, movePattern};
            return (Enemy)Activator.CreateInstance(type, parameters);
        }
        private static Enemy SpawnAsteroid(object parameter)
        {
            if (parameter == null || parameter is not int)
                return (Enemy)Activator.CreateInstance(typeof(Asteroid), new object[]{null});
            else
                return (Enemy)Activator.CreateInstance(typeof(Asteroid), new object[]{parameter});
        }
    }
}