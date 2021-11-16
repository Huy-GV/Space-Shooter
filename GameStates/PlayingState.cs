using Gameplay;
using Main;


namespace GameStates
{
    public class PlayingState : State
    {
        private static Session _session;
        private static bool _sessionStarted;
        public PlayingState(Game game) : base(game)
        {
            _session = null;
            _sessionStarted = false;
        }
        public override void Draw()
        {
            if (_sessionStarted)
                _session.Draw();
        }
        public override void ProcessInput()
        {
            if (_sessionStarted && _session.CurrentState != Session.State.Paused)
                _session.ProcessInput();
        }
///<summary>
///Prevents the state from starting a new session when the player jumps back from the paused state 
///</summary>
        public override void Update()
        {
            if (!_sessionStarted)
            {
                _session = new Session(_game.SpaceshipChoice, _game.GameMode);
                _sessionStarted = true;
            } else
            {
                _session.Update();
                if (_session.CurrentState == Session.State.Over)
                {
                    _game.SetState(_game.GameOverState);
                    _sessionStarted = false;
                } else if (_session.CurrentState == Session.State.Paused)
                    _game.SetState(_game.PausedGameState);
            }             
        }
        public static void DeleteSession()=> _sessionStarted = false;
        public static void ContinueSession()=> _session.Continue();
         
    }
}