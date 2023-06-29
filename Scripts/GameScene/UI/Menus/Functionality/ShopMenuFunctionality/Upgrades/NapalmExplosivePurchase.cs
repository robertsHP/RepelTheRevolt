using Godot;
using System;

namespace Game {
	public class NapalmExplosivePurchase : SpecialUpgradeNode {
		protected override void OnPurchaseButtonPressed (Weapon gun) {
			gun.explosiveFireRounds = true;
		}
	}
}
