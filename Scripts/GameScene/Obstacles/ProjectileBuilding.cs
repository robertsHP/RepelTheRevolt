using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Game {
	public class ProjectileBuilding : Structure {
		[Export] public NodePath personelNodePath;
		
		public Node2D personelNode;
		public AIShooter [] aiShooters = new AIShooter [4];
		public List<Enemy> enemyTargets = new List<Enemy>();
		
		protected override void Ready2() {
			personelNode = GetNode<Node2D>(personelNodePath);
			Ready3();
		}
		protected override void Process2 (float delta) {
			if(!GameScene.paused) {
				foreach (AIShooter shooter in aiShooters) {
					if(shooter != null) {
						if(enemyTargets.Count != 0) {
							shooter.FireAt(enemyTargets.First());
						}
					}
				}
			}
			Process3(delta);
		}
		protected virtual void Ready3 () {}
		protected virtual void Process3 (float delta) {}
		
		public void AddAIShooter (AIShooter aiShooter) {
			for (int i = 0; i < aiShooters.Length; i++) {
				if(aiShooters[i] == null) {
					aiShooters[i] = aiShooter;
					return;
				}
			}
		}

		private void _on_ViewArea_body_entered(Node2D node) {
			if (node.IsInGroup("Enemies"))
				enemyTargets.Add((Enemy) node);
		}
		private void _on_ViewArea_body_exited(Node2D node) {
			if (node.IsInGroup("Enemies"))
				enemyTargets.Remove((Enemy) node);
		}
	}
}
