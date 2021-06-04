using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Gun
    {

        private double _overheadTime = 0;
        private double _overheatDuration;
        private Bullet.Type _type;
        private bool _hasSound;
        public Gun(double coolDownLimit, Bullet.Type type, bool hasSound)
        {
            _overheatDuration = coolDownLimit;
            _type = type;
            _hasSound = hasSound;
        }
        public Gun() : this ( 2.5, Bullet.Type.BlueLaser, false) { }
        public Gun(double coolDownLimit) : this(coolDownLimit, Bullet.Type.BlueLaser, false){ }
        public bool OverheatEnded => _overheadTime == 0;
        public Bullet OpenFire(int x, int y, int moveAngle, int imageAngle)
        {
            if (_hasSound) SplashKit.LoadSoundEffect("laserSound", "laser.mp3").Play();
            _overheadTime = _overheatDuration;
            return new Bullet(x, y, _type, moveAngle, imageAngle);
        }
        public void Update()
        {
            _overheadTime = (_overheadTime > 0 ? _overheadTime - 1/(double)60 : 0);
        }
    }
}