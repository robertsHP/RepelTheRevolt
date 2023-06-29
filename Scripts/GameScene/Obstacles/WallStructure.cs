using Godot;
using System;

namespace Game {
	public class WallStructure : Structure {
		protected override void OnClick () {
			GameScene.ui.LoadObstacleMenu("WallMenu", this);
		}
	}
}
