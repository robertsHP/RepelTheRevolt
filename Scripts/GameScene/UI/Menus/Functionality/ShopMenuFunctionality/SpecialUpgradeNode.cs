using Godot;
using System;

namespace Game {
	public class SpecialUpgradeNode : ShopPurchaseNode {
		public bool purchaseLimitReached = false;
		
		protected override void Process1 () {
			if(!purchaseLimitReached) {
				Button gunPurchaseButton = gunPayment.gunPurchaseNode.purchaseButton;
				bool result = !gunPurchaseButton.Disabled;
				purchaseButton.Disabled = result;
			}
		}
		protected void _on_PurchaseButton_pressed() {
			if(GameScene.mainBuilding.money >= cost) {
				GameScene.mainBuilding.money -= cost;
				gunPayment.shopMenu.updateMenuNodes = true;
				Weapon gun = gunPayment.gunPurchaseNode.purchasedGun;
				OnPurchaseButtonPressed(gun);
				purchaseLimitReached = true;
				purchaseButton.Disabled = true;
			} else {
				GameScene.ui.LoadTemporaryMessageBox("You don't have enough money.");
			}
		}
		protected virtual void OnPurchaseButtonPressed (Weapon gun) {}
	}
}
