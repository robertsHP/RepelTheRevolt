using Godot;
using System.Collections.Generic;
using System;

namespace Game {
	public class GameScene : Node2D {
		[Export] public NodePath tileMapPath;
		[Export] public NodePath soundsNodePath;
		[Export] public NodePath groundNodePath;
		[Export] public NodePath projectilesNodePath;
		[Export] public NodePath buildingsNodePath;
		[Export] public NodePath enemiesNodePath;
		[Export] public NodePath UINodePath;
		[Export] public NodePath mainBuildingPath;
		[Export] public NodePath pathFindingPath;
		
		public static TileMap tileMap;
		public static Sounds sounds;
		public static Ground ground;
		public static Projectiles projectiles;
		public static Node2D buildings;
		public static Node2D enemies;
		public static UI ui;
		public static MainBuilding mainBuilding;
		public static PathFinding pathFinding;

		public enum State {
			Default,
			TilePlace,
			Menu
		} public static State state = State.Default;
		
		public static bool paused = false;
		public static bool intermission = false;
		
		public override void _Ready() {
			tileMap = GetNode<TileMap>(tileMapPath);
			sounds = GetNode<Sounds>(soundsNodePath);
			ground = GetNode<Ground>(groundNodePath);
			projectiles = GetNode<Projectiles>(projectilesNodePath);
			buildings = GetNode<Node2D>(buildingsNodePath);
			enemies = GetNode<Node2D>(enemiesNodePath);
			ui = GetNode<UI>(UINodePath);
			mainBuilding = GetNode<MainBuilding>(mainBuildingPath);
			pathFinding = GetNode<PathFinding>(pathFindingPath);
			
			ui.Init();
			pathFinding.CreateNavigationMap();
		}
		public static Node LoadSceneNode (string path) {
			return (Node) Global.M.LoadSceneNode("GameScene/"+path);
		}
	}
}
