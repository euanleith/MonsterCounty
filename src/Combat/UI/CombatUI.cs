using Godot;
using System.Linq;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.UI;

namespace MonsterCounty.Combat.UI
{
	public partial class CombatUI : CanvasLayer
	{
		public static readonly Singleton<CombatUI> Instance = new();
		
		[Export] private HealthBarContainer _playerPartyHealthBars;
		[Export] private HealthBarContainer _enemyPartyHealthBars;
		[Export] private ButtonContainer _buttons;
		[Export] private Arrow _playerArrow;
		[Export] private Arrow _enemyArrow;
		[Export] private float _arrowOffsetY = 10;
		public int SelectedEnemy => _enemyArrow.SelectedIndex;
		
		private Party _playerParty;
		private Party _enemyParty;
		private CombatPlayer _currentPlayer;

		public void Load(Party playerParty, Party enemyParty)
		{
			if (!Instance.Create(this, false)) return;
			_playerParty = playerParty;
			_enemyParty = enemyParty;
			HideDeadPartyMembers(_playerParty);
			HideDeadPartyMembers(_enemyParty);
			_buttons.Load(LoadButton, RebindButton);
			_playerPartyHealthBars.LoadAndBind(BindHealthBar, _playerParty);
			_enemyPartyHealthBars.LoadAndBind(BindHealthBar, _enemyParty);
			_playerArrow.Load(Arrow.ArrowPosition.Above, _arrowOffsetY);
			_enemyArrow.LoadAndBind(Arrow.ArrowPosition.Below, _arrowOffsetY, _enemyParty);
			BindCombatEvents();
			Reset();
		}

		private void Reset()
		{
			_enemyArrow.Reset();
		}

		private void HideDeadPartyMembers(Party party)
		{
			foreach (CombatActor member in party)
			{
				if (!member.Controllers.Get<CombatController>().IsAlive)
				{
					member.Visible = false;
				}
			}
		}

		private void LoadButton(Button button, int index)
		{
			button.Pressed += () => OnButtonPressed(index);
		}

		private void RebindButton(Button button, int index)
		{
			button.Text = _currentPlayer.Controllers.Get<CombatController>().Actions[index].Name;
		}

		private void OnButtonPressed(int buttonIndex)
		{
			Combat.Instance.ResolveTurn(_currentPlayer, buttonIndex);
		}

		private void BindHealthBar(HealthBar bar, CombatActor actor)
		{
			bar.Bind(actor);
		}
		
		public void NextPlayer(CombatPlayer player)
		{
			_currentPlayer = player;
			_buttons.Rebind();
			_playerArrow.Rebind(player);
		}
		
		private void RemoveActor(CombatActor actor)
		{
			actor.Visible = false;
			Reset();
		}

		private void BindCombatEvents()
		{
			Combat.ActorDying += RemoveActor;
		}

		public override void _ExitTree()
		{
			Combat.ActorDying -= RemoveActor;
		}

		public override void _UnhandledInput(InputEvent inputEvent)
		{
			if (inputEvent.IsActionPressed(Direction.RIGHT)) _enemyArrow.Next();
			else if (inputEvent.IsActionPressed(Direction.LEFT)) _enemyArrow.Prev();
		}
	}
}
