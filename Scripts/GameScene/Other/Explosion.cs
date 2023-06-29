using Godot;
using System;

namespace Game {
	public class Explosion : Area2D {
		[Export] public uint damage;
		[Export] public NodePath explosionAnimatedSpritePath;
		[Export] public AudioStream audioStream;
		[Export] public NodePath aftermathSpritePath;
		[Export] public NodePath fadeAwayComponentPath;
		
		private AnimatedSprite explosionAnimatedSprite;
		private Sprite aftermathSprite;
		protected FadeAwayComponent fadeAwayComponent;

		public override void _Ready() {
			explosionAnimatedSprite = GetNode<AnimatedSprite>(explosionAnimatedSpritePath);
			aftermathSprite = GetNode<Sprite>(aftermathSpritePath);
			fadeAwayComponent = GetNode<FadeAwayComponent>(fadeAwayComponentPath);
			
			GameScene.sounds.PlayNewSound(audioStream, GlobalPosition);
			
			explosionAnimatedSprite.Playing = true;
		}
		public override void _Process(float delta) {
			if(!fadeAwayComponent.IsRunning()) {
				if (explosionAnimatedSprite.Frame == 4)
					aftermathSprite.Visible = true;
				else if (explosionAnimatedSprite.Frame == 9) {
					explosionAnimatedSprite.QueueFree();
					fadeAwayComponent.Start();
				}
			}
		}
		private void _on_Explosion_body_entered(Node2D node) {
			if(!fadeAwayComponent.IsRunning()) {
				if(node.IsInGroup("Enemies")) {
					Enemy enemy = (Enemy) node;
					enemy.Damage(damage, true, GlobalPosition);
				}	
			}
		}
		private void _on_Explosion_area_entered(Node2D node) {
			if(!fadeAwayComponent.IsRunning()) {
				if(node.IsInGroup("Structures")) {
					Structure structure = (Structure) node;
					structure.Damage(damage);
				}
			}
		}
	}
}
