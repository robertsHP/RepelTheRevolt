using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Game {
	public class AIShooter : Node2D {
		[Export] public NodePath pistolPath;
		public Weapon pistol;
		
		public override void _Ready () {
			pistol = GetNode<Weapon>(pistolPath);
		}
		public override void _Process (float delta) {
			if (pistol.ammoInWeapon == 0)
				pistol.Reload();
		}
		public void FireAt (Enemy enemy) {
			pistol.Fire(this, enemy.GlobalPosition);
		}
	}
}
