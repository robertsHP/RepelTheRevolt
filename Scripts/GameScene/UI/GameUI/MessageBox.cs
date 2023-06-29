using Godot;
using System;

namespace Game {
	public class MessageBox : Panel {
		[Export] public NodePath fadeAwayComponentPath;
		
		private FadeAwayComponent fadeAwayComponent;
		
		public override void _Ready() {
			fadeAwayComponent = GetNode<FadeAwayComponent>(fadeAwayComponentPath);
			fadeAwayComponent.Start();
		}
	}
}
