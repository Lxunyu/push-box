using UnityEngine;
using System.Collections;

public class MapBuilder : MonoBehaviour {
	public int rowNumber = 9, colNumber = 9;

	private int[] snapshotMap;
	private GameObject[] boxOjects;
	public GameObject wall;
	public GameObject player;
	public GameObject box;
	public GameObject targetPoint;

	private int targetCount = 0;

	public enum TileType {Null = 0,Wall = 1 ,Player = 2, Box = 3,TargetPoint = 9, targetWidthBox = 10, TargetWidthPlayer = 11};

	// Use this for initialization
	void Start () {
		InitMap ();
		BuildMap ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void InitMap(){
		boxOjects = new GameObject[rowNumber*colNumber];
		snapshotMap = new int[] {
			1, 1, 1, 1, 1, 0, 0, 0, 0,
			1, 2, 0, 0, 1, 0, 0, 0, 0,
			1, 0, 3, 0, 1, 0, 1, 1, 1,
			1, 0, 3, 0, 1, 0, 1, 9, 1,
			1, 1, 1, 3, 1, 1, 1, 9, 1,
			0, 1, 1, 0, 0, 0, 0, 9, 1,
			0, 1, 0, 0, 0, 1, 0, 0, 1,
			0, 1, 0, 0, 0, 1, 1, 1, 1,
			0, 1, 1, 1, 1, 1, 0, 0, 0,
		};
	}

	private void BuildMap(){
		for (int row = 0; row < rowNumber; row++) {
			for (int col = 0; col < colNumber; col++) {
				switch (snapshotMap [row * rowNumber + col]) {
				case (int)TileType.Null:
					break;
				case (int)TileType.Wall:
					BuildTile (row, col, wall);
					//BuildWall (row, col);
					break;
				case (int)TileType.Player:
					BuildTile (row, col, player);
					//BuildPlayer (row, col);
					break;
				case (int)TileType.TargetPoint:
					BuildTile (row, col, targetPoint);
					targetCount++;
					//BuildTargetPoint (row, col);
					break;
				case (int)TileType.Box:
					boxOjects[row*9+col] = BuildTile (row, col, box);
					break;
				}
			}
		}
	}

	private GameObject BuildTile(int row, int col, GameObject tile){
		 GameObject tileObj = Instantiate (tile) as GameObject;
		tileObj.transform.position = new Vector3 (col, -row, 0);
		tileObj.name = tile.name;
		return tileObj;
	}

	public GameObject GetBox(Vector2 pos,GameController.Direction direction){
		return boxOjects [-(int)pos.y * rowNumber + (int)pos.x + (int)direction];
	}

	public void SetBox(Vector2 pos,GameController.Direction direction, GameObject box){
		boxOjects [-(int)pos.y * rowNumber + (int)pos.x + (int)direction] = box;
	}


	public TileType GetTileType(Vector2 pos,GameController.Direction direction){
		return (TileType)snapshotMap [(int)-pos.y * rowNumber + (int)pos.x + (int)direction];
	}

	public void SetTileType(Vector2 pos,GameController.Direction direction,TileType tiltType){
		snapshotMap [-(int)pos.y * rowNumber + (int)pos.x + (int)direction] = (int)tiltType;
	}

	public void AddTargetCount(){
		targetCount++;
	}

	public void ReduceTagetCount(){
		if(targetCount > 0){
			targetCount--;
		}
	}

	public int GetTargetCount(){
		return targetCount;
	}
}
