using Godot;
using System;

namespace Game {
	public class Obstacle : Area2D {
		public Vector2 posOnTileMap;
	
		public bool canBeClickedOn = false;
		public bool hoveredOver = false;
		
		public override void _Ready () {
			Ready1();
		}
		public override void _Process(float delta) {
			if(canBeClickedOn) {
				if(hoveredOver) {
					if(!Input.IsKeyPressed((int) KeyList.Control)) {
						if(Input.IsMouseButtonPressed((int) ButtonList.Left)) {
							OnClick();
						}
					}
				}
			}
			Process1(delta);
		}
		protected virtual void Ready1 () {}
		protected virtual void Process1 (float delta) {}
		protected virtual void OnClick () {}
		
		private void _on_Obstacle_mouse_entered() {
			hoveredOver = true;
		}
		private void _on_Obstacle_mouse_exited() {
			hoveredOver = false;
		}
	}
}
