using Godot;
using System;

namespace Game {
	public class ReloadSpeedPurchase : UpgradePurchaseNode {
		protected override void UpdateStateLabel (Weapon gun, Projectile projectile) {
			float reloadTime = gun.reloadTimer.waitTime;
			stateLabel.Text = (reloadTime+gun.reloadTimeBoost).ToString();
		}
		protected override void OnPurchaseButtonPressed (Weapon gun) {
			gun.reloadTimeBoost += -0.5f;
		}
	}
}
