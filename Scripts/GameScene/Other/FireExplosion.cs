using Godot;
using System;

namespace Game {
	public class FireExplosion : Area2D {
		[Export] public uint damage;
		[Export] public NodePath explosionAnimatedSpritePath;
		[Export] public AudioStream audioStream;
		
		private AnimatedSprite explosionAnimatedSprite;
		
		public override void _Ready() {
			explosionAnimatedSprite = GetNode<AnimatedSprite>(explosionAnimatedSpritePath);
			
			GameScene.sounds.PlayNewSound(audioStream, GlobalPosition);
			explosionAnimatedSprite.Playing = true;
		}
		public override void _Process(float delta) {
			if (explosionAnimatedSprite.Frame == 9) {
				QueueFree();
			}
		}
		private void _on_FireExplosion_body_entered(Node2D node) {
			if(node.IsInGroup("Enemies")) {
				Enemy enemy = (Enemy) node;
				enemy.Damage(damage, true, GlobalPosition);
				enemy.SetOnFire();
			}
		}
		private void _on_FireExplosion_area_entered(Node2D node) {
			if(node.IsInGroup("Structures")) {
				Structure structure = (Structure) node;
				structure.Damage(damage);
			}
		}
	}
}
