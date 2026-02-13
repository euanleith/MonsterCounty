namespace MonsterCounty.Actor.Controllers
{
    public abstract partial class InteractionController<S, R> : ActionController<R> 
        where S : InteractionController<S, R>
        where R : InteractionController<R, S> 
    {
        public bool IsInteracting { get; private set; }
        public bool IsInteractable { get; private set; } // todo not using this currently
        protected R Interlocutor; 

        public void StartInteracting()
        {
            IsInteracting = true;
            // Actor.Controllers.Get<MovementController>().Stop(); // todo i guess this should actually pause the game instead? though might not be necessary at all with dialogue plugin
        }
        
        public void StopInteracting()
        {
            IsInteracting = false;
            Interlocutor?.StopInteracting();
        }
    }
}