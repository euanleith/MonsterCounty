using System;
using System.Linq;
using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Combat
{
    public class Combat
    {
        public static Combat Instance { get; private set; }

        public static event Action<CombatActor> ActorDying;
        public static event Action<Party> Exiting;
        
        private readonly Party _playerParty;
        private readonly Party _enemyParty;
        private readonly CircularLinkedList<CombatActor> _turnQueue;

        public Combat(Party playerParty, Party enemyParty)
        {
            if (Instance == null) Instance = this;
            else return;
            _playerParty = playerParty;
            _enemyParty = enemyParty;
            _playerParty.Load(_enemyParty);
            _enemyParty.Load(_playerParty);
            var shuffled = _playerParty.Concat(_enemyParty).OrderBy(x => new Random().Next()).ToArray();
            _turnQueue = new CircularLinkedList<CombatActor>(shuffled);
            StartTurn();
        }
        
        private void StartTurn()
        {
            CombatActor next = _turnQueue.Peek();
            if (next.Controllers.Get<CombatController>().IsAlive)
            {
                TurnResult res = next.StartTurn();
                if (res.WaitingForInput) return;
                if (res.ActorToDie != null)
                {
                    if (!OnActorDie(res.ActorToDie)) return;
                }
            }
            NextTurn();
        }

        public void ResolveTurn(CombatPlayer player, int index)
        {
            CombatActor actorToDie = player.ResolveTurn(index);
            if (actorToDie != null)
            {
                if (!OnActorDie(actorToDie)) return;
            }
            NextTurn();
        }

        private void NextTurn()
        {
            _turnQueue.Next();
            StartTurn();
        }
        
        private bool OnActorDie(CombatActor actor)
        {
            GD.Print($"{actor.Name} died!");
            ActorDying?.Invoke(actor);
            if (actor.Party.IsDefeated())
            {
                Exit(actor.Party);
                return false;
            }
            return true;
        }

        private void Exit(Party losers)
        {
            GD.Print("exiting combat");
            Instance = null;
            SaveGameState();
            Exiting?.Invoke(losers);
        }
        
        private void SaveGameState()
        {
            _playerParty.SaveGameState(GameState.PlayerParty);
        }
    }
}