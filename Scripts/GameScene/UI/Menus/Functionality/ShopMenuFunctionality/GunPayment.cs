using Godot;
using System;
using System.Collections.Generic;

namespace Game {
	public class GunPayment : Control {
		[Export] public NodePath shopMenuPath;
		[Export] public NodePath gunPurchaseNodePath;
		[Export] public NodePath ammoPurchaseNodePath;
		[Export] public NodePath [] upgradeNodePaths;
		
		public ShopMenu shopMenu;
		public GunPurchase gunPurchaseNode;
		public AmmoPurchase ammoPurchaseNode;
		public List<ShopPurchaseNode> upgradeNodes;
		
		public override void _Ready() {
			shopMenu = GetNode<ShopMenu>(shopMenuPath);
			upgradeNodes = new List<ShopPurchaseNode>();
			gunPurchaseNode = GetNode<GunPurchase>(gunPurchaseNodePath);
			ammoPurchaseNode = GetNode<AmmoPurchase>(ammoPurchaseNodePath);
			foreach (NodePath upgradeNodePath in upgradeNodePaths) {
				upgradeNodes.Add(GetNode<ShopPurchaseNode>(upgradeNodePath));
			}
		}
		public void Ready () {
			if(GameScene.mainBuilding != null) {
				gunPurchaseNode.Ready();
				ammoPurchaseNode.Ready();
				foreach (ShopPurchaseNode upgradeNode in upgradeNodes) {
					upgradeNode.Ready();
				}
			}
		}
		public void Process () {
			if(GameScene.mainBuilding != null) {
				gunPurchaseNode.Process();
				ammoPurchaseNode.Process();
				foreach (ShopPurchaseNode upgradeNode in upgradeNodes) {
					upgradeNode.Process();
				}
			}
		}
	}
}
