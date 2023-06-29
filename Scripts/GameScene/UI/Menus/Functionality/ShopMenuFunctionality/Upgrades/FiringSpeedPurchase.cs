using Godot;
using System;

namespace Game {
	public class FiringSpeedPurchase : UpgradePurchaseNode {
		protected override void UpdateStateLabel (Weapon gun, Projectile projectile) {
			float fireTime = gun.fireTimer.waitTime;
			stateLabel.Text = (fireTime+gun.fireTimeBoost).ToString();
		}
		protected override void OnPurchaseButtonPressed (Weapon gun) {
			gun.fireTimeBoost += -0.5f;
		}
	}
}
