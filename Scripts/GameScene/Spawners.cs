using Godot;
using System;
using System.Collections.Generic;

namespace Game {
	public class Spawners : Node2D {
		[Export] public int startingSpawnRange;
		[Export] public int spawnRangeIncrement;
		[Export] public int maximumSpawnRange;
		[Export] public float startingWaitTime;
		[Export] public float waitTimeDecrement;
		[Export] public PackedScene [] enemies;
		
		public int spawnRange;
		public int [] rangeNums;
		
		private Godot.Collections.Array spawners;
		private RandomNumberGenerator rng;
		public Timer spawnDelayTimer;
		
		public override void _Ready() {
			spawners = GetChildren();
			rng = new RandomNumberGenerator ();
			StartDelayTimer();
			
			int tempSpawnRange = maximumSpawnRange;
			
			spawnRange = startingSpawnRange;
			rangeNums = new int [enemies.Length];
			for (int i = 0; i < enemies.Length; i++) {
				rangeNums[i] = tempSpawnRange;
				tempSpawnRange /= 2;
			}
		}
		public override void _Process (float delta) {
			if(GameScene.intermission && !spawnDelayTimer.IsStopped()) {
				spawnDelayTimer.Stop();
			} else if(!GameScene.intermission && spawnDelayTimer.IsStopped()) {
				spawnRange += spawnRangeIncrement;
				if(spawnDelayTimer.WaitTime > 0f) {
					spawnDelayTimer.WaitTime -= waitTimeDecrement;
				}
				spawnDelayTimer.Start();
			}
		}
		public void StartDelayTimer () {
			spawnDelayTimer = new Timer();
			AddChild(spawnDelayTimer);
			spawnDelayTimer.WaitTime = startingWaitTime;
			spawnDelayTimer.OneShot = false;
			spawnDelayTimer.Connect("timeout", this, "SpawnMethod");
			spawnDelayTimer.Start();
		}
		public void SpawnMethod () {
			int randNum = rng.RandiRange(0, spawners.Count - 1);
			Sprite spawner = (Sprite) spawners[randNum];
			SpawnEnemy(spawner);
		}
		public void SpawnEnemy (Sprite spawner) {
			int rangeNum = rng.RandiRange(0, spawnRange);
			int i = 0;
			int currentRangeCount = 0, prevRangeCount = 0;
			while (i < rangeNums.Length) {
				currentRangeCount += rangeNums[i];
				if(prevRangeCount <= rangeNum && rangeNum < currentRangeCount) {
					Enemy enemy = (Enemy) enemies[i].Instance();
					enemy.Position = spawner.GlobalPosition;
					GameScene.enemies.AddChild(enemy);
				}
				prevRangeCount = currentRangeCount;
				i++;
			}
		}
	}
}
