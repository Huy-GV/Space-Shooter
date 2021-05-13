using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Gun
    {

        private double _coolDownTime = 0;
        private double _coolDownLimit;
        private Bullet.Type _type;
        private bool _hasSound;
        public Gun(double coolDownLimit, Bullet.Type type, bool hasSound)
        {

            _coolDownLimit = coolDownLimit;
            _type = type;
            _hasSound = hasSound;
        }
        public Gun() : this ( 2.5, Bullet.Type.BlueLaser, false) { }
        public Gun(double coolDownLimit) : this(coolDownLimit, Bullet.Type.BlueLaser, false){ }
        public bool CoolDownEnded => _coolDownTime == 0;
        public Bullet OpenFire(int x, int y, int moveAngle, int imageAngle)
        {
            _coolDownTime = _coolDownLimit;
            return new Bullet(x, y, _type, _hasSound, moveAngle, imageAngle);
        }
        public void Update()
        {
            _coolDownTime = (_coolDownTime > 0 ? _coolDownTime - 1/(double)60 : 0);
        }
    }
}