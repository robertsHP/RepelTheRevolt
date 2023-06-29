using Godot;
using System;

namespace Game {
	public class MineFieldMenu : ObstacleMenu {
		private void _on_DemolishButton_pressed() {
			obstacle.QueueFree();
			QueueFreeAndSetStateToDefault();
		}
	}
}
