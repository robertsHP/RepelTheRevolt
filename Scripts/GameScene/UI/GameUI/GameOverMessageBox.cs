using Godot;
using System;

namespace Game {
	public class GameOverMessageBox : Panel {
		public override void _Ready () {
			GameScene.paused = true;
		}
		private void _on_QuitButton_pressed() {
			GetTree().ChangeScene("res://Scenes/MainMenuScene.tscn");
		}
		private void _on_TryAgainButton_pressed() {
			GetTree().ChangeScene("res://Scenes/Level1Scene.tscn");
		}
	}
}
