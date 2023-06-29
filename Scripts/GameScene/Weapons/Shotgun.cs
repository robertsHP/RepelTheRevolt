using Godot;
using System;

namespace Game {
	public class Shotgun : Weapon {
		public override void FireProjectile (Vector2 direction) {
			for (uint i = 0; i < 10; i++) {
				GameScene.projectiles.FireProjectile("Shell", this, direction);
			}
		}
		public override Projectile GetProjectileType () {
			Projectile projectile = (Projectile) GameScene.LoadSceneNode(
				"Projectiles/Shell");
			projectile.Hide();
			return projectile;
		}
	}
}
