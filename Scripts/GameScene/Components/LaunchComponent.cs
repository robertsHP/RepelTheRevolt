using Godot;
using System;

namespace Game {
	public class LaunchComponent : Node2D {
		private static RandomNumberGenerator rng = new RandomNumberGenerator ();
		
		[Export] public NodePath parentPath;
		
		private Node2D parent;
		
		public bool move = false;
		public Vector2 direction = Vector2.Zero;
		
		//!!!!!SET BEFORE USING
		public void SetLaunchDirection (Vector2 direction, float accuracyOffset = 0) {
			direction.x -= rng.RandfRange(-accuracyOffset, accuracyOffset);
			direction.y -= rng.RandfRange(-accuracyOffset, accuracyOffset);
			this.direction = direction;
			parent.LookAt(direction);
		}
		
		public override void _Ready () {
			parent = GetNode<Node2D>(parentPath);
		}
		public void Move (float speed, float delta) {
			if(move) {
				float rotation = parent.Rotation;
				parent.GlobalPosition += new Vector2(speed, 0)
					.Rotated(rotation) * delta;
			}
		}
		public float MoveWhileSlowingDown (float speed, float delta, float slowDown) {
			if(move) {
				if(speed <= 0)
					Stop();
				float rotation = parent.Rotation;
				parent.GlobalPosition += new Vector2(speed, 0).Rotated(rotation) * delta;
				speed -= slowDown;
			}
			return speed;
		}
		public void Stop () {
			move = false;
		}
		public void Start () {
			move = true;
		}
		public bool IsRunning () {
			return move;
		}
	}
}
