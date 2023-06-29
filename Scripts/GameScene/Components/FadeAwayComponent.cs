using Godot;
using System;

namespace Game {
	public class FadeAwayComponent : Node2D {
		[Export] public NodePath parentPath;
		[Export] public NodePath fadeTimerPath;
		[Export] public float fadeStartTime;
		[Export] public float fadeSpeed = 0.01f;
		
		public enum State {
			None,
			Wait,
			FadeAway
		} private State state = State.None;
		
		private CanvasItem parent;
		private Timer fadeTimer;
		private float timeCounterBeforeFadeAway = 0;
		
		public override void _Ready () {
			parent = GetNode<CanvasItem>(parentPath);
			fadeTimer = GetNode<Timer>(fadeTimerPath);
			fadeTimer.Connect("timeout", this, "FadeAwayMethod");
		}
		public override void _Process (float delta) {
			switch (state) {
				case State.Wait :
					if (timeCounterBeforeFadeAway <= fadeStartTime) {
						timeCounterBeforeFadeAway += delta;
					} else {
						state = State.FadeAway;
					}
					break;
				case State.FadeAway :
					if(fadeTimer.IsStopped())
						fadeTimer.Start();
					break;
			}
		}
		public void Start () {
			state = State.Wait;
		}
		public bool IsRunning () {
			return state != State.None;
		}
		private void FadeAwayMethod () {
			Color tempColor = parent.Modulate;
			if(tempColor.a == 0) {
				fadeTimer.Stop();
				parent.QueueFree();
			} else {
				tempColor.a -= Mathf.Clamp(fadeSpeed, 0f, 1f);
				parent.Modulate = tempColor;
			}
		}
	}
}
