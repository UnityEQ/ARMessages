using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Core;

public class BlocksSpawner : MonoBehaviour {

	private static BlocksSpawner _instance;
	public static BlocksSpawner Instance { get { return _instance; } } 

	public GameObject messagePrefabAR;

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (DelayTestMessage ());
	}

	IEnumerator DelayTestMessage(){

		yield return new WaitForSeconds (2f);
		//LoadAllMessages();
		SaveMessage ("1", 1.1,1.1,1.1,1,1);
	}

	public void RemoveAllMessages(){
		new GameSparks.Api.Requests.LogEventRequest ()
			.SetEventKey ("REMOVE_MESSAGES")
			.Send ((response) => {
			if (!response.HasErrors) {
				Debug.Log ("Message Saved To GameSparks...");
			} else {
				Debug.Log ("Error Saving Message Data...");
			}
		});
	}

	public void LoadAllMessages(){

		List<GameObject> messageObjectList = new List<GameObject> ();
		
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LOAD_BLOCKS").Send((response) => {
			if (!response.HasErrors) {
				Debug.Log("Received Player Data From GameSparks...");
				List<GSData> locations = response.ScriptData.GetGSDataList ("all_Blocks");
				for (var e = locations.GetEnumerator (); e.MoveNext ();) {
					
					GameObject MessageBubble = Instantiate (messagePrefabAR);
					Message message = MessageBubble.GetComponent<Message>();

					//message.name = e.Current.GetString ("name");
					//message.lat = double.Parse(e.Current.GetString ("lat"));
					//message.lon = double.Parse(e.Current.GetString ("lon"));
					//message.alt = double.Parse(e.Current.GetString ("alt"));
					//message.type = int.Parse(e.Current.GetString ("type"));
					//message.type = int.Parse(e.Current.GetString ("health"));
					messageObjectList.Add(MessageBubble);
				}
			} else {
				Debug.Log("Error Loading Message Data...");
			}
		});

		ARMessageProvider.Instance.LoadARMessages (messageObjectList);
	}

	public void SaveMessage(string name, double lat, double lon, double alt, int type, int health){
		new GameSparks.Api.Requests.LogEventRequest ()

			.SetEventKey ("SAVE_BLOCKS")
			.SetEventAttribute ("name", name)
			.SetEventAttribute ("lat", lat.ToString())
			.SetEventAttribute ("lon", lon.ToString())
			.SetEventAttribute ("alt", alt.ToString())
			.SetEventAttribute ("type", type.ToString())
			.SetEventAttribute ("health", health.ToString())
			.Send ((response) => {
				
			if (!response.HasErrors) {
				Debug.Log ("Message Saved To GameSparks...");
			} else {
				Debug.Log ("Error Saving Message Data...");
			}
		});
	}
}
