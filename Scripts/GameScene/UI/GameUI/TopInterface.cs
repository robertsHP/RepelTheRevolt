using Godot;
using System;

namespace Game {
	public class TopInterface : Panel {
		[Export] public NodePath healthBarPath;
		[Export] public NodePath fireBarPath;
		[Export] public NodePath reloadBarPath;
		[Export] public NodePath moneyInfoPath;
		[Export] public NodePath ammoInfoPath;
		[Export] public NodePath gunButtonsPath;
		
		public ProgressBar healthBar;
		public ProgressBar fireBar;
		public ProgressBar reloadBar;
		public Label moneyInfo;
		public Label ammoInfo;
		public GunButtons gunButtons;
		
		public void Init () {
			healthBar = GetNode<ProgressBar>(healthBarPath);
			fireBar = GetNode<ProgressBar>(fireBarPath);
			reloadBar = GetNode<ProgressBar>(reloadBarPath);
			moneyInfo = GetNode<Label>(moneyInfoPath);
			ammoInfo = GetNode<Label>(ammoInfoPath);
			gunButtons = GetNode<GunButtons>(gunButtonsPath);
			
			gunButtons.Init();
		}
		public override void _Process(float delta) {
			gunButtons.Process(delta);
			UpdatePlayerInfo();
		}
		private void UpdatePlayerInfo () {
			healthBar.Value = GameScene.mainBuilding.health;
			if(GameScene.mainBuilding.weapon != null) {
				fireBar.Value = GameScene.mainBuilding.weapon.fireVal;
				reloadBar.Value = GameScene.mainBuilding.weapon.reloadVal;
				ammoInfo.Text = GetAmmoInfo();	
			}
			moneyInfo.Text = "$"+GameScene.mainBuilding.money;
		}
		private string GetAmmoInfo () {
			Weapon weapon = GameScene.mainBuilding.weapon;
			
			uint ammoLimit = weapon.ammoLimit;
			string ammoLimitStr = (weapon.infiniteAmmo) ? "infinite" : weapon.inventoryAmmo.ToString();
			return weapon.ammoInWeapon+"/"+ammoLimitStr;
		}
	}
}
