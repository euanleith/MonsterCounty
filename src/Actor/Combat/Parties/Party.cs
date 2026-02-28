using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace MonsterCounty.Actor.Combat.Parties
{
    public class Party(Array<CombatActor> members) : IEnumerable<CombatActor>
    {
        [Export] private Array<CombatActor> _members = members;

        IEnumerator<CombatActor> IEnumerable<CombatActor>.GetEnumerator() => _members.GetEnumerator();
        public IEnumerator GetEnumerator() => _members.GetEnumerator();
        public IEnumerable<CombatActor> Concat(Party other) => _members.Concat(other._members);
        public int Count() => _members.Count;
        public CombatActor Get(int index) => _members[index];
        
        
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