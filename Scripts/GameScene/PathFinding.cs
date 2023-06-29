using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game {
	public class PathFinding : Node2D {
		[Export] public bool displayPaths = false;
		[Export] public Color pointColor;
		
		private AStar2D aStar = new AStar2D ();
		private Vector2 halfCellSize;
		private Rect2 usedRect;
		
		private bool drawn = false;
		private bool canTraverse = true;
		
		public override void _Draw () {
			if(displayPaths) {
				if(!drawn) {
					foreach (int pointID in aStar.GetPoints()) {
						Vector2 point = aStar.GetPointPosition(pointID);
						Vector2 pointWorld = GameScene.tileMap.MapToWorld(point) + halfCellSize;
						DrawCircle(pointWorld, 1f, pointColor);
	//					if(displayConnections) {
	//						int id = GetIDForPoint(MainScene.tileMap.WorldToMap(point));
	//						foreach (int connectedPointID in aStar.GetPointConnections(id)) {
	//							Vector2 connPoint = aStar.GetPointPosition(connectedPointID);
	//							Vector2 connPointWorld = MainScene.tileMap.MapToWorld(connPoint) + halfCellSize;
	//							DrawLine(
	//								pointWorld, connPointWorld, connectionColor);
	//						}
	//					}
					}
					drawn = true;
				}
			}
		}
		public override void _PhysicsProcess (float delta) {
			UpdateNavigationMap();
		}
		public void CreateNavigationMap () {
			halfCellSize = GameScene.tileMap.CellSize / 2;
			usedRect = GameScene.tileMap.GetUsedRect();
			var tiles = GameScene.tileMap.GetUsedCells();
			AddTraversableTiles(tiles);
			ConnectTraversableTiles(tiles);
		}
		public void AddTraversableTiles (Godot.Collections.Array tiles) {
			foreach (Vector2 tilePos in tiles) {
				int id = GetIDForPoint(tilePos);
				aStar.AddPoint(id, tilePos);
			}
		}
		public void ConnectTraversableTiles (Godot.Collections.Array tiles) {
			foreach (Vector2 tilePos in tiles) {
				int id = GetIDForPoint(tilePos);
				
				for (int x = 0; x < 3; x++) {
					for (int y = 0; y < 3; y++) {
						var target = tilePos + new Vector2(x - 1, y - 1);
						int targetID = GetIDForPoint(target);
						
						if (tilePos == target || !aStar.HasPoint(targetID))
							continue;
						aStar.ConnectPoints(id, targetID, true);
					}
				}
			}
		}

		public void UpdateNavigationMap () {
			foreach (int point in aStar.GetPoints()) {
				aStar.SetPointDisabled(point, false);
			}
			
			if(canTraverse) {
				var structures = GetTree().GetNodesInGroup("Obstacles");
				foreach (var structure in structures) {
					if (structure.GetType().IsSubclassOf(typeof(Structure))) {
						if(structure.GetType() == typeof(MainBuilding))
							continue;
						Structure currStructure = (Structure) structure;
						var tileCoord = GameScene.tileMap.WorldToMap(
							currStructure.collisionShape.GlobalPosition);
						var id = GetIDForPoint(tileCoord);
						if (aStar.HasPoint(id)) {
							aStar.SetPointDisabled(id, true);
						}
					}
				}
			}
		}
		public List<Vector2> GetNewPath (Vector2 start, Vector2 end) {
			Vector2 startTile = GameScene.tileMap.WorldToMap(start);
			Vector2 endTile = GameScene.tileMap.WorldToMap(end);
			
			int startID = GetIDForPoint(startTile);
			int endID = GetIDForPoint(endTile);
			
			if (!aStar.HasPoint(startID) || !aStar.HasPoint(endID)) {
				return new List<Vector2>();
			}
			Vector2 [] pathMap = aStar.GetPointPath(startID, endID);
			List<Vector2> pathWorld = new List<Vector2>();
			foreach (Vector2 point in pathMap) {
				int id = GetIDForPoint(point);
				if(!aStar.IsPointDisabled(id)) {
					Vector2 pointWorld = GameScene.tileMap.MapToWorld(point) + halfCellSize;
					pathWorld.Add(pointWorld);
				}
			}
			if(pathWorld.Count != 0) {
				canTraverse = true;
				pathWorld.Remove(pathWorld.First());
			} else {
				canTraverse = false;
			}
			return pathWorld;
		}
		public int GetIDForPoint (Vector2 point) {
			var x = point.x - usedRect.Position.x;
			var y = point.y - usedRect.Position.y;
			return (int) (x + y * usedRect.Size.x);
		}
	}
}
