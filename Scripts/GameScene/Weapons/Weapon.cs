using Godot;
using System;

namespace Game {
	public class Weapon : Node2D {
		[Export] public NodePath fireTimerPath;
		[Export] public NodePath reloadTimerPath;
		[Export] public bool infiniteAmmo = false;
		[Export] public bool affectEnemies = false;
		[Export] public bool affectStructures = false;

		[Export] public uint ammoInWeapon = 0;
		[Export] public uint inventoryAmmo = 0;
		[Export] public uint ammoLimit = 0;
		[Export] public float accuracy;
		[Export] public float fireSecondsMax;
		[Export] public float reloadSecondsMax;
		
		[Export] public AudioStream audioStream;
		
		public uint fireVal = 0;
		public uint reloadVal = 100;
		
		public float projectileDamageBoost = 0f;
		public float fireTimeBoost = 0f;
		public float reloadTimeBoost = 0f;
		public bool explosiveRounds = false;
		public bool explosiveFireRounds = false;
		
		public DeltaTimer fireTimer;
		public DeltaTimer reloadTimer;
		
		public override void _Ready () {
			fireTimer = GetNode<DeltaTimer>(fireTimerPath);
			reloadTimer = GetNode<DeltaTimer>(reloadTimerPath);
			LoadTimer(fireTimer, fireSecondsMax, "FireTimerMethod");
			LoadTimer(reloadTimer, reloadSecondsMax, "ReloadTimerMethod");
		}
		public override void _Process (float delta) {
			fireTimer.waitTime = fireSecondsMax + fireTimeBoost;
			reloadTimer.waitTime = reloadSecondsMax + reloadTimeBoost;
		}
		protected void LoadTimer (DeltaTimer timer, float waitTime, string method) {
			timer.waitTime = waitTime;
			timer.Connect("timeout", this, method);
			timer.Stop();
		}
		protected void StartFireTimer () {
			fireVal = 100;
			fireTimer.Start();
		}
		private void FireTimerMethod () {
			if(fireVal <= 0) {
				fireVal = 0;
				fireTimer.RestartAndStop();
			} else {
				fireVal -= 5;
			}
		}
		protected void StartReloadTimer () {
			reloadVal = 0;
			reloadTimer.Start();
		}
		private void ReloadTimerMethod () {
			if(reloadVal >= 100) {
				if(!infiniteAmmo) {
					uint ammoLeftover = ammoLimit - ammoInWeapon;
					ammoInWeapon += ammoLeftover;
					inventoryAmmo -= ammoLeftover;
				} else {
					ammoInWeapon = ammoLimit;
				}
				reloadVal = 100;
				reloadTimer.RestartAndStop();
			} else {
				reloadVal += 5;
			}
		}
		
		public void Fire (Node2D user, Vector2 direction) {
			if(ammoInWeapon != 0 && !fireTimer.IsOn && !reloadTimer.IsOn) {
				ammoInWeapon -= 1;
				if(audioStream != null)
					GameScene.sounds.PlayNewSound(audioStream, GlobalPosition);
				FireProjectile(direction);
				StartFireTimer();
			}
		}
		public void Reload () {
			if((inventoryAmmo != 0 || infiniteAmmo) && !reloadTimer.IsOn) {
				StartReloadTimer();
			}
		}
		public virtual void FireProjectile (Vector2 direction) {}
		public virtual Projectile GetProjectileType () { return null;}
	}
}
