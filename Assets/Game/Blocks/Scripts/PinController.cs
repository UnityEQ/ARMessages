using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class PinController : MonoBehaviour {
	public Mapbox.Utils.Vector2d originPos;
	public BasicMap Map;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(originPos);
		//var pos = Conversions.GeoToWorldPosition(originPos, Map.CenterMercator, Map.WorldRelativeScale);
		//transform.position = new Vector3((float)pos.x, 0, (float)pos.y);
	}
}
