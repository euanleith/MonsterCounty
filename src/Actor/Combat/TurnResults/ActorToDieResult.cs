namespace MonsterCounty.Actor.Combat.TurnResults
{
    public class ActorToDieResult(CombatActor actor) : TurnResult
    {
        public readonly CombatActor Actor = actor;
    }
}