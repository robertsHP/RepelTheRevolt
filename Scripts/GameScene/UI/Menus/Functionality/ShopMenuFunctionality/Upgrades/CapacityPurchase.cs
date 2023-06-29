using Godot;
using System;

namespace Game {
	public class CapacityPurchase : UpgradePurchaseNode {
		protected override void UpdateStateLabel (Weapon gun, Projectile projectile) {
			stateLabel.Text = gun.ammoLimit.ToString();
		}
		protected override void OnPurchaseButtonPressed (Weapon gun) {
			gun.ammoLimit += 1;
		}
	}
}
