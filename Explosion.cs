using SplashKitSDK;

namespace SpaceShooter
{
    public class Explosion 
    {
        private AnimatedImage _image;
        private int _x, _y;
        public enum Type{
            RedLaser,
            Fire,
            Default
        }
        public bool AnimationEnded => _image.AnimationEnded;
        public Explosion(int x, int y, Type type)
        {
            var explosionSound = SplashKit.LoadSoundEffect("explosionSound", "explosion.mp3");
            SplashKit.PlaySoundEffect("explosionSound", (float)0.12);
            _x = x;
            _y = y;
            _image = SetAnimations(type);
        }
        public AnimatedImage SetAnimations(Type type)
        {
            switch(type)
            {
                case Type.RedLaser:
                    return SetLaserExplosion();
                case Type.Fire:
                    return SetFireExplosion();
                default:
                    return SetDefaultExplosion();
            }
        }
        private AnimatedImage SetLaserExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("laserExplosion", "Explosions/laserExplosion.png"); 
            var cellDetails = new int[]{180,180, 17, 1, 17};
            return new AnimatedImage("laserExplosion", "laserExplosion", bitmap, cellDetails);
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
        public void Draw()
        {
            _image.Draw(_x, _y);
        } 
        public void Update()
        {
            _y += 2;
        }
    }
}
    





    























