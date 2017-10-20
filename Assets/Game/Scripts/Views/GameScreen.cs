﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : View {

	[SerializeField] int gameTime = 120;
	[SerializeField] Text timeLeftText;
	[SerializeField] Button tile1;
	[SerializeField] Button tile2;
	[SerializeField] Button tile3;
	[SerializeField] Button tile4;
	[SerializeField] Button tile5;
	[SerializeField] Button tile6;
	[SerializeField] Button tile7;
	[SerializeField] Button tile8;
	[SerializeField] Button tile9;

	private TileManager tileManager;
	private int[] tiles;
	private Button[] tileObjects;
	private float timeLeft;
	private int currentLevel;
	private bool paused;
	private bool gameOver;

	// Use this for initialization
	void Start () {
		tileManager = new TileManager ();
		currentLevel = 5;
		requestTiles ();
		InstantiateTiles ();

		timeLeft = gameTime;
		timeLeftText.text = timeLeft.ToString();
		paused = false;

		EventBroadcaster.Instance.AddObserver (EventNames.ON_UNPAUSE, this.unpause);

	}
	
	// Update is called once per frame
	void Update () {
		if (!paused && !gameOver) {
			updateTime ();
		}
	}

	private void updateTime() {
		timeLeft -= Time.deltaTime;
		timeLeftText.text = timeLeft.ToString();
	}

	private void requestTiles() {
		int tileNum = 0;

		if (currentLevel == 1) {
			tileNum = 8;
		}
		else if (currentLevel == 2) {
			tileNum = 12;
		}
		else if (currentLevel == 3) {
			tileNum = 16;
		}
		else if (currentLevel == 4) {
			tileNum = 20;
		}
		else if (currentLevel >= 5) {
			tileNum = 24;
		}

		tiles = tileManager.RequestTiles (tileNum);
	}

	private void InstantiateTiles() {
		tileObjects = new Button[tiles.Length];

		int x = -300;
		int y = 30;

		for (int i = 0; i < tiles.Length; i++) {
			Debug.Log (tiles [i]);
			Button tile = null;
			if (tiles[i] == TileManager.TILE_TYPE_1) {
				tile = Instantiate (tile1);
			}
			else if(tiles[i] == TileManager.TILE_TYPE_2) {
				tile = Instantiate (tile2);
			}
			else if (tiles[i] == TileManager.TILE_TYPE_3) {
				tile = Instantiate (tile3);
			}
			else if (tiles[i] == TileManager.TILE_TYPE_4) {
				tile = Instantiate (tile4);
			}
			else if (tiles[i] == TileManager.TILE_TYPE_5) {
				tile = Instantiate (tile5);
			}
			else if (tiles[i] == TileManager.TILE_TYPE_6) {
				tile = Instantiate (tile6);
			}
			else if (tiles[i] == TileManager.TILE_TYPE_7) {
				tile = Instantiate (tile7);
			}
			else if (tiles[i] == TileManager.TILE_TYPE_8) {
				tile = Instantiate (tile8);
			}
			else if (tiles[i] == TileManager.TILE_TYPE_9) {
				tile = Instantiate (tile9);
			}

			tile.transform.SetParent (this.transform);
			tile.transform.localScale = new Vector3 (0.7f, 0.7f, 1);
			tile.transform.localPosition = new Vector3 (x, y);

			tileObjects [i] = tile;

			x += 70;

			if ((i + 1) % 8 == 0) {
				x = -300;
				y -= 70;
			}

		}
	}

	public void pause() {
		paused = true;
		ViewHandler.Instance.Show (ViewNames.PAUSE_SCREEN_NAME);
	}

	public void unpause() {
		paused = false;
	}
}