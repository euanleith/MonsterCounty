using System;
using Godot;
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
		[Export] private Button _positionButton;
		[Export] private Arrow _playerArrow;
		[Export] private Arrow _enemyArrow;
		[Export] private float _arrowOffsetY = 10;
		public int SelectedEnemy => _enemyArrow.SelectedIndex;
		
		private Party _playerParty;
		private Party _enemyParty;
		private CombatPlayer _currentPlayer;
		private Action<InputEvent> _inputEvent;

		public void Load(Party playerParty, Party enemyParty)
		{
			if (!Instance.Create(this, false)) return;
			_playerParty = playerParty;
			_enemyParty = enemyParty;
			HideDeadPartyMembers(_playerParty);
			HideDeadPartyMembers(_enemyParty);
			_buttons.Load(LoadButton, RebindButton);
			_positionButton.Pressed += OnPositionButtonPressed;
			_playerPartyHealthBars.LoadAndBind(BindHealthBar, _playerParty);
			_enemyPartyHealthBars.LoadAndBind(BindHealthBar, _enemyParty);
			_playerArrow.Load(Arrow.ArrowPosition.Above, _arrowOffsetY);
			_enemyArrow.LoadAndBind(Arrow.ArrowPosition.Below, _arrowOffsetY, _enemyParty);
			_inputEvent = EnemySelectionInput;
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
			button.Pressed += () =>
			{
				Combat.Instance.ResolveTurn(_currentPlayer, index);
			};
		}

		private void RebindButton(Button button, int index)
		{
			button.Text = _currentPlayer.Controllers.Get<CombatController>().Actions[index].Name;
		}

		private void OnPositionButtonPressed()
		{
			if (_currentPlayer.Controllers.Get<CombatController>().HasChangedPosition)
			{
				GD.Print(_currentPlayer.Name + " has already moved this turn");
				return;
			}
			_inputEvent = PlayerPositionSelectionInput;
			_buttons.Disable();
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

		public override void _UnhandledInput(InputEvent inputEvent) => _inputEvent(inputEvent);

		private void EnemySelectionInput(InputEvent inputEvent)
		{
			CombatPosition target;
			if (inputEvent.IsActionPressed(Direction.RIGHT)) target = CombatPosition.Left;
			else if (inputEvent.IsActionPressed(Direction.LEFT)) target =CombatPosition.Right;
			else if (inputEvent.IsActionPressed(Direction.UP)) target = CombatPosition.Back;
			else if (inputEvent.IsActionPressed(Direction.DOWN)) target = CombatPosition.Front;
			else return;
			_enemyArrow.MoveToPosition(target);
		}

		private void PlayerPositionSelectionInput(InputEvent inputEvent)
		{
			CombatPosition target;
			bool wantsToMove = true;
			if (inputEvent.IsActionPressed(Direction.RIGHT)) target = CombatPosition.Right;
			else if (inputEvent.IsActionPressed(Direction.LEFT)) target = CombatPosition.Left;
			else if (inputEvent.IsActionPressed(Direction.UP)) target = CombatPosition.Front;
			else if (inputEvent.IsActionPressed(Direction.DOWN)) target = CombatPosition.Back;
			else if (inputEvent.IsActionPressed("escape"))
			{
				target = _currentPlayer.Controllers.Get<CombatController>().CombatPosition;
				wantsToMove = false;
			}
			else return;
			if (wantsToMove)
			{
				_currentPlayer.Controllers.Get<CombatController>().ChangePosition(target);
				_playerArrow.Rebind(_currentPlayer);
			}
			_buttons.Enable();
			_inputEvent = EnemySelectionInput;
		}
	}
}
