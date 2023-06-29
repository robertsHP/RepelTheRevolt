using Godot;
using System;

namespace Game {
	public class BuildMenu : Menu {
		[Export] public NodePath wallCostLabelPath;
		[Export] public NodePath towerCostLabelPath;
		[Export] public NodePath mineFieldCostLabelPath;
		
		public Label wallCostLabel;
		public Label towerCostLabel;
		public Label mineFieldCostLabel;
		
		public override void _Ready () {
			wallCostLabel = GetNode<Label>(wallCostLabelPath);
			towerCostLabel = GetNode<Label>(towerCostLabelPath);
			mineFieldCostLabel = GetNode<Label>(mineFieldCostLabelPath);
		}
		private void _on_WallPurchaseButton_pressed() {
			PurchaseObstacle(typeof(WallStructure), wallCostLabel);
		}
		private void _on_TowerPurchaseButton_pressed() {
			PurchaseObstacle(typeof(TowerBuilding), towerCostLabel);
		}
		private void _on_MineFieldPurchaseButton_pressed() {
			PurchaseObstacle(typeof(MineField), mineFieldCostLabel);
		}
		
		private void PurchaseObstacle (Type type, Label label) {
			string costStr = label.Text;
			int cost = int.Parse(costStr.Replace("$", ""));
			
			if(GameScene.mainBuilding.money >= cost) {
				GameScene.mainBuilding.money -= (uint) cost;
				SpawnStructureSprite(type);
			} else {
				GameScene.ui.LoadTemporaryMessageBox("You dont have enough money.");
			}
		}
		private void SpawnStructureSprite (Type obstacleType) {
			Sprite obstacleSprite = new Sprite ();
			obstacleSprite.Name = obstacleType.Name;
			var txtr = (Texture) Global.M.LoadTexture("Obstacles/"+obstacleType.Name+".png");
			obstacleSprite.Texture = txtr;
			obstacleSprite.Position = GameScene.ui.selectionBox.Position;
			GameScene.ui.selectionNode.AddChild(obstacleSprite);
			GameScene.ui.selectionNode.MoveChild(obstacleSprite, 0);
			GameScene.ui.selectionBox.tempBuildingSprite = obstacleSprite;
			GameScene.ui.selectionBox.tempBuildingType = obstacleType;
			GameScene.ui.selectionBox.Show();
			GameScene.state = GameScene.State.TilePlace;
			QueueFree();
		}
	}
}
