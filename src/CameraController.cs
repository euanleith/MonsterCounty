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

		public override void _Process(double delta)
		{
			GlobalPosition = WorldPlayer.Instance.Get().GlobalPosition;
		}
	}
}
