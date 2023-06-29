using Godot;
using System;

namespace Game {
	public class MineField : Obstacle {
		protected override void OnClick () {
			GameScene.ui.LoadObstacleMenu("MineFieldMenu", this);
		}
	}
}
