using System.Collections.Generic;
using Drawable;

namespace Interface
{
    public interface IKillable
    {
        public Image Image{get;}
        public int X{get;}
        public int Y{get;}
        public abstract void LoseHealth(int damage);
    }
}