using Godot;
using System;

namespace Game {
	public class UI : Node2D {
		[Export] public NodePath topInterfacePath;
		[Export] public NodePath selectionNodePath;
		
		public TopInterface topInterface;
		public Node2D selectionNode;
		public SelectionBox selectionBox;
		
		public void Init () {
			topInterface = GetNode<TopInterface>(topInterfacePath);
			selectionNode = GetNode<Node2D>(selectionNodePath);
			selectionBox = (SelectionBox) selectionNode.GetNode("SelectionBox");
			
			topInterface.Init();
		}
		public void LoadMenu (string path) {
			if(GameScene.state != GameScene.State.Menu) {
				string fullPath = "UI/Menus/" + path;
				AddChild(GameScene.LoadSceneNode(fullPath));
				GameScene.state = GameScene.State.Menu;
			}
		}
		public void LoadObstacleMenu (string path, Obstacle obstacle) {
			if(GameScene.state != GameScene.State.Menu) {
				string fullPath = "UI/Menus/" + path;
				ObstacleMenu obstacleMenu = (ObstacleMenu) GameScene.LoadSceneNode(fullPath);
				obstacleMenu.SetObstacle(obstacle);
				AddChild(obstacleMenu);
				GameScene.state = GameScene.State.Menu;
			}
		}
		public void LoadGameOverMessageBox () {
			if(!GameScene.paused) {
				GameOverMessageBox gameOverMessageBox = 
					(GameOverMessageBox) GameScene.LoadSceneNode("UI/GameOverMessageBox");
				AddChild(gameOverMessageBox);
			}
		}
		public void LoadTemporaryMessageBox (string text) {
			Panel messageBox = (Panel) GameScene.LoadSceneNode("UI/MessageBox");
			Label label = (Label) messageBox.GetNode("MessageLabel");
			label.Text = text;
			AddChild(messageBox);
		}
	}
}
