using Godot;
using System;

namespace Game {
	public class Mine : Area2D {
		private void _on_Mine_body_entered(object body) {
			GameScene.ground.SpawnExplosion(GlobalPosition, new Vector2(2, 2));
			QueueFree();
		}
	}
}
