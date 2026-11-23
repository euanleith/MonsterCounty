using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Model;

namespace MonsterCounty.Combat.UI
{
	public partial class CombatUi : CanvasLayer
	{
		public static readonly Singleton<CombatUi> Instance = new();
		
		[Export] private HealthBarContainer _playerPartyHealthBars;
		[Export] private HealthBarContainer _enemyPartyHealthBars;
		[Export] private Control _combatPhaseContainer;
		[Export] private PackedScene _chooseActionScene;
		[Export] private PackedScene _chooseTargetScene;
		
		private CombatUiContext _context;
		private Control _currentCombatPhase;

		public void Load(Party playerParty, Party enemyParty)
		{
			if (!Instance.Create(this, false)) return;
			_context = new CombatUiContext
			{ 
				Ui = this,
				PlayerParty = playerParty,
				EnemyParty = enemyParty
			};
			HideDeadPartyMembers(_context.PlayerParty);
			HideDeadPartyMembers(_context.EnemyParty);
			_playerPartyHealthBars.LoadAndBind(BindHealthBar, _context.PlayerParty);
			_enemyPartyHealthBars.LoadAndBind(BindHealthBar, _context.EnemyParty);
			BindCombatEvents();
		}
		
		private void HideDeadPartyMembers(Party party)
		{
			foreach (CombatActor member in party)
			{
				if (!member.CombatController.IsAlive)
				{
					member.Visible = false;
				}
			}
		}
		
		private void ChangeCombatPhase<T>(PackedScene scene) 
			where T : Control, CombatUiPhase
		{
			_currentCombatPhase?.QueueFree();
			T state = scene.Instantiate<T>();
			_combatPhaseContainer.AddChild(state);
			state.Load(_context);
			_currentCombatPhase = state;
		}

		public void OnActionButtonPressed(int actionIndex)
		{
			_context.SelectedAction = actionIndex;
			if (actionIndex == 0) // todo any better way? i want to only go to _chooseTargetScene when 'Attack' action is selected
			{
				ChangeCombatPhase<ChooseTarget>(_chooseTargetScene);
			} 
			else
			{
				OnTargetSelected();
			}
		}

		public void OnTargetSelected()
		{
			Combat.Instance.ResolveTurn(_context.CurrentPlayer, _context.SelectedAction, _context.SelectedTarget);
		}

		private void BindHealthBar(HealthBar bar, CombatActor actor)
		{
			bar.Bind(actor);
		}
		
		public void NextPlayer(CombatPlayer player)
		{
			_context.CurrentPlayer = player;
			ChangeCombatPhase<ChooseAction>(_chooseActionScene);
		}
		
		private void RemoveActor(CombatActor actor)
		{
			actor.Visible = false;
		}

		private void BindCombatEvents()
		{
			Combat.ActorDying += RemoveActor;
		}

		public override void _ExitTree()
		{
			Combat.ActorDying -= RemoveActor;
		}

		public static CombatPosition? DirectionToPosition(InputEvent inputEvent, bool invertX = false, bool invertY = false)
		{
			if (inputEvent.IsActionPressed(Direction.RIGHT)) return invertX ? CombatPosition.Left : CombatPosition.Right;
			if (inputEvent.IsActionPressed(Direction.LEFT)) return invertX ? CombatPosition.Right : CombatPosition.Left;
			if (inputEvent.IsActionPressed(Direction.UP)) return invertY ? CombatPosition.Back : CombatPosition.Front;
			if (inputEvent.IsActionPressed(Direction.DOWN)) return invertY ? CombatPosition.Front : CombatPosition.Back;
			return null;
		}
	}
}
