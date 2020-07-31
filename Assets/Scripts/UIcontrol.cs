using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIcontrol : MonoBehaviour {

private GameObject gameStartPanel;
public GameObject gameOvertPanel;
private GameObject gameController;

private GameObject gamebackground;

private GameObject home;
private GameObject background;

public GameObject button;

public GameObject gameStopPanel;
public GameObject gameDesPanel;

public GameObject startBtn;
public GameObject desBtn;
public GameObject desBackBtn;



	// Use this for initialization
	void Start () {
		home = GameObject.Find("home");
		gameStartPanel = transform.Find("GameStartpanel").gameObject;
		
		background = GameObject.Find("BackGround");
		gameController = GameObject.Find("GameController");
		button = transform.Find("Button").gameObject;
		gameStopPanel = transform.Find("GameStoppanel").gameObject;
		gameDesPanel = transform.Find("GameDespanel").gameObject;
		startBtn =  GameObject.Find("gamestart");
		desBtn =  GameObject.Find("gamedes");
		desBackBtn =  GameObject.Find("desBack");

		background.SetActive(false);
		gameController.SetActive(false);
		button.SetActive(false);
		gameStopPanel.SetActive(false);
		gameDesPanel.SetActive(false);
		gameStartPanel.SetActive(true);


	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void onGameStartButtonClick(){
		home.SetActive(false);
		gameStartPanel.SetActive(false);
		background.gameObject.SetActive(false);
		gameController.SetActive(true);
		button.SetActive(true);

	}

	// public void ReStart(){
	// 	gameOvertPanel.SetActive(false);
	// 	gameController.SetActive(false);
	// 	background.SetActive(false);
	// }

	public void Back(){
		SceneManager.LoadScene(0);


	}

	public void exit(){
		button.SetActive(false);
		home.SetActive(true);
		gameController.SetActive(false);
		gameStartPanel.SetActive(true);
		startBtn.SetActive(false);
		desBtn.SetActive(false);
		gameStopPanel.SetActive(true);

	}

	public void continueGame(){
		home.SetActive(false);
		gameStartPanel.SetActive(false);
		gameStopPanel.SetActive(false);
		gameController.SetActive(true);
		button.SetActive(true);
	}

	public void gameDes(){
		gameDesPanel.SetActive(true);
	}

	public void BackMenu(){
		SceneManager.LoadScene(0);
	}
	
	public void desBack(){
		SceneManager.LoadScene(0);
	}
	
}
