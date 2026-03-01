namespace MonsterCounty.Actor.Combat
{
    public struct TurnResult(bool waitingForInput, CombatActor actor=null)
    {
        public readonly bool WaitingForInput = waitingForInput;
        public readonly CombatActor ActorToDie = actor;
    }
}