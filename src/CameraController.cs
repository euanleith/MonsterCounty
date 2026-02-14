using Godot;
using MonsterCounty.Actor.World;

namespace MonsterCounty
{
	public partial class CameraController : Camera2D
	{
		public override void _Ready()
		{
			TileMapLayer tileMap = GetParent().GetNode<TileMapLayer>("TileMapLayer");
			if (tileMap == null)
			{
				GD.PushError($"{Name}: TileMapLayer 'TileMapLayer' not found under parent '{GetParent()?.Name}'.");
				return;
			}
			ClampToTileMap(tileMap);
		}
		
		private void ClampToTileMap(TileMapLayer tileMap)
		{
			Rect2I rect = tileMap.GetUsedRect();
			Vector2 tileSize = tileMap.TileSet.TileSize;
			Vector2 mapPos = tileMap.ToGlobal(rect.Position * tileSize);
			Vector2 mapSize = rect.Size * tileSize;
			LimitLeft = (int)mapPos.X;
			LimitTop = (int)mapPos.Y;
			LimitRight = (int)(mapPos.X + mapSize.X);
			LimitBottom = (int)(mapPos.Y + mapSize.Y);
		}


		private bool _initialised;
		public override void _Process(double delta)
		{
			if (!_initialised)
			{
				_initialised = true;
				return;
			}
			// todo add condition here or something idk (could just use initialised flag)
			GlobalPosition = WorldPlayer.Instance.Get().GlobalPosition;
		}
	}
}
