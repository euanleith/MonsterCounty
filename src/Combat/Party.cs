using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Actor.World;
using MonsterCounty.State;

namespace MonsterCounty.Combat
{
	public partial class Party: Node, IEnumerable<CombatActor>
	{
		private Array<CombatActor> _members;
		private List<CombatActorState> _state;
		public CombatController HoldingTheLine { get; set; }
		
		IEnumerator<CombatActor> IEnumerable<CombatActor>.GetEnumerator() => _members.GetEnumerator();
		public IEnumerator GetEnumerator() => _members.GetEnumerator();
		public IEnumerable<CombatActor> Concat(Party other) => _members.Concat(other._members);
		public int Count() => _members.Count;
		public CombatActor Get(int index) => _members[index];
		public int IndexOf(CombatActor actor) =>_members.IndexOf(actor);

		public int IndexOf(CombatPosition position, Actor.Actor exclude=null)
		{
			return _members
				.Select((member, i) => new { member, i })
				.Where(x => x.member != exclude)
				.FirstOrDefault(x => x.member.CombatController.CombatPosition == position)?.i ?? -1;
		}

		public void Load(List<CombatActorState> state)
		{
			_members = new Array<CombatActor>(GetChildren().OfType<CombatActor>());
			foreach (CombatActor member in _members)
			{
				member.Load();
				member.CombatController.Party = this;
			}
			_state = state;
		}
		
		public void LoadFromGameState(List<CombatActorState> state)
		{
			Load(state);
			for (int i = 0; i < _state.Count; i++)
			{
				_members[i].LoadCombat(_state[i]);
			}
		}
		
		public void LoadFromGameStateIntoTemplates(List<CombatActorState> state)
		{
			Load(state);
			for (int i = 0; i < _state.Count; i++)
			{
				CombatPosition templatePosition = _members[i].CombatController.CombatPosition;
				_members[i].LoadCombat(_state[i]);
				MoveFromTemplatePosition(_members[i], templatePosition);
			}
		}
		
		public void Save(WorldActor worldActor=null)
		{
			_state.Clear();
			foreach (CombatActor member in _members)
			{
				CombatController combatController = member.CombatController;
				if (combatController.IsAlive) combatController.SaveGameState(_state, worldActor);
			}
		}
		
		public void LoadOpponents(Party opponents)
		{
			foreach (CombatActor member in _members)
			{
				CombatController combatController = member.CombatController;
				combatController.Opponents = opponents;
			}
		}

		public List<int> GetAliveMembersIndices()
		{
			return _members
				.Select((member, index) => new { member, index })
				.Where(x => x.member.CombatController.IsAlive)
				.Select(x => x.index)
				.ToList();
		}

		public bool IsDefeated()
		{
			return _members.All(member => !member.CombatController.IsAlive);
		}

		public void ChangePosition(CombatActor actor, CombatPosition position)
		{
			CombatController actorController = actor.CombatController;
			CombatActor swap = _members[IndexOf(position)];
			(actor.Position, swap.Position) = (swap.Position, actor.Position);
			(swap.CombatController.CombatPosition, actorController.CombatPosition) = (actorController.CombatPosition, position);
		}

		private void MoveFromTemplatePosition(CombatActor actor, CombatPosition templatePosition)
		{
			CombatController actorController = actor.CombatController;
			if (actorController.CombatPosition == templatePosition) return;
			CombatActor swap = _members[IndexOf(actorController.CombatPosition, actor)];
			(actor.Position, swap.Position) = (swap.Position, actor.Position);
			swap.CombatController.CombatPosition = templatePosition;
		}
	}
}
