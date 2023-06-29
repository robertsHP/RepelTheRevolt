using Godot;
using System;

namespace Game {
	public class ShopPurchaseNode : Control {
		[Export] public NodePath gunPaymentPath;
		public GunPayment gunPayment;
		
		public Label costLabel;
		public Button purchaseButton;
		public uint cost;
		
		public void Ready () {
			costLabel = (Label) GetNode("CostLabel");
			purchaseButton = (Button) GetNode("PurchaseButton");
			
			string costTxt = costLabel.Text.Replace("$", string.Empty);
			cost = uint.Parse(costTxt);
			
			gunPayment = GetNode<GunPayment>(gunPaymentPath);
			Ready1();
		}
		public void Process () {
			Process1();
		}
		
		protected virtual void Ready1 () {}
		protected virtual void Process1 () {}
	}
}
