using Godot;
using System;

namespace Game {
	public class AccuracyPurchase : UpgradePurchaseNode {
		protected override void UpdateStateLabel (Weapon gun, Projectile projectile) {
			stateLabel.Text = gun.accuracy.ToString();
		}
		protected override void OnPurchaseButtonPressed (Weapon gun) {
			gun.accuracy -= 0.5f;
		}
	}
}
