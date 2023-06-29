using Godot;
using System;

namespace Game {
	public class Projectile : Node2D {
		public Node2D nodeFiredFrom;
		
		[Export] public float damage;
		[Export] public float speed;
		[Export] public bool explosive = false;
		[Export] public bool napalmExplosive = false;
		[Export] public NodePath launchComponentPath;
		
		public bool affectEnemies = false;
		public bool affectStructures = false;
		
		public LaunchComponent launchComponent;
		
		//!!!!!SET BEFORE USING
		public void SetShootingInfo (Vector2 direction, Weapon weapon) {
			launchComponent.SetLaunchDirection(direction, weapon.accuracy);
			damage += weapon.projectileDamageBoost;
			affectEnemies = weapon.affectEnemies;
			affectStructures = weapon.affectStructures;
		}
		
		public override void _Ready () {
			launchComponent = GetNode<LaunchComponent>(launchComponentPath);
			launchComponent.Start();
		}
		public override void _PhysicsProcess (float delta) {
			launchComponent.Move(speed, delta);
		}
		protected void OnHit (Node2D node) {
			if(node.IsInGroup("Enemies")) {
				if(affectEnemies) {
					Enemy enemy = (Enemy) node;
					enemy.Damage(damage, false, launchComponent.direction);
					Destroy();
				}
			} else if (node.IsInGroup("Structures")) {
				if(affectStructures) {
					Structure structure = (Structure) node;
					structure.Damage(damage);
					Destroy();
				}
			}
		}
		
		protected void Destroy () {
			if(explosive) {
				GameScene.ground.SpawnExplosion(GlobalPosition, new Vector2(4, 4));
			} else if (napalmExplosive) {
				GameScene.ground.SpawnNapalmExplosion(GlobalPosition, new Vector2(4, 4));
			}
			QueueFree();
		}
		private void _on_VisibilityNotifier2D_screen_exited() {
			Destroy();
		}
		private void _on_Area2D_body_entered(Node2D node) {
			OnHit(node);
		}
	}
}
