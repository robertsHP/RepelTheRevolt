using Godot;
using System;
using System.Collections.Generic;

namespace Game {
	public class ShopMenu : Menu {
		[Export] public NodePath repairFunctionPath;
		
		public RepairFunction repairFunction;
		public List<GunPayment> gunPaymentNodes;
		public bool updateMenuNodes = true;
		
		protected override void Ready1 () {
			repairFunction = GetNode<RepairFunction>(repairFunctionPath);
			repairFunction.Ready(GameScene.mainBuilding);
			
			Godot.Collections.Array tempPaymentNodes = GetNode(
				"CoverOverPanel/Guns").GetChildren();
			gunPaymentNodes = new List<GunPayment>();
			foreach (GunPayment paymentNode in tempPaymentNodes) {
				paymentNode.Ready();
				gunPaymentNodes.Add(paymentNode);
			}
		}
		protected override void Process1 (float delta) {
			repairFunction.Process();
			if(updateMenuNodes) {
				foreach (GunPayment gunPayment in gunPaymentNodes)
					gunPayment.Process();
				updateMenuNodes = false;
			}
		}
	}
}
