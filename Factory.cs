using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public static class EnemyFactory
    {
        public static Enemy SpawnEnemy(Type enemyType, object[] parameters)
        {
            if (enemyType == typeof(Asteroid))
                return SpawnAsteroid(parameters[1]);
            else if ((enemyType == typeof(BlueAlienship) || 
                enemyType == typeof(PurpleAlienship) || 
                enemyType == typeof(RedAlienship) || 
                enemyType == typeof(Spacemine)))
                return (Enemy)Activator.CreateInstance(enemyType, parameters);
            else if (enemyType == typeof(KamikazeAlien))
                return new KamikazeAlien();
            else
                throw new NotImplementedException($"The type {enemyType} does not exist");
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