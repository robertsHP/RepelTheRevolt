using Godot;
using System;

namespace Game {
	public class Ground : Node2D {
		public void SpawnExplosion (Vector2 spawnPosition, Vector2 size) {
			Explosion explosion = (Explosion) GameScene.LoadSceneNode("Other/Explosion");
			explosion.GlobalPosition = spawnPosition;
			explosion.Scale += size;
			CallDeferred("add_child", explosion);
		}
		public void SpawnNapalmExplosion (Vector2 spawnPosition, Vector2 size) {
			FireExplosion explosion = (FireExplosion) GameScene.LoadSceneNode("Other/FireExplosion");
			explosion.GlobalPosition = spawnPosition;
			explosion.Scale += size;
			CallDeferred("add_child", explosion);
		}
		public void SpawnGiblet (Enemy enemy, bool randomized, Vector2 launchPoint) {
			Giblet giblet = (Giblet) enemy.partToDrop.Instance();
			giblet.Position = enemy.GlobalPosition;
			AddChild(giblet);
			if(randomized) {
				giblet.SetRandomizedLaunchDirection(launchPoint);
			} else {
				giblet.SetSpecificLaunchDirection(launchPoint);
			}
		}
	}
}
