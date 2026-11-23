using System;
using Godot;

namespace MonsterCounty.Combat.UI
{
    public partial class ChooseTarget : Control, CombatUiPhase
    {
        [Export] private Button _button;
        [Export] private Arrow _arrow;

        private CombatUiContext _context;
        private Action<InputEvent> _inputEvent;
        
        public void Load(CombatUiContext context)
        {
            _context = context;
            
            _button.Pressed += () => _context.Ui.OnTargetSelected();
            
            _context.SelectedTarget = _arrow.LoadAndBind(_context.EnemyParty); 
            _inputEvent = EnemySelectionInput;
        }
        
        private void EnemySelectionInput(InputEvent inputEvent)
        {
            CombatPosition target;
            CombatPosition? position = CombatUi.DirectionToPosition(inputEvent, invertX: true, invertY: true);
            if (position.HasValue) target = position.Value;
            else return;
            _context.SelectedTarget = _arrow.Rebind(target);
        }
        
        public override void _UnhandledInput(InputEvent inputEvent) => _inputEvent?.Invoke(inputEvent);
    }
}