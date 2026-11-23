using System;
using Godot;
using MonsterCounty.UI;

namespace MonsterCounty.Combat.UI
{
	public partial class ChooseAction : Control, CombatUiPhase
	{
		[Export] private ButtonContainer _actionButtons;
		[Export] private Button _positionButton;
		[Export] private Arrow _playerArrow;

		private CombatUiContext _context;
		private Action<InputEvent> _inputEvent;
		
		public void Load(CombatUiContext context)
		{
			_context = context;
			
			_actionButtons.Load(LoadActionButton, RebindActionButton);
			_actionButtons.Rebind();
			
			_positionButton.Pressed += OnPositionButtonPressed;
			
			_playerArrow.Load();
			_playerArrow.Rebind(_context.CurrentPlayer);
		}
		
		private void LoadActionButton(Button button, int index)
		{
			button.Pressed += () => _context.Ui.OnActionButtonPressed(index);
		}

		private void RebindActionButton(Button button, int index)
		{
			button.Text = _context.CurrentPlayer.CombatController.Actions[index].Name;
		}
		
		private void OnPositionButtonPressed()
		{
			if (_context.CurrentPlayer.CombatController.HasChangedPosition)
			{
				GD.Print(_context.CurrentPlayer.Name + " has already moved this turn");
				return;
			}
			_inputEvent = PlayerPositionSelectionInput;
			_actionButtons.Disable();
		}
		
		private void PlayerPositionSelectionInput(InputEvent inputEvent)
		{
			CombatPosition target;
			bool wantsToMove = true;
			CombatPosition? position = CombatUi.DirectionToPosition(inputEvent);
			if (position.HasValue) target = position.Value;
			else if (inputEvent.IsActionPressed("escape"))
			{
				target = _context.CurrentPlayer.CombatController.CombatPosition;
				wantsToMove = false;
			}
			else return;
			_actionButtons.Enable();
			if (wantsToMove)
			{
				_context.CurrentPlayer.CombatController.ChangePosition(target);
				_playerArrow.Rebind(_context.CurrentPlayer);
				_actionButtons.Focus(0);
			}
			_inputEvent = null;
		}
		
		public override void _UnhandledInput(InputEvent inputEvent) => _inputEvent?.Invoke(inputEvent);
	}
}
