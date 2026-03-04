using System.Collections.Generic;
using Godot;
using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Combat;
using MonsterCounty.State;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class CombatController : ActionController<CombatActor>
	{
		[Export] public int MaxHealth;
		[Export] public CombatPosition CombatPosition;
		
		[Signal] public delegate void CurrentHealthChangedEventHandler(int health);

		public Party Party;
		public Party Opponents;
		private int _currentHealth;
		public int CurrentHealth
		{
			get => _currentHealth;
			set { EmitSignal(nameof(CurrentHealthChanged), value); _currentHealth = value; }
		}
		public bool IsAlive => CurrentHealth > 0;
		public bool HasChangedPosition;
		
		public void LoadGameState(CombatActorState state)
		{
			MaxHealth = state.MaxHealth;
			CurrentHealth = state.Health;
			foreach (var path in state.ActionScenePaths)
			{
				AddChildFromScenePath<CombatAction>(this, path);
			}
			CombatPosition = state.CombatPosition;
			LoadActions();
		}

		public void SaveGameState(List<CombatActorState> state)
		{
			var actionScenePaths = new string[Actions.Count];
			for (var i = 0; i < Actions.Count; i++)
			{
				actionScenePaths[i] = Actions[i].SceneFilePath;
			}
			state.Add(new CombatActorState(Actor));
			foreach (var child in GetChildren())
			{
				child.QueueFree();
			}
		}

		public virtual TurnResult StartTurn()
		{
			HasChangedPosition = false;
			return new TurnResult(); // shouldn't be returned
		}
		
		public virtual CombatActor ResolveTurn(int index) => null;
		
		public CombatActor TakeTurn(double delta)
		{
			return LoadDecision().Choose(Actions).Do(delta);
		}
		
		public bool ChangePosition(CombatPosition position)
		{
			if (HasChangedPosition) return false;
			Party.ChangePosition(Actor, position);
			HasChangedPosition = true;
			return true;
		}
	}
}
