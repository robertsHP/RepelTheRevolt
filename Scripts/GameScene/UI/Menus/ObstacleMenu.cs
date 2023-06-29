using Godot;
using System;

namespace Game {
	public class ObstacleMenu : Menu {
		public Obstacle obstacle;
		
		public void SetObstacle (Obstacle obstacle) {
			this.obstacle = obstacle;
		}
	}
}
