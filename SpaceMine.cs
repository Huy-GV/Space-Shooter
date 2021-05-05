using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class Spacemine : Enemy
    {
        public Spacemine(int lastEnemyX, int lastEnemyY): base()
        {
            ExplosionType = Explosion.Type.Default;
            CollisionDamage = 16;
            Image = SetAnimations();
            int random = SplashKit.Rnd(0,3);
            _movePattern = new StraightLinePattern(3, SetX(lastEnemyX, random), SetY(lastEnemyX, random), 90);
        }
        private int SetX(int lastEnemyX, int random)
        {
            if (lastEnemyX >= 120)
                return (2 * random + 1) * 100; 
            else
                return (2 * SplashKit.Rnd(0,3) + 1) * 100;
        }
        private int SetY(int lastEnemyY, int random)
        {
            if (lastEnemyY >= 120)
                return -140;
            else
                return lastEnemyY - 240;
        }
        private Image SetAnimations()
        {
            var bitmap = SplashKit.LoadBitmap("blueSpacemine", "Spacemines/blueSpacemine.png");
            var cellDetails = new int[]{120, 120, 2, 1, 2};
            return new AnimatedImage("spacemineScript", "floating", bitmap, cellDetails);
        }
        public Spacemine() : this(Global.Width, Global.Height){}
        public override void Draw()
        {
            Image.Draw(_movePattern.X, _movePattern.Y);
        } 
        public override void Update()
        { 
            Move();
        }
    }
}