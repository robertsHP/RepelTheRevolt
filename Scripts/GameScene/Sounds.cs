using Godot;
using System;

namespace Game {
	public class Sounds : Node2D {
		public AudioStreamPlayer2D PlayNewSound (AudioStream stream, Vector2 position) {
			AudioStreamPlayer2D soundPlayer = new AudioStreamPlayer2D ();
			CallDeferred("add_child", soundPlayer);
			soundPlayer.Connect(
				"finished", 
				this, 
				"_on_AudioStreamPlayer2D_finished", 
				new Godot.Collections.Array (soundPlayer));
			soundPlayer.Stream = stream;
			soundPlayer.GlobalPosition = position;
			soundPlayer.Play();
			return soundPlayer;
		}
		private void _on_AudioStreamPlayer2D_finished(Godot.Collections.Array parameters) {
			if(parameters.Count != 0) {
				AudioStreamPlayer2D soundPlayer = (AudioStreamPlayer2D) parameters[0];
				soundPlayer.QueueFree();
			}
		}
	}
}
