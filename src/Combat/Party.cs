using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.State;

namespace MonsterCounty.Combat
{
	public class Party: IEnumerable<CombatActor>
	{
		private readonly Array<CombatActor> _members;
		
		IEnumerator<CombatActor> IEnumerable<CombatActor>.GetEnumerator() => _members.GetEnumerator();
		public IEnumerator GetEnumerator() => _members.GetEnumerator();
		public IEnumerable<CombatActor> Concat(Party other) => _members.Concat(other._members);
		public int Count() => _members.Count;
		public CombatActor Get(int index) => _members[index];

		public int IndexOf(CombatPosition position, Actor.Actor exclude=null)
		{
			return _members
				.Select((member, i) => new { member, i })
				.Where(x => x.member != exclude)
				.FirstOrDefault(x => x.member.Controllers.Get<CombatController>().CombatPosition == position)?.i ?? -1;
		}

		public Party(Array<CombatActor> templates, List<CombatActorState> state)
		{
			_members = templates;
			LoadGameState(state);
		}
		
		private void LoadGameState(List<CombatActorState> state)
		{
			for (int i = 0; i < state.Count; i++)
			{
				CombatPosition templatePosition = _members[i].Controllers.Get<CombatController>().CombatPosition;
				_members[i].Controllers.Get<CombatController>().LoadGameState(state[i]);
				MoveFromTemplatePosition(_members[i], templatePosition);
			}
		}
		
		public void SaveGameState(List<CombatActorState> state)
		{
			state.Clear();
			foreach (CombatActor member in _members)
			{
				member.Controllers.Get<CombatController>().SaveGameState(state);
			}
		}
		
		public void Load(Party opponents)
		{
			foreach (CombatActor member in _members)
			{
				CombatController combatController = member.Controllers.Get<CombatController>();
				combatController.Party = this;
				combatController.Opponents = opponents;
			}
		}

		public List<int> GetAliveMembersIndices()
		{
			return _members
				.Select((member, index) => new { member, index })
				.Where(x => x.member.Controllers.Get<CombatController>().IsAlive)
				.Select(x => x.index)
				.ToList();
		}

		public bool IsDefeated()
		{
			return _members.All(member => !member.Controllers.Get<CombatController>().IsAlive);
		}

		public void ChangePosition(Actor.Actor actor, CombatPosition position)
		{
			CombatController actorController = actor.Controllers.Get<CombatController>();
			CombatActor swap = _members[IndexOf(position)];
			(actor.Position, swap.Position) = (swap.Position, actor.Position);
			(swap.Controllers.Get<CombatController>().CombatPosition, actorController.CombatPosition) = (actorController.CombatPosition, position);
		}

		private void MoveFromTemplatePosition(Actor.Actor actor, CombatPosition templatePosition)
		{
			CombatController actorController = actor.Controllers.Get<CombatController>();
			if (actorController.CombatPosition == templatePosition) return;
			CombatActor swap = _members[IndexOf(actorController.CombatPosition, actor)];
			(actor.Position, swap.Position) = (swap.Position, actor.Position);
			swap.Controllers.Get<CombatController>().CombatPosition = templatePosition;
		}
	}
}
