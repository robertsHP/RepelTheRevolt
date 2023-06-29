using Godot;
using System;

namespace Game {
	public class WallMenu : ObstacleMenu {
		[Export] public NodePath repairFunctionPath;
		public RepairFunction repairFunction;
		
		protected override void Ready1 () {
			repairFunction = GetNode<RepairFunction>(repairFunctionPath);
			repairFunction.Ready((Structure) obstacle);
		}
		protected override void Process1 (float delta) {
			if(obstacle == null) {
				QueueFreeAndSetStateToDefault();
			} else {
				repairFunction.Process();
			}
		}
		private void _on_DemolishButton_pressed() {
			obstacle.QueueFree();
			QueueFreeAndSetStateToDefault();
		}
	}
}
