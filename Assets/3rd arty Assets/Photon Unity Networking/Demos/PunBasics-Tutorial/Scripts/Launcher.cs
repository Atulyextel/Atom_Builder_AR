// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to connect, and join/create room automatically
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Launch manager. Connect, join a random room or create one if none or all full.
/// </summary>
namespace ExitGames.Demos.DemoAnimator
{
	public class Launcher : Photon.PunBehaviour
	{

		#region Public Variables
		public GameObject canvas;
		[Tooltip ("The Ui Text to inform the user about the connection progress")]
		public Text feedbackText;

		[Tooltip ("The maximum number of players per room")]
		public byte maxPlayersPerRoom = 4;

		#endregion

		#region Private Variables

		bool isConnecting;
		string _gameVersion = "1";

		#endregion

		#region MonoBehaviour CallBacks

		void Awake ()
		{
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.automaticallySyncScene = true;
			PhotonNetwork.offlineMode = false;

		}

		private void Start ()
		{
			if (!isConnecting) {
				Connect ();
			}

		}

		#endregion


		#region Public Methods

		public void Connect ()
		{
			// we want to make sure the log is clear everytime we connect, we might have several failed attempted if connection failed.
			feedbackText.text = "";

			// keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
			isConnecting = true;

			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			if (PhotonNetwork.connected) {
				//LogFeedback("Joining Room...");
				// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
				PhotonNetwork.JoinRoom (PlayerPrefs.GetString ("roomName"));
			} else {

				//LogFeedback("Connecting...");

				// #Critical, we must first and foremost connect to Photon Online Server.
				PhotonNetwork.ConnectUsingSettings (_gameVersion);
			}
		}

		/// <summary>
		/// Logs the feedback in the UI view for the player, as opposed to inside the Unity Editor for the developer.
		/// </summary>
		/// <param name="message">Message.</param>
		void LogFeedback (string message)
		{
			// we do not assume there is a feedbackText defined.
			if (feedbackText == null) {
				return;
			}

			// add new messages as a new line and at the bottom of the log.
			feedbackText.text += System.Environment.NewLine + message;
		}

		#endregion


		#region Photon.PunBehaviour CallBacks

		// below, we implement some callbacks of PUN
		// you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage


		/// <summary>
		/// Called after the connection to the master is established and authenticated but only when PhotonNetwork.autoJoinLobby is false.
		/// </summary>
		public override void OnConnectedToMaster ()
		{

			Debug.Log ("Region:" + PhotonNetwork.networkingPeer.CloudRegion);

			// we don't want to do anything if we are not attempting to join a room. 
			// this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
			// we don't want to do anything.
			if (isConnecting) {
				//LogFeedback("Connected To Master: Next -> try to Join Room " + AllApiCalls.roomName);
				Debug.Log ("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");
//				string guestName = "InfiVR" + Random.Range(0,1000);
//				PhotonNetwork.playerName = PlayerPrefs.GetString(AllApiCalls.userName, guestName);
				//PhotonNetwork.JoinLobby(TypedLobby.Default);
				PhotonNetwork.JoinRoom (PlayerPrefs.GetString ("roomName"));
			}
		}

		public override void OnPhotonJoinRoomFailed (object[] codeAndMsg)
		{
			//LogFeedback("<Color=Red>Failed To Join the Room</Color>: Next -> Creating a new Room");
			Debug.Log ("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

			// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
			PhotonNetwork.CreateRoom (PlayerPrefs.GetString("roomName"), new RoomOptions () { MaxPlayers = this.maxPlayersPerRoom }, null);
		}


		public override void OnPhotonCreateRoomFailed (object[] codeAndMsg)
		{
			LogFeedback ("Room is full Try again Later");
			print ("Create room failed reason is " + codeAndMsg [1]);
		}
			
		public override void OnDisconnectedFromPhoton ()
		{
			LogFeedback ("<Color=Red>Not Connected with network</Color>");
			Debug.LogError ("DemoAnimator/Launcher:Disconnected");
			isConnecting = false;
			PhotonNetwork.offlineMode = true;
		}

		public override void OnJoinedRoom ()
		{
			LogFeedback ("<Color=Green>Joined Experience Successfully</Color>");
			Debug.Log ("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

			//LogFeedback("<Color=Red>PlayerName : </Color>" + PhotonNetwork.playerName);
			Invoke ("DisableFeeback", 3);
			canvas.SetActive (true);
		}

		void DisableFeeback ()
		{
			feedbackText.text = "";
		}

		#endregion

		
	}
}