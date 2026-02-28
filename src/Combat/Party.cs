using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		public Party(Array<CombatActor> members)
		{
			_members = members;
		}

		public Party(Array<CombatActor> members, List<CombatActorState> membersData)
		{
			_members = members;
			LoadGameState(members, membersData);
		}

		private void LoadGameState(Array<CombatActor> members, List<CombatActorState> state)
		{
			for (int i = 0; i < state.Count; i++)
			{
				members[i].Controllers.Get<CombatController>().LoadGameState(state[i]);
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
		
		public void Remove(CombatActor member)
		{
			_members.Remove(member);
			member.Party = null;
		}
		
		public void LoadOpponents(Party opponents)
		{
			foreach (CombatActor member in _members)
			{
				member.Party = this;
				member.Opponents = opponents;
			}
		}
	}
}
