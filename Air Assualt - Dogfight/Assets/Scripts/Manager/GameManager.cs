using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using AirAssault;

namespace AirAssault
{
	[Serializable]
	public class GameManager : MonoBehaviour
	{
		static public GameManager s_Instance;

		public GameObject player;
		public PlayerManager playerManager;

		public float startDelay = 3f;
		public float endDelay = 3f;

		public Text messageText;
		public Text scoreText;
		public Text healthText;
		public Text timeText;

		public GameObject playerPrefab;
		public GameObject enemyPrefab;

		public bool gameStarted = false;
		public bool gameFinished = false;

		private int roundNumber;
		private WaitForSeconds startWait;
		private WaitForSeconds endWait;

		public SpawnPoint[] enemySpawnPoints;
		public Transform playerSpawnPoint;

		public int elapsedTimeMinute = 0;
		public int elapsedTimeSecond = 0;

		void Awake ()
		{
			s_Instance = this;
		}

		void Start ()
		{
			startWait = new WaitForSeconds (startDelay);
			endWait = new WaitForSeconds (endDelay);

			StartCoroutine (GameLoop ());
		}

		private IEnumerator GameLoop ()
		{
			yield return StartCoroutine (RoundStarting ());

			yield return StartCoroutine (RoundPlaying ());

			yield return StartCoroutine (RoundEnding ());

			yield return StartCoroutine (EndGame ());
		}


		private IEnumerator RoundStarting ()
		{
			Clear ();
			SpawnPlayer ();
			messageText.text = "PLAY";
			scoreText.text = "" + playerManager.score;
			healthText.text = "" + playerManager.health;

			yield return startWait;
		}

		private IEnumerator RoundPlaying ()
		{
			SpawnEnemy ();
			messageText.text = "";
			gameStarted = true;

			while (playerManager.alive)
			{
				scoreText.text = "" + playerManager.score;
				healthText.text = "" + playerManager.health;
				SpawnEnemy ();
				yield return null;
			}
		}

		private IEnumerator RoundEnding ()
		{
			messageText.text = "GAME FINISHED!";
			gameFinished = true;
			yield return endWait;
		}

		void DisplayScore ()
		{
			messageText.text = scoreText.text;
			scoreText.enabled = false;
			healthText.enabled = false;
		}

		IEnumerator EndGame ()
		{
			DisplayScore ();
			yield return endWait;
			SceneManager.LoadScene (0);
		}

		void SpawnEnemy ()
		{
			for (int i = 0; i < enemySpawnPoints.Length; i++)
			{
				if (enemySpawnPoints [i].enemy == null)
				{
					enemySpawnPoints [i].enemy = (GameObject)Instantiate (enemyPrefab, enemySpawnPoints [i].transform.position, enemySpawnPoints [i].transform.rotation);
				}
			}

		}

		void SpawnPlayer ()
		{
			if (player == null)
			{
				player = (GameObject)Instantiate (playerPrefab, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
				playerManager = player.GetComponentInChildren<PlayerManager> ();
				FlightControl fc = player.GetComponentInChildren<FlightControl> ();
				fc.m_thrust = fc.m_maxThrust / 2f;
				fc.m_velocity = fc.m_maxVelocity / 4f;
			}
		}

		void Clear ()
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

			for (int i = 0; i < players.Length; i++)
			{
				players [i].SetActive (false);
			}

			for (int i = 0; i < enemies.Length; i++)
			{
				enemies [i].SetActive (false);
			}
		}
	}
}