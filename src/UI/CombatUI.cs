using System;
using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Combat.Parties;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.Scene;

namespace MonsterCounty.UI
{
	public partial class CombatUI : CanvasLayer
	{
		public static readonly Singleton<CombatUI> Instance = new();
		
		[Export] private Array<Button> _buttons;
		[Export] private VBoxContainer _playerPartyHealthBars;
		[Export] private HealthBar _playerHealthBarTemplate;
		[Export] private VBoxContainer _enemyPartyHealthBars;
		[Export] private HealthBar _enemyHealthBarTemplate;

		[Export] private Sprite2D _playerArrow;
		private int _selectedPlayer;
		[Export] private Sprite2D _enemyArrow;
		public int SelectedEnemy;
		[Export] private float _arrowOffsetY = 10;
		
		private Party _playerParty;
		private Party _enemyParty;
		private CombatPlayer _currentPlayer;

		public void Load(Party playerParty, Party enemyParty)
		{
			if (!Instance.Create(this, false)) return;
			_playerParty = playerParty;
			_enemyParty = enemyParty;
			LoadCombatButtons();
			BindPlayerHealthBars();
			BindEnemyHealthBars();
			Reset();
		}

		private void Reset()
		{
			SelectedEnemy = 0;
			if (_enemyParty.Any()) SelectWithArrow(_enemyArrow, _enemyParty.Get(SelectedEnemy), ArrowPosition.Below);
		}

		public void RemoveActor(CombatActor actor)
		{
			actor.Visible = false;
			Reset();
		}

		private void LoadCombatButtons()
		{
			for (int i = 0; i < _buttons.Count; i++)
			{
				int capturedIndex = i;
				_buttons[i].Pressed += () => OnButtonPressed(capturedIndex);
			}
		}

		private void OnButtonPressed(int buttonIndex)
		{
			CombatScene.Instance.Get().ResolveTurn(_currentPlayer, buttonIndex);
		}

		public void NextPlayer(CombatPlayer player)
		{
			BindCombatButtons(player);
			SelectWithArrow(_playerArrow, player, ArrowPosition.Above);
		}
		
		private void BindCombatButtons(CombatPlayer player)
		{
			_currentPlayer = player;
			for (int i = 0; i < _buttons.Count; i++)
			{
				_buttons[i].Text = _currentPlayer.Controllers.Get<CombatController>().Actions[i].Name;
			}
		}
		
		private void BindHealthBars(VBoxContainer container, HealthBar template, Party party)
		{
			foreach (CombatActor member in party)
			{
				HealthBar bar = template.Duplicate() as HealthBar;
				bar.Visible = true;
				bar.Bind(member);
				container.AddChild(bar);
			}
			template.Visible = false;
		}

		// todo generalise to class CombatPartyUI
		public void BindPlayerHealthBars()
		{
			BindHealthBars(_playerPartyHealthBars, _playerHealthBarTemplate, _playerParty);
		}

		public void BindEnemyHealthBars()
		{
			BindHealthBars(_enemyPartyHealthBars, _enemyHealthBarTemplate, _enemyParty);
		}

		public override void _UnhandledInput(InputEvent inputEvent)
		{
			if (inputEvent.IsActionPressed(Direction.RIGHT)) SelectNextEnemy();
			else if (inputEvent.IsActionPressed(Direction.LEFT)) SelectPreviousEnemy();
		}
		
		private void SelectNextEnemy()
		{
			SelectedEnemy = Math.Min(SelectedEnemy+1, _enemyParty.Count()-1);
			SelectWithArrow(_enemyArrow, _enemyParty.Get(SelectedEnemy), ArrowPosition.Below);
		}

		private void SelectPreviousEnemy()
		{
			SelectedEnemy = Math.Max(SelectedEnemy-1, 0);
			SelectWithArrow(_enemyArrow, _enemyParty.Get(SelectedEnemy), ArrowPosition.Below);
		}

		private enum ArrowPosition
		{
			Above,
			Below
		}

		private void SelectWithArrow(Sprite2D arrow, CombatActor actor, ArrowPosition arrowPosition)
		{
			float offsetY = _arrowOffsetY + actor.Controllers.Get<VisualController>().GetSize().Y/2;
			if (arrowPosition == ArrowPosition.Above) offsetY *= -1;
			arrow.GlobalPosition = actor.GlobalPosition + new Vector2(0, offsetY);
		}
	}
}
