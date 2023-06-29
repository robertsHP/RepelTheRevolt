using Godot;
using System;

namespace Game {
	public class SelectionBox : Sprite {
		public Vector2 selectedTileMapPos = Vector2.Zero;
		
		public Sprite tempBuildingSprite;
		public Type tempBuildingType;
		
		public override void _Ready() {
			Hide();
		}
		
		public override void _Input(InputEvent @event) {
			if(GameScene.state == GameScene.State.TilePlace) {
				if(!Visible)
					Show();
				if (@event is InputEventMouseMotion eventMouseMotion) {
					selectedTileMapPos = GameScene.tileMap.WorldToMap(
						GameScene.tileMap.GetLocalMousePosition());
					if(IfNotObstacle())
						Position = MapToWorldProper(selectedTileMapPos);
				}
				if(tempBuildingSprite != null) {
					if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) {
						if((ButtonList) mouseEvent.ButtonIndex == ButtonList.Left) {
							SpawnObstacle();
							tempBuildingSprite.QueueFree();
							tempBuildingSprite = null;
							tempBuildingType = null;
							GameScene.state = GameScene.State.Default;
							Hide();
						}
					}
				}
			}
		}
		public bool IfNotObstacle () {
			var obstacles = GetTree().GetNodesInGroup("Obstacles");
			
			foreach (Node2D obst in obstacles) {
				if (obst.IsInGroup("Structures")) {
					Structure structure = (Structure) obst;
					if(selectedTileMapPos == structure.posOnTileMap) {
						return false;
					}
				}
			}
			return true;
		}
		public Vector2 MapToWorldProper (Vector2 selectedTileMapPos) {
			return GameScene.tileMap.MapToWorld(selectedTileMapPos) + GameScene.tileMap.CellSize / 2;
		}
		
		private void SpawnObstacle () {
			Obstacle obstacle = (Obstacle) GameScene.LoadSceneNode("Obstacles/"+tempBuildingType.Name);
			obstacle.Position = Position;
			obstacle.posOnTileMap = selectedTileMapPos;
			obstacle.canBeClickedOn = true;
			GameScene.ui.selectionNode.RemoveChild(tempBuildingSprite);
			GameScene.buildings.AddChild(obstacle);
		}

		public override void _Process(float delta) {
			if(GameScene.state == GameScene.State.TilePlace) {
				if(tempBuildingSprite != null) {
					tempBuildingSprite.Position = Position;
				}
			}
		}
	}
}
