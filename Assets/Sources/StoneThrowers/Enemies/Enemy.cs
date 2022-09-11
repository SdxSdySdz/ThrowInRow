using Sources.GameLogic.AI.Bots;
using Sources.GameLogic.Core;

namespace Sources.StoneThrowers.Enemies
{
    public abstract class Enemy : StoneThrower
    {
        public abstract void ForceMove(GameState game);
    }
    
    public abstract class Enemy<TBot> : Enemy 
        where TBot : Bot, new()
    {
        private TBot _bot;

        private void Awake()
        {
            _bot = new TBot();
        }

        public override void ForceMove(GameState game)
        {
            RequestMove(_bot.SelectMove(game));
        }
    }
}