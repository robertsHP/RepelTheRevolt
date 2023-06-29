using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game {
	public class Enemy : KinematicBody2D {
		[Export] public float health;
		[Export] public float speed;
		[Export] public uint moneyAfterKilling;
		[Export] public PackedScene partToDrop;
		
		public enum State {
			Moving,
			Attack
		} public State state = State.Moving;
		
		public Structure targetStructure;
		public List<Vector2> path;
		public Fire fire;
		
		public bool dead = false;

		/*************************************
			VIRTUAL METHODS
		**********************************/
		
		protected virtual void FindTargetBuilding () {
			targetStructure = GameScene.mainBuilding;
		}
		protected virtual void Die () {
			QueueFree();
		}
		
		/*************************************
			OTHER METHODS
		*************************************/
		
		protected void UpdatePath () {
			FindTargetBuilding();
			path = GameScene.pathFinding.GetNewPath(
				GlobalPosition, targetStructure.GlobalPosition);
		}
		protected void Move (float delta) {
			if(path.Count() != 0) {
				Vector2 movePos = path.First() - GlobalPosition;
				MoveAndSlide(movePos.Normalized() * speed);
			}
		}
		public void Damage (float damage, bool gibletLaunchRandomized, Vector2 gibletLaunchPoint) {
			for (int i = 0; i < damage; i++)
				GameScene.ground.SpawnGiblet(this, gibletLaunchRandomized, gibletLaunchPoint);
			health -= damage;
			
			if(health <= 0) {
				GameScene.mainBuilding.money += moneyAfterKilling;
				if(fire != null) {
					fire.QueueFree();
				}
				Die();
			}
		}
		public void SetOnFire () {
			fire = (Fire) GameScene.LoadSceneNode(
				"Other/Fire");
			fire.parent = this;
		}
	//	private void _on_Area2D_body_entered(Node2D node) {
	////		if(node.IsInGroup("Projectiles")) {
	////			Projectile proj = (Projectile) node;
	////			Damage(proj.damage, proj.launchComponent.direction);
	////		}
	//	}
	}
}
