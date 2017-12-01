using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Examples;

public class PlacePins : MonoBehaviour {

	public List<string> Coordinates; // for network
	public BasicMap Map;
	public GameObject pin;
	public PositionWithLocationProvider locationProvider;
	
	// Use this for initialization
	void Start () {
		//in here, we will set coords from the database and then place them around
		//this will be a work in progress
		Map.OnInitialized += () =>
        {
            foreach (var item in Coordinates)
            {
                var latLonSplit = item.Split(',');
                var llpos = new Vector2d(double.Parse(latLonSplit[0]), double.Parse(latLonSplit[1]));
                var pos = Conversions.GeoToWorldPosition(llpos, Map.CenterMercator, Map.WorldRelativeScale);
                var gg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                gg.transform.position = new Vector3((float)pos.x, 0, (float)pos.y);
				//and then remove this loc from LIST
            }
        };
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void PinPlacer()
	{
		GameObject cube = Instantiate(pin, this.transform.position, this.transform.rotation);
		cube.transform.parent = this.gameObject.transform;
		cube.transform.position = new Vector3((float)locationProvider._targetPosition.x, 0f, (float)locationProvider._targetPosition.z);
		
		PinController pc = cube.GetComponent<PinController>();
		//pc.originPos = locationProvider.latlon;
		pc.Map = Map;
	}
}