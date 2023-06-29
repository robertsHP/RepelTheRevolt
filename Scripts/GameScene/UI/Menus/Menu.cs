using Godot;
using System;

namespace Game {
	public class Menu : Panel {
		public override void _Ready () {
			Ready1();
		}
		public override void _Process(float delta) {
			BackKeyPress();
			Process1(delta);
		}
		protected virtual void Ready1 () {}
		protected virtual void Process1 (float delta) {}
		protected void BackKeyPress () {
			if(Input.IsKeyPressed((int) KeyList.Escape)) {
				QueueFreeAndSetStateToDefault();
			}
		}
		protected void _on_BackButton_pressed() {
			QueueFreeAndSetStateToDefault();
		}
		public void QueueFreeAndSetStateToDefault () {
			GameScene.state = GameScene.State.Default;
			QueueFree();
		}
	}
}
