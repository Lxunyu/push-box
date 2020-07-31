using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {
	public enum Direction{Null = 0,Up = -9,Down = 9,Left = -1,Right = 1}
	private Direction dir;

	public MapBuilder map;

	public Transform playerTransform;

	private Vector3 playerPos = Vector3.zero;

	private Vector3 nextPlayerPos = Vector3.zero;

	private bool canMovePlayer = false;

	private bool canMoveBox = false;

	public Text tileTypeText;

	public Canvas canvas;

	private Text[,] mapText;

	public Sprite normalBoxImage;

	public Sprite targetBoxImage;

	public Image winImage;

	public GameObject restart;
	

	public GameObject gameOvertPanel;
	// public GameObject button;

	// Use this for initialization
	void Start () {
		gameOvertPanel = transform.Find("GameOverpanel").gameObject;
		restart = transform.Find("GameOverpanel").gameObject;
		gameOvertPanel.SetActive(false);
		restart.SetActive(false);
		// button = transform.Find("Button").gameObject;
		// button.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		DetectPlayer ();
		DetectInput ();
		Judge ();
		MovePlayer ();
		MoveBox ();
		ShowMap ();
		GameOver();
	
	}

	private void DetectPlayer(){
		if (playerTransform == null) {
			playerTransform = GameObject.Find ("Player").GetComponent<Transform> ();
		}
		playerPos = playerTransform.position;
	}

	private void DetectInput(){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			dir = Direction.Up;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			dir = Direction.Down;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			dir = Direction.Left;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			dir = Direction.Right;
		} else {
			dir = Direction.Null;
		}
	}

	private void MovePlayer(){
		switch (dir) {
		case Direction.Up:
			nextPlayerPos = new Vector3 (0, 1);
		//	Debug.Log ("Up = " + map.GetTileType (playerTransform.position,dir));
			break;
		case Direction.Down:
			nextPlayerPos = new Vector3 (0, -1);
		//	Debug.Log ("Down = " + map.GetTileType (playerTransform.position,dir));
			break;
		case Direction.Left:
			nextPlayerPos = new Vector3 (-1, 0);
			//Debug.Log ("Left = " + map.GetTileType (playerTransform.position,dir));
			break;
		case Direction.Right:
			nextPlayerPos = new Vector3 (1,0);
		//	Debug.Log ("Right = " + map.GetTileType (playerTransform.position,dir));
			break;
		case Direction.Null:
			nextPlayerPos = new Vector3 (0, 0);
		//	Debug.Log ("Player = " + map.GetTileType (playerTransform.position,dir));
			break;
		}
		if(canMovePlayer){
			playerTransform.position += nextPlayerPos;
		}
	}

	private void MoveBox(){
		if (canMoveBox == true) {
			GameObject currentBox = map.GetBox (playerPos, dir);
			map.SetBox (playerPos,dir, null);
			map.SetBox (playerPos,(Direction)((int)dir * 2), currentBox);
			currentBox.transform.position = playerPos + nextPlayerPos * 2;
			// if (map.GetTileType (currentBox.transform.position, (int)Direction.Null) == MapBuilder.TileType.targetWidthBox) {
			// 	currentBox.GetComponent<SpriteRenderer>().sprite = targetBoxImage;
			// } else {
			// 	currentBox.GetComponent<SpriteRenderer>().sprite = normalBoxImage;
			// }
		}
	}

	private void Judge(){
		switch (map.GetTileType (playerPos, dir)) {
			case MapBuilder.TileType.Null:
				map.SetTileType (playerPos, dir, MapBuilder.TileType.Player);
					if (map.GetTileType (playerPos, Direction.Null) == MapBuilder.TileType.Player) {
						map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.Null);
					} else {
						map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.TargetPoint);
					}
				canMovePlayer = true;
			break;
			case MapBuilder.TileType.TargetPoint:
				map.SetTileType(playerPos,dir,MapBuilder.TileType.TargetWidthPlayer);
				if (map.GetTileType (playerPos, Direction.Null) == MapBuilder.TileType.Player) {
					map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.Null);
				} else {
					map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.TargetPoint);
				}
				canMovePlayer = true;
			break;
			case MapBuilder.TileType.Box:
				switch(map.GetTileType(playerPos,(Direction)((int)dir*2))){
					case MapBuilder.TileType.Null:
					map.SetTileType (playerPos, dir, MapBuilder.TileType.Player);
					map.SetTileType (playerPos, (Direction)((int)dir * 2), MapBuilder.TileType.Box);
					if (map.GetTileType (playerPos, Direction.Null) == MapBuilder.TileType.Player) {
						map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.Null);
					} else {
						map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.TargetPoint );
					}
					canMovePlayer = true;
					canMoveBox = true;
					break;
					case MapBuilder.TileType.TargetPoint:
					map.SetTileType (playerPos, dir, MapBuilder.TileType.Player);
					map.SetTileType (playerPos, (Direction)((int)dir * 2), MapBuilder.TileType.targetWidthBox);
					if (map.GetTileType (playerPos, Direction.Null) == MapBuilder.TileType.Player) {
						map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.Null);
					} else {
						map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.TargetPoint );
					}
					canMovePlayer = true;
					canMoveBox = true;
					map.ReduceTagetCount();
					break;
				}
			break;
			case MapBuilder.TileType.targetWidthBox:
				switch (map.GetTileType (playerPos, (Direction)((int)dir * 2))) {
				case MapBuilder.TileType.Null:
				map.SetTileType(playerPos,dir,MapBuilder.TileType.TargetWidthPlayer);
				map.SetTileType(playerPos,(Direction)((int)dir*2),MapBuilder.TileType.Box);
				if (map.GetTileType (playerPos, Direction.Null) == MapBuilder.TileType.Player) {
					map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.Null);
				} else {
					map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.TargetPoint);
				}
				canMovePlayer = true;
				canMoveBox = true;
				map.AddTargetCount();
					break;
				case MapBuilder.TileType.TargetPoint:
				map.SetTileType(playerPos,dir,MapBuilder.TileType.TargetWidthPlayer);
				map.SetTileType(playerPos,(Direction)((int)dir*2),MapBuilder.TileType.targetWidthBox);
				if (map.GetTileType (playerPos, Direction.Null) == MapBuilder.TileType.Player) {
					map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.Null);
				} else {
					map.SetTileType (playerPos, Direction.Null, MapBuilder.TileType.TargetPoint);
				}
				canMovePlayer = true;
				canMoveBox = true;
				break;
				}
			break;
			default:
				canMovePlayer = false;
				canMoveBox = false;
			break;
		}
	}

	private void ShowMap(){
		if (mapText == null) {
			mapText = new Text[9,9];
		}
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (mapText [i, j] == null) {
					mapText[i,j] = Instantiate(tileTypeText);
					mapText[i,j].transform.parent = canvas.transform;
					mapText[i,j].transform.position = new Vector3 (j * 15, -i * 15, 0);
				}
				mapText[i,j].text = ((int)map.GetTileType (new Vector2 (j, -i), Direction.Null)).ToString ();
			}
		}
	}

	private void GameOver(){
		if(map.GetTargetCount() == 0){
			//winImage.gameObject.SetActive(true);
			gameOvertPanel.SetActive(true);
		}
	}
	public void ReStart(){
		SceneManager.LoadScene(0);

	}
}
