using Godot;
using System;

namespace Global {
	public static class V {
		public const String textureFolderPath = "res://Resources/";
		public const String scriptsFolderPath = "res://Scripts/";
		public const String sceneObjFolderPath = "res://SceneNodes/";
		
		public static Texture cursorDefault = M.LoadTexture("Resources/CursorDefault.png");
		public static Texture cursorSight = M.LoadTexture("Resources/CursorSight.png");
	}
	public static class M {
		public static Resource Load (string path) {
			return GD.Load(path);
		}
		public static Texture LoadTexture (string path) {
			return (Texture) GD.Load(Global.V.textureFolderPath+path);
		}
		public static Script LoadScript (string path) {
			return (Script) GD.Load(Global.V.scriptsFolderPath+path);
		}
		public static Node LoadSceneNode (string path) {
			var packed = (PackedScene) GD.Load(Global.V.sceneObjFolderPath+path+".tscn");
			return (Node) packed.Instance();
		}	
		public static DataType getAttrFromEnum<EnumType, DataType> (this EnumType enumVal) 
		where DataType : System.Attribute {
			var type = enumVal.GetType();
			var memInfo = type.GetMember(enumVal.ToString());
			var attributes = memInfo [0].GetCustomAttributes(typeof(DataType), false);
			return (attributes.Length > 0) ? (DataType) attributes [0] : null;
		}
		public static EnumType getEnumByString<EnumType> (string name) {
			return (EnumType) Enum.Parse(typeof(EnumType), name);
		}
	}
}
