using System;
using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Combat
{
    public class Combat
    {
        public static Combat Instance { get; private set; }

        public static event Action<CombatActor> ActorDying;
        public static event Action Exiting;
        
        private readonly Party _playerParty;
        private readonly CircularLinkedList<CombatActor> _turnQueue;

        public Combat(Array<CombatActor> playerPartyArr, Array<CombatActor> enemyPartyArr)
        {
            if (Instance == null) Instance = this;
            else return;
            _playerParty = GameState.Party != null ? new(playerPartyArr, GameState.Party) : new(playerPartyArr);
            Party enemyParty = new(enemyPartyArr);
            _playerParty.LoadOpponents(enemyParty);
            enemyParty.LoadOpponents(_playerParty);
            var shuffled = _playerParty.Concat(enemyParty).OrderBy(x => new Random().Next()).ToArray();
            _turnQueue = new CircularLinkedList<CombatActor>(shuffled);
            StartTurn();
        }
        
        private void StartTurn()
        {
            TurnResult res = _turnQueue.Peek().StartTurn();
            if (res.WaitingForInput) return;
            if (res.ActorToDie != null)
            {
                if (!OnActorDie(res.ActorToDie)) return;
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
            RemoveActor(actor);
            ActorDying?.Invoke(actor);
            if (!_turnQueue.Contains(actor.GetType()))
            {
                Exit();
                return false;
            }
            return true;
        }

        private void RemoveActor(CombatActor actor)
        {
            _turnQueue.Remove(actor);
            actor.Party.Remove(actor);
        }

        private void Exit()
        {
            GD.Print("exiting combat");
            Instance = null;
            SaveGameState();
            Exiting?.Invoke();
        }
        
        private void SaveGameState()
        {
            _playerParty.SaveGameState(GameState.Party);
        }
    }
}