using Godot;
using System;

namespace Game {
	public class AssaultRifle : Weapon {
		public override void FireProjectile (Vector2 direction) {
			GameScene.projectiles.FireProjectile("RifleBullet", this, direction);
		}
		public override Projectile GetProjectileType () {
			Projectile projectile = (Projectile) GameScene.LoadSceneNode(
				"Projectiles/RifleBullet");
			projectile.Hide();
			return projectile;
		}
	}
}
