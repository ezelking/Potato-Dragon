using UnityEngine;

public class CharacterScript : MonoBehaviour {
    public bool target = false;

    public int flightNumber = 0;
    bool boarding = false;

    public bool suspected = false;

    public TextAsset script;

    public int currentline = 0;

	// Update is called once per frame
	void Update () {
        if (GetComponentInParent<FlightsScript>().GetDepartureTime(flightNumber) <= 0)
            boarding = true;
        if (suspected && !target)
            Debug.Log("You Lose");
        else if (target && suspected)
            Debug.Log("You Win");
	}

    //Assign a flight to this character
    public void SetFlight(int flightnr) {
        flightNumber = flightnr;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
                GetComponentInParent<TextBehaviourScript>().ShowText(this);
    }
}
