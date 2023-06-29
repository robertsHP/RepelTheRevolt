using Godot;
using System;

namespace Game {
	public class UpgradePurchaseNode : ShopPurchaseNode {
		public Label stateLabel;
		public bool purchaseLimitReached = false;
		
		private uint timesCanBePurchased = 6;
		private uint purchaseCount = 0;
		
		protected override void Ready1 () {
			stateLabel = (Label) GetNode("StateLabel");
		}
		
		protected override void Process1 () {
			Weapon gun = gunPayment.gunPurchaseNode.purchasedGun;
			if(gun != null) {
				Projectile projectile = gunPayment.gunPurchaseNode.projectileType;
				if(projectile != null) {
					UpdateStateLabel(gun, projectile);
				}
			} else {
				stateLabel.Text = "";
			}
			if(!purchaseLimitReached) {
				Button gunPurchaseButton = gunPayment.gunPurchaseNode.purchaseButton;
				bool result = !gunPurchaseButton.Disabled;
				purchaseButton.Disabled = result;
			}
		}
		protected virtual void UpdateStateLabel (Weapon gun, Projectile projectile) {}
		
		protected void _on_PurchaseButton_pressed() {
			if(GameScene.mainBuilding.money >= cost) {
				GameScene.mainBuilding.money -= cost;
				gunPayment.shopMenu.updateMenuNodes = true;
				if(purchaseCount <= timesCanBePurchased) {
					Weapon gun = gunPayment.gunPurchaseNode.purchasedGun;
					OnPurchaseButtonPressed(gun);
					purchaseCount++;
				} else {
					purchaseLimitReached = true;
					purchaseButton.Disabled = true;
				}
			} else {
				GameScene.ui.LoadTemporaryMessageBox("You dont have enough money.");
			}
		}
		protected virtual void OnPurchaseButtonPressed (Weapon gun) {}
	}
}
