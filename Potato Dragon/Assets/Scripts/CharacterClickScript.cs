using UnityEngine;

public class CharacterClickScript : MonoBehaviour {
    
	//Checks if the cursor collides with the collider
	void OnMouseOver () {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponentInParent<TextBehaviourScript>().ShowText(GetComponent<CharacterScript>().target);
            Debug.Log("Clicked");
        }
	}
}
