using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Examples;
using GameSparks.Core;

public class BlocksSpawner : MonoBehaviour {

	private static BlocksSpawner _instance;
	public PlacePins placePins;
	public AbstractMap Map;
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
		LoadAllMessages();
		//SaveMessage ("1", 1.1,1.1,1.1,1,1);
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
					var lat = double.Parse(e.Current.GetString ("lat"));
					var lon = double.Parse(e.Current.GetString ("lon"));
					var blockY = float.Parse(e.Current.GetString ("blockY"));
					var llpos = new Vector2d(lat, lon);
					var pos = Conversions.GeoToWorldPosition(llpos, Map.CenterMercator, Map.WorldRelativeScale);					
					placePins.Coordinates.Add(new Vector3((float)pos.x,blockY,(float)pos.y));
//					placePins.CoordinatesB.Add(float.Parse(e.Current.GetString ("cursorY")));
				}
				placePins.LoadPins();
			} else {
				Debug.Log("Error Loading Message Data...");
			}
		});

		//ARMessageProvider.Instance.LoadARMessages (messageObjectList);
	}

	public void SaveMessage(string name,float cursorY,double lat, double lon, float alt, int type, int health){
		new GameSparks.Api.Requests.LogEventRequest ()

			.SetEventKey ("SAVE_BLOCKS")
			.SetEventAttribute ("name", name)
			//.SetEventAttribute ("blockX", lat.ToString())
			.SetEventAttribute ("blockY", cursorY.ToString())
			//.SetEventAttribute ("blockZ", lat.ToString())
			.SetEventAttribute ("lat", lat.ToString())
			.SetEventAttribute ("lon", lon.ToString())
			.SetEventAttribute ("height", alt.ToString())
			.SetEventAttribute ("material", type.ToString())
			.SetEventAttribute ("hp", health.ToString())
			.Send ((response) => {
				
			if (!response.HasErrors) {
				Debug.Log ("Message Saved To GameSparks...");
			} else {
				Debug.Log ("Error Saving Message Data...");
			}
		});
	}
}
