using System.Collections.Generic;
using Godot;
using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Combat;
using MonsterCounty.State;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.Actor.Controllers
{
	public partial class CombatController : ActionController<CombatActor>
	{
		[Export] public int MaxHealth;
		
		[Signal] public delegate void CurrentHealthChangedEventHandler(int health);

		public Party Opponents;
		private int _currentHealth;
		public int CurrentHealth
		{
			get => _currentHealth;
			set { EmitSignal(nameof(CurrentHealthChanged), value); _currentHealth = value; }
		}
		public bool IsAlive => CurrentHealth > 0;

		public void LoadGameState(CombatActorState state)
		{
			MaxHealth = state.MaxHealth;
			CurrentHealth = state.Health;
			foreach (var path in state.ActionScenePaths)
			{
				AddChildFromScenePath<CombatAction>(this, path);
			}
			LoadActions();
		}

		public void SaveGameState(List<CombatActorState> state)
		{
			var actionScenePaths = new string[Actions.Count];
			for (var i = 0; i < Actions.Count; i++)
			{
				actionScenePaths[i] = Actions[i].SceneFilePath;
			}
			state.Add(new CombatActorState(MaxHealth, CurrentHealth, actionScenePaths, GetNodeId(this)));
			foreach (var child in GetChildren())
			{
				child.QueueFree();
			}
		}
		
		public CombatActor TakeTurn(double delta)
		{
			return LoadDecision().Choose(Actions).Do(delta);
		}
	}
}
