using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Game {
	public class Shooter : Enemy {
		[Export] public NodePath weaponPath;
		[Export] public float timeBeforeShootingNext;
		
		public Timer shootTimer;
		public List<Structure> structuresInVision = new List<Structure>();
		public Weapon weapon;
		
		public override void _Ready() {
			weapon = GetNode<Weapon>(weaponPath);
		}
		public override void _Process(float delta) {
			if(!GameScene.paused) {
				if (weapon != null)
					if (weapon.ammoInWeapon == 0)
						weapon.Reload();
				switch (state) {
					case State.Moving :
						UpdatePath();
						Move(delta);
						break;
					case State.Attack :
						StartShootTimer(timeBeforeShootingNext);
						break;
				}
			}
		}
		protected override void FindTargetBuilding () {
			if(structuresInVision.Any()) {
				targetStructure = structuresInVision.First();
				state = State.Attack;
			} else {
				targetStructure = GameScene.mainBuilding;
				state = State.Moving;
			}
		}
		protected override void Die () {
			dead = true;
			QueueFree();
		}
		private void StartShootTimer (float secondsWait) {
			if(shootTimer == null) {
				shootTimer = new Timer();
				AddChild(shootTimer);
				shootTimer.WaitTime = secondsWait;
				shootTimer.OneShot = false;
				shootTimer.Connect("timeout", this, "ShootTargetedBuilding");
				shootTimer.Start();
			}
		}
		private void ShootTargetedBuilding () {
			if(!GameScene.paused) {
				if(targetStructure != null) {
					if(weapon != null) {
						weapon.Fire(this, targetStructure.GlobalPosition);
					}
				}
			}
		}
		private void _on_ViewArea_area_entered(Node2D node) {
			if (node.IsInGroup("Structures"))
				structuresInVision.Add((Structure) node);
		}
		private void _on_ViewArea_area_exited(Node2D node) {
			if (node.IsInGroup("Structures"))
				structuresInVision.Remove((Structure) node);
		}
	}
}
