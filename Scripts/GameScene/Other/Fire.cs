using Godot;
using System;

namespace Game {
	public class Fire : AnimatedSprite {
		private static RandomNumberGenerator rng = new RandomNumberGenerator();
		
		[Export] public uint damage;
		[Export] public float lifeTimeMin;
		[Export] public float lifeTimeMax;
		[Export] public AudioStream audioStream;
		
		public Enemy parent;
		public float lifeTime;
		public float currentLifeTime = 0f;
		public AudioStreamPlayer2D audioStreamPlayer;
		
		public override void _Ready() {
			lifeTime = rng.RandfRange(lifeTimeMin, lifeTimeMax);
		}
		public override void _Process(float delta) {
			currentLifeTime += delta;
			GlobalPosition = parent.GlobalPosition;
			
			if(currentLifeTime >= lifeTime)
				QueueFree();
			
			if(audioStreamPlayer == null || !audioStreamPlayer.Playing)
				audioStreamPlayer = GameScene.sounds.PlayNewSound(audioStream, GlobalPosition);
			
			parent.Damage(damage, true, GlobalPosition);
		}
	}
}
