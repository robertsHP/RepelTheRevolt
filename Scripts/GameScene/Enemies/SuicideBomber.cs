using Godot;
using System;

namespace Game {
	public class SuicideBomber : Enemy {
		public override void _Process(float delta) {
			if(!GameScene.paused) {
				switch (state) {
					case State.Moving :
						UpdatePath();
						Move(delta);
						break;
					case State.Attack :
						Die();
						break;
				}
			}
		}
		protected override void FindTargetBuilding () {
			targetStructure = GameScene.mainBuilding;
		}
		protected override void Die () {
			if(!dead) {
				GameScene.ground.SpawnExplosion(GlobalPosition, new Vector2(2, 2));
				QueueFree();
				dead = true;
			}
		}
	}
}
