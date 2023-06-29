using Godot;
using System;

namespace Game {
	public class GunPurchase : ShopPurchaseNode {
		[Export] public PackedScene weaponScene;
		public Weapon purchasedGun;
		public Projectile projectileType;
		
		protected override void Process1 () {
			UpdateButtonInfo();
		}
		public void UpdateButtonInfo () {
			if(GameScene.mainBuilding.IsWeaponLoaded(purchaseButton.Text)) {
				purchasedGun = GameScene.mainBuilding.GetWeapon(
					purchaseButton.Text);
				projectileType = purchasedGun.GetProjectileType();
				purchaseButton.Disabled = true;
			} else {
				purchaseButton.Disabled = false;
			}
		}
				
		//Buy the gun
		private void _on_PurchaseButton_pressed() {
			if(GameScene.mainBuilding.money >= cost) {
				GameScene.mainBuilding.money -= cost;
				GameScene.mainBuilding.LoadWeapon(weaponScene);
				UpdateButtonInfo();
			} else {
				GameScene.ui.LoadTemporaryMessageBox("You dont have enough money.");
			}
		}
		public string GetWeaponNameFromResourcePath () {
			int index = weaponScene.ResourcePath.LastIndexOf("/");
			return weaponScene.ResourcePath.Substring(index + 1).Split(".")[0];
		}
	}
}
