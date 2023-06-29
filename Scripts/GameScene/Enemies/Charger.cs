using Godot;
using System;

namespace Game {
	public class Charger : Enemy {
		[Export] public uint meleeDamage;
		public Timer punchTimer;
		
		public override void _Process(float delta) {
			if(!GameScene.paused) {
				switch (state) {
					case State.Moving :
						UpdatePath();
						Move(delta);
						break;
					case State.Attack :
						StartPunchTimer(1f);
						break;
				}
			}
		}
		protected override void FindTargetBuilding () {
			targetStructure = GameScene.mainBuilding;
		}
		protected override void Die () {
			dead = true;
			QueueFree();
		}
		private void StartPunchTimer (float secondsWait) {
			if(punchTimer == null) {
				punchTimer = new Timer();
				AddChild(punchTimer);
				punchTimer.WaitTime = secondsWait;
				punchTimer.OneShot = false;
				punchTimer.Connect("timeout", this, "PunchTargetedBuilding");
				punchTimer.Start();
			}
		}
		private void PunchTargetedBuilding () {
			if(!GameScene.paused) {
				if(targetStructure != null) {
					targetStructure.Damage(meleeDamage);
				}
			}
		}
	}
}
