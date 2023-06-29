using Godot;
using System;

namespace Game {
	public class DeltaTimer : Node {
		[Export] public float waitTime = 1f;
		
		public bool IsOn {get; set;}
		private float currentTime = 0f;
		
		[Signal] delegate void timeout();
		
		public override void _Process(float delta) {
			if(IsOn) {
				currentTime += delta;
				if(currentTime >= waitTime) {
					EmitSignal(nameof(timeout));
					Restart();
				}
			}
		}
		public void Start () {
			IsOn = true;
		}
		public void Stop () {
			IsOn = false;
		}
		public void Restart () {
			currentTime = 0f;
		}
		public void RestartAndStop () {
			Restart();
			Stop();
		}
	}
}
