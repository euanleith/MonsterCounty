namespace MonsterCounty.Actor.World
{
    public partial class WorldActor : Actor
    {
        protected World _world;

        public void Init(World world)
        {
            _world = world;
        }
    }
}