using Godot;
using System;

namespace MainMenu {
	public class MainMenuScene : Control {
		private void _on_PlayButton_pressed () {
			GetTree().ChangeScene("res://Scenes/Level1Scene.tscn");
		}
		private void _on_QuitButton_pressed () {
			GetTree().Quit();
		}
	}
}
