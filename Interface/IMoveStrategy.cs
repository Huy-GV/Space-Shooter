using SplashKitSDK;
using Main;
using Drawable;

namespace Interface 
{
    public interface IMoveStrategy
    {
        Position Move(Position position);
    }
}