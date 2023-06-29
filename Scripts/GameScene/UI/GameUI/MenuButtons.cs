using Godot;
using System;

namespace Game {
	public class MenuButtons : Control {
		public override void _Process(float delta) {
			MenuInput();
		}
		private void _on_BuildButton_pressed () {
			GameScene.ui.LoadMenu("BuildMenu");
		}
		private void _on_ShopButton_pressed () {
			GameScene.ui.LoadMenu("ShopMenu");
		}
		private void MenuInput () {
			if(Input.IsKeyPressed((int) KeyList.S)) {
				GameScene.ui.LoadMenu("ShopMenu");
			} else if (Input.IsKeyPressed((int) KeyList.B)) {
				GameScene.ui.LoadMenu("BuildMenu");
			} else if (Input.IsKeyPressed((int) KeyList.M)) {
				GameScene.ui.LoadMenu("OptionsMenu");
			}
		}
		private void _on_MenuButton_pressed () {
			GameScene.paused = true;
			GameScene.ui.LoadMenu("OptionsMenu");
		}
	}
}
