using Godot;
using System;

namespace Game {
	public class DamagePurchase : UpgradePurchaseNode {
		protected override void UpdateStateLabel (Weapon gun, Projectile projectile) {
			string val = (projectile.damage+gun.projectileDamageBoost).ToString();
			stateLabel.Text = val;
		}
		protected override void OnPurchaseButtonPressed (Weapon gun) {
			gun.projectileDamageBoost += 1;
		}
	}
}
