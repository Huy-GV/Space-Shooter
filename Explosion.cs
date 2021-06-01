using SplashKitSDK;
using System;

namespace SpaceShooter
{
    public class Explosion : DrawableObject
    {
        public enum Type{
            RedLaser,
            BlueLaser,
            Fire,
            Default
        }
        public bool AnimationEnded => ((AnimatedImage)Image).AnimationEnded;
        public Explosion(int x, int y, Type type)
        {
            var explosionSound = SplashKit.LoadSoundEffect("explosionSound", "explosion.mp3");
            SplashKit.PlaySoundEffect("explosionSound", (float)0.12);
            _position.X = x;
            _position.Y = y;
            Image = SetAnimations(type);
        }
        public AnimatedImage SetAnimations(Type type)
        {
            switch(type)
            {
                case Type.RedLaser:
                    return SetRedLaserExplosion();
                case Type.Fire:
                    return SetFireExplosion();
                case Type.BlueLaser:
                    return SetBlueLaserExplosion();
                case Type.Default:
                    return SetDefaultExplosion();
                default:
                    throw new NotImplementedException($"The type {type} does not exist");
            }
        }
        private AnimatedImage SetRedLaserExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("laserExplosion", "Explosions/laserExplosion.png"); 
            var cellDetails = new int[]{180,180, 17, 1, 17};
            return new AnimatedImage("laserExplosion", "laserExplosion", bitmap, cellDetails);
        }
        private AnimatedImage SetBlueLaserExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("blueExplosion", "Explosions/blueExplosion.png"); 
            var cellDetails = new int[]{120, 120, 17, 1, 17};
            return new AnimatedImage("blueLaserExplosion", "blueLaserExplosion", bitmap, cellDetails);
        }
        private AnimatedImage SetFireExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("fireExplosion", "Explosions/fireExplosion.png"); 
            var cellDetails = new int[]{100, 100, 15, 1, 15};
            return new AnimatedImage("fireExplosion", "fireExplosion", bitmap, cellDetails);
        }
        private AnimatedImage SetDefaultExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("defaultExplosion", "Explosions/defaultExplosion.png"); 
            var cellDetails = new int[]{180, 180, 20, 1, 20};
            return new AnimatedImage("defaultExplosion", "defaultExplosion", bitmap, cellDetails);
        }
        public void Update()
        {
            _position.Y += 2;
        }
    }
}
    




