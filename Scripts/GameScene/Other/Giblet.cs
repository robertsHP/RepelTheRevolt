using Godot;
using System;

namespace Game {
	public class Giblet : Sprite {
		private static RandomNumberGenerator rng = new RandomNumberGenerator();
		
		[Export] public float launchSpeedMin;
		[Export] public float launchSpeedMax;
		[Export] public NodePath launchComponentPath;
		[Export] public NodePath fadeAwayComponentPath;
		[Export] public Texture [] textures;
		
		private float launchSpeed;
		private LaunchComponent launchComponent;
		private FadeAwayComponent fadeAwayComponent;

		//!!!!!SET BEFORE USING
		public void SetSpecificLaunchDirection (Vector2 direction) {
			launchComponent.SetLaunchDirection(direction);
		}
		public void SetRandomizedLaunchDirection (Vector2 mainPoint) {
			Vector2 direction = mainPoint;
			direction.x -= rng.RandiRange(-400, 400);
			direction.y -= rng.RandiRange(-400, 400);
			launchComponent.SetLaunchDirection(direction);
		}
		
		public override void _Ready() {
			launchComponent = GetNode<LaunchComponent>(launchComponentPath);
			fadeAwayComponent = GetNode<FadeAwayComponent>(fadeAwayComponentPath);
			
			if(textures.Length != 0) {
				int index = rng.RandiRange(0, textures.Length - 1);
				Texture = textures[index];
			}
			launchSpeed = rng.RandfRange(launchSpeedMin, launchSpeedMax);
			launchComponent.Start();
		}
		public override void _Process(float delta) {
			if(launchComponent.IsRunning()) {
				fadeAwayComponent.Start();
			}
		}
		public override void _PhysicsProcess (float delta) {
			launchSpeed = launchComponent.MoveWhileSlowingDown(launchSpeed, delta, 10);
		}
	}
}
