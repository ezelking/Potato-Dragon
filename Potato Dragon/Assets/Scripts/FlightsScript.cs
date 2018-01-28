using System.Collections.Generic;
using UnityEngine;

public class FlightsScript : MonoBehaviour {
    int[] flights = new int[] { 100, 200 };

    Dictionary<int,float> departureTimes = new Dictionary<int, float>();
    float timePassed = 0f; 

    // Use this for initialization
    void Start() {
        foreach (int i in flights)
            departureTimes.Add(i, Random.Range(60f, 85f));        
	}

    // Update is called once per frame
    void Update() {
        timePassed += Time.deltaTime;
    }

    //Return a random flight number
    public int ChooseFlight() {
        return flights[Random.Range(0, flights.Length)];
    }
    
    //Get time remaining untill departure
    public float GetDepartureTime(int flightnr) {
        float departureTime = 0f;
        departureTimes.TryGetValue(flightnr, out departureTime);
        departureTime -= timePassed;
        return departureTime;
    }
}
