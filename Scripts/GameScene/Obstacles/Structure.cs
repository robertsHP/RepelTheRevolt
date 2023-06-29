using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Game {
	public class Structure : Obstacle {
		[Export] public NodePath collisionShapePath;
		[Export] public NodePath healthBarPath;
		
		public CollisionShape2D collisionShape;
		public ProgressBar healthBar;
		
		public int maxHealth = 100;
		public int health = 100;
		public List<Enemy> enemiesAttached = new List<Enemy>();

		protected override void Ready1 () {
			collisionShape = GetNode<CollisionShape2D>(collisionShapePath);
			healthBar = GetNode<ProgressBar>(healthBarPath);
			Ready2();
		}
		protected override void Process1(float delta) {
			Process2(delta);
		}
		
		protected virtual void Ready2 () {}
		protected virtual void Process2 (float delta) {}
		
		public void Damage (float damage) {
			if(health > 0) {
				health -= (int) damage;
				if(healthBar != null)
					healthBar.Value = health;
			}
			if(health <= 0) {
				if(this.GetType() == typeof(MainBuilding)) {
					GameScene.ui.LoadGameOverMessageBox();
				} else {
					QueueFree();
				}
			}
		}
		public void AttachEnemy (Enemy enemy) {
			enemy.state = Enemy.State.Attack;
			enemy.targetStructure = this;
			enemiesAttached.Add(enemy);
		}
		private void _on_Structure_body_entered(Node2D node) {
			if (node.IsInGroup("Enemies"))
				AttachEnemy((Enemy) node);
		}
	}
}
