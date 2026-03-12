using System.Collections.Generic;
using Godot;
using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Combat.TurnResults;
using MonsterCounty.Actor.Decisions.Combat;
using MonsterCounty.Actor.World;
using MonsterCounty.Combat;
using MonsterCounty.State;
using MonsterCounty.Utilities;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class CombatController : ActionController<CombatActor>
	{
		[Export] public int MaxHealth;
		[Export] public CombatPosition CombatPosition;
		[Export] public Weapon Weapon = new();
		
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
		public bool IsDefending { get; set; }
		public CombatController Focus;

		public override void Load(Actor actor)
		{
			base.Load(actor);
			CurrentHealth = MaxHealth;
			MonsterCounty.Combat.Combat.ActorDying += OnActorDie;
		}

		public override void _ExitTree()
		{
			MonsterCounty.Combat.Combat.ActorDying -= OnActorDie;
		}
		
		// todo should use existing load and save functions
		public void LoadGameState(CombatActorState state)
		{
			MaxHealth = state.MaxHealth;
			CurrentHealth = state.CurrentHealth;
			Actions = [];
			foreach (var path in state.ActionResourcePaths)
			{
				CombatAction action = GD.Load<CombatAction>(path).Duplicate(true) as CombatAction;
				action.SetMeta(META_INSTANCE_RESOURCE_PATH, path);
				Actions.Add(action);
			}
			CombatPosition = state.CombatPosition;
			Weapon = state.Weapon;
			LoadActions();
		}

		public void SaveGameState(List<CombatActorState> state, WorldActor worldActor=null)
		{
			state.Add(new CombatActorState(Actor as CombatActor, worldActor));
			foreach (var child in GetChildren()) // todo replace with removing resources
			{
				child.QueueFree();
			}
		}

		public virtual TurnResult StartTurn()
		{
			HasChangedPosition = false;
			return new PassResult(); // shouldn't be returned
		}
		
		public virtual TurnResult ResolveTurn(int index) => null;
		
		public CombatActor TakeTurn(double delta)
		{
			var decision = Decision.Choose(this);
			if (decision is CombatChoice combatDecision) ChangePosition(combatDecision.CombatPosition);
			return decision.Action.Do(delta);
		}
		
		public bool ChangePosition(CombatPosition position)
		{
			if (HasChangedPosition) return false;
			Party.ChangePosition(Actor, position);
			HasChangedPosition = true;
			return true;
		}

		public int RollToDefend()
		{
			int nDice = 1;
			if (IsDefending) nDice++;
			IsDefending = false;
			return Dice.Roll(nDice, 8);
		}

		private void OnActorDie(CombatActor actor)
		{
			LoseFocus(actor);
		}

		private void LoseFocus(CombatActor actor)
		{
			if (Focus == actor.Controllers.Get<CombatController>()) Focus = null;
		}

		private void StopHoldingTheLine(CombatActor actor)
		{
			if (actor == Actor && Party.HoldingTheLine == this)
			{
				GD.Print($"{Actor.Name} no longer holding the line");
				Party.HoldingTheLine = null;
			}
		}
	}
}
