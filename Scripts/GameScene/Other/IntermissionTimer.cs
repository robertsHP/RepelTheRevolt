using Godot;
using System;

namespace Game {
	public class IntermissionTimer : Node2D {
		[Export] public float intermissionTime = 0f;
		[Export] public float actionTime = 0f;
		[Export] public NodePath timeLabelPath;
		[Export] public bool startWithIntermission = false;
		
		private float time = 0;
		public Label timeLabel;
		
		public override void _Ready () {
			timeLabel = GetNode<Label>(timeLabelPath);
			GameScene.intermission = startWithIntermission;
			if(GameScene.intermission) {
				Show();
			} else {
				Hide();
			}
		}
		public override void _Process (float delta) {
			if(GameScene.intermission) {
				if(time >= intermissionTime) {
					time = 0;
					GameScene.intermission = false;
					Hide();
				}
			} else {
				if(time >= actionTime) {
					time = 0;
					GameScene.intermission = true;
					Show();
				}
			}
			time += delta;
			timeLabel.Text = ((int)time).ToString();
		}
	}
}
