using Godot;
using System;

namespace Game {
	public class AdditionalShooters : Panel {
		[Export] public NodePath purchaseButton1Path;
		[Export] public NodePath purchaseButton2Path;
		[Export] public NodePath purchaseButton3Path;
		[Export] public NodePath purchaseButton4Path;
		
		public Button purchaseButton1;
		public Button purchaseButton2;
		public Button purchaseButton3;
		public Button purchaseButton4;
		
		public override void _Ready () {
			purchaseButton1 = GetNode<Button>(purchaseButton1Path);
			purchaseButton2 = GetNode<Button>(purchaseButton2Path);
			purchaseButton3 = GetNode<Button>(purchaseButton3Path);
			purchaseButton4 = GetNode<Button>(purchaseButton4Path);
		}
		
		private void _on_Shooter1_pressed() {
			PurchaseShooter(purchaseButton1);
		}
		private void _on_Shooter2_pressed() {
			PurchaseShooter(purchaseButton2);
		}
		private void _on_Shooter3_pressed() {
			PurchaseShooter(purchaseButton3);
		}
		private void _on_Shooter4_pressed() {
			PurchaseShooter(purchaseButton4);
		}
		private void PurchaseShooter (Button button) {
			string costStr = button.Text;
			int cost = int.Parse(costStr.Replace("$", ""));
			
			if(GameScene.mainBuilding.money >= cost) {
				GameScene.mainBuilding.money -= (uint) cost;
				AIShooter aiShooter = (AIShooter) GameScene.LoadSceneNode("Other/AIShooter");
				aiShooter.GlobalPosition = GameScene.mainBuilding.GlobalPosition;
				GameScene.mainBuilding.AddAIShooter(aiShooter);
				button.Disabled = true;
			} else {
				GameScene.ui.LoadTemporaryMessageBox("You dont have enough money.");
			}
		}
	}
}
