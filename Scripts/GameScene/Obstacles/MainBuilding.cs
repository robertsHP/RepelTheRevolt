using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Game {
	public class MainBuilding : ProjectileBuilding {
		public Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
		public Weapon weapon;
		
		public uint money = 0;
		public bool fireHeld = false;
		
		private Node2D weaponsNode;
		
		protected override void Ready3() {
			weaponsNode = (Node2D) GetNode("Weapons");
			LoadWeapons();
		}
		protected override void Process3(float delta) {
			if(fireHeld) {
				weapon.Fire(this, GetGlobalMousePosition());
			}
		}
		public override void _Input(InputEvent @event) {
			if(weapon != null) {
				if(GameScene.state == GameScene.State.Default) {
					if(Input.IsKeyPressed((int) KeyList.Control)) {
						fireHeld = Input.IsMouseButtonPressed((int) ButtonList.Left);
					}
					if(Input.IsKeyPressed((int) KeyList.R)) {
						weapon.Reload();
					}
				}
			}
		}
		public void LoadWeapons () {
			Godot.Collections.Array weaponsTemp = weaponsNode.GetChildren();
			foreach (Weapon weapon in weaponsTemp)
				weapons.Add(weapon.Name.Replace(" ", ""), weapon);
			if(weapons.Count != 0)
				weapon = (Weapon) weapons.First().Value;
		}
		public void LoadWeapon (PackedScene weaponScene) {
			Weapon weapon = (Weapon) weaponScene.Instance();
			weapons.Add(weapon.Name.Replace(" ", ""), weapon);
			weaponsNode.AddChild(weapon);
		}
		public Weapon GetWeapon (string name) {
			name = name.Replace(" ", "");
			return weapons[name];
		}
		public bool IsWeaponLoaded (string name) {
			name = name.Replace(" ", "");
			return weapons.ContainsKey(name);
		}
		protected override void OnClick () {
			GameScene.ui.LoadMenu("ShopMenu");
		}
	}
}
