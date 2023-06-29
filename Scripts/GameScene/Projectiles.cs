using Godot;
using System;

namespace Game {
	public class Projectiles : Node2D {
		public Projectile FireProjectile (string projectileName, 
					Weapon weapon, Vector2 direction) {
			Projectile projectile = (Projectile) GameScene.LoadSceneNode(
				"Projectiles/"+projectileName);
			projectile.Position = weapon.GlobalPosition;
			projectile.nodeFiredFrom = weapon;
			AddChild(projectile);
			projectile.SetShootingInfo(direction, weapon);
			return projectile;
		}
	}
}
