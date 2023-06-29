using Godot;
using System;

namespace Game {
	public class AmmoPurchase : ShopPurchaseNode {
		protected override void Ready1 () {}
		protected override void Process1 () {
			string name = gunPayment.gunPurchaseNode.GetWeaponNameFromResourcePath();
			if(GameScene.mainBuilding.weapons.ContainsKey(name)) {
				Weapon weapon = GameScene.mainBuilding.weapons[name];
				Button gunPurchaseButton = gunPayment.gunPurchaseNode.purchaseButton;
				bool result = !gunPurchaseButton.Disabled || weapon.infiniteAmmo;
				purchaseButton.Disabled = result;
			} else {
				purchaseButton.Disabled = true;
			}
		}
		private void _on_PurchaseButton_pressed() {
			if(GameScene.mainBuilding.money >= cost) {
				GameScene.mainBuilding.money -= cost;
				Weapon gun = gunPayment.gunPurchaseNode.purchasedGun;
				gun.inventoryAmmo++;
			} else {
				GameScene.ui.LoadTemporaryMessageBox("You dont have enough money.");
			}
		}
	}
}
