using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    //TODO: delete alienship subclasses and create subtypes by attaching different components in the factory ?
    public abstract class Alienship : Enemy, IHaveGun
    {
        protected Gun _gun;
        public bool OverheatEnded => _gun.OverheatEnded;
        public Alienship(Position position, Gun gun, IMoveStrategy movePattern) : base(position, movePattern)
        {
            _gun = gun;
        }
        public override void Update()
        {
            base.Update();
            _gun.Update();
        }
        public Bullet Shoot() => _gun.OpenFire(X, Y, Angle, 0); 
    }
}