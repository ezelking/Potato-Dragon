using UnityEngine;

public class CharacterScript : MonoBehaviour {
    public bool target = false;

    public int flightNumber = 0;
    bool boarding = false;
    
	
	// Update is called once per frame
	void Update () {
        if (GetComponentInParent<FlightsScript>().GetDepartureTime(flightNumber) <= 0)
            boarding = true;
	}

    //Assign a flight to this character
    public void SetFlight(int flightnr) {
        flightNumber = flightnr;
    }
}
