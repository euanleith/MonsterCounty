namespace MonsterCounty.Actor.Combat
{
    public struct TurnResult(bool waitingForInput, CombatActor actor=null)
    {
        public bool WaitingForInput = waitingForInput;
        public CombatActor ActorToDie = actor;
    }
}