namespace MonsterCounty.Actor.Decisions
{
    public class Choice<TAction>(TAction action)
    {
        public readonly TAction Action = action;
    }
}