using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Spacemine : Enemy
    {
        public Spacemine(Position position, IMoveStrategy movePattern, int damage) : base(position, movePattern, EnemyType.Spacemine)
        {
            CollisionDamage = damage;
            Image = SetAnimations();
        }
        private Image SetAnimations()
        {
            var bitmap = SplashKit.LoadBitmap("blueSpacemine", "Spacemines/blueSpacemine.png");
            var cellDetails = new int[]{120, 120, 2, 1, 2};
            return new AnimatedImage("spacemineScript", "floating", bitmap, cellDetails);
        }
    }
}