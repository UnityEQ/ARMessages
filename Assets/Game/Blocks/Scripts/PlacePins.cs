using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Examples;

public class PlacePins : MonoBehaviour {

	public List<Vector3> Coordinates; // for network
	public AbstractMap Map;
	public GameObject pin;
	public GameObject DiamondOre;
	public PositionWithLocationProvider locationProvider;
	
	// Use this for initialization
	void Start () {
       // Start service before querying location
        Input.location.Start();
		//in here, we will set coords from the database and then place them around
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void LoadPins()
	{
		foreach (var item in Coordinates)
            {
				GameObject pinObj = Instantiate(pin, this.transform.position, this.transform.rotation);
				pinObj.transform.parent = this.gameObject.transform;
				pinObj.transform.position = new Vector3(item.x, 0, item.z);

				GameObject newBlock = Instantiate (DiamondOre, this.transform.position, this.transform.rotation);
				//GameObject pinObj = Instantiate(pin, this.transform.position, this.transform.rotation);
				newBlock.transform.parent = this.gameObject.transform;
				newBlock.transform.position = new Vector3(item.x, item.y, item.z);
				//and then remove this loc from LIST
            }
	}
	
	public void PinPlacer(GameObject blockObject, float cursorY)
	{
		GameObject pinObj = Instantiate(pin, this.transform.position, this.transform.rotation);
		pinObj.transform.parent = this.gameObject.transform;
		pinObj.transform.position = new Vector3((float)locationProvider._targetPosition.x, 0f, (float)locationProvider._targetPosition.z);
		
		PinController pc = blockObject.GetComponent<PinController>();
		pc.originPos.x = locationProvider.latlon.x;
		pc.originPos.y = locationProvider.latlon.y;
		pc.Map = Map;
		LocationInfo li = new LocationInfo();
		//Debug.Log("alt: " + Input.location.lastData.altitude);
		float deviceAlt = Input.location.lastData.altitude;
		//Debug.log("alt: " + Input.location.lastData.altitude);
		BlocksSpawner.Instance.SaveMessage ("test",cursorY,locationProvider.latlon.x, locationProvider.latlon.y,Input.location.lastData.altitude,1,1);
	}
}