using Godot;
using System;

namespace Game {
	public class ExplosivePurchase : SpecialUpgradeNode {
		protected override void OnPurchaseButtonPressed (Weapon gun) {
			gun.explosiveRounds = true;
		}
	}
}
