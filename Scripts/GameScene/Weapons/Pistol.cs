using Godot;
using System;

namespace Game {
	public class Pistol : Weapon {
		public override void FireProjectile (Vector2 direction) {
			GameScene.projectiles.FireProjectile("PistolBullet", this, direction);
		}
		public override Projectile GetProjectileType () {
			Projectile projectile = (Projectile) GameScene.LoadSceneNode(
				"Projectiles/PistolBullet");
			projectile.Hide();
			return projectile;
		}
	}
}
