using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Game {
	public class TowerBuilding : ProjectileBuilding {
		[Export] public NodePath aiShooterPath;
		
		protected override void Ready3 () {
			aiShooters[0] = GetNode<AIShooter>(aiShooterPath);
		}
		protected override void OnClick () {
			GameScene.ui.LoadObstacleMenu("TowerMenu", this);
		}
	}
}
