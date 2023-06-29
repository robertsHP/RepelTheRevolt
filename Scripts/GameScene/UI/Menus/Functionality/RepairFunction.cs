using Godot;
using System;

namespace Game {
	public class RepairFunction : Control {
		public Label costLabel;
		public Button purchaseButton;
		public ProgressBar healthBar;
		
		public Structure structure;
		
		public void Ready (Structure structure) {
			this.structure = structure;
			costLabel = (Label) GetNode("CostLabel");
			purchaseButton = (Button) GetNode("PurchaseButton");
			healthBar = (ProgressBar) GetNode("HealthBar");
		}
		public void Process () {
			if(structure != null) {
				healthBar.Value = structure.health;
				UpdateCost();
			}
		}
		private void UpdateCost () {
			int cost = structure.maxHealth - structure.health;
			if(GameScene.mainBuilding.money <= cost) {
				cost = (int) GameScene.mainBuilding.money;
			}
			costLabel.Text = cost+"$";
		}
		private uint GetCost () {
			string cost = costLabel.Text;
			return uint.Parse(cost.Replace("$", ""));
		}
		private void _on_PurchaseButton_pressed() {
			uint cost = GetCost();
			
			if(cost == 0) {
				return;
			}
			if(GameScene.mainBuilding.money < cost) {
				GameScene.ui.LoadTemporaryMessageBox("You dont have enough money.");
				return;
			}
			GameScene.mainBuilding.money -= cost;
			structure.health += (int) cost;
		}
	}
}
