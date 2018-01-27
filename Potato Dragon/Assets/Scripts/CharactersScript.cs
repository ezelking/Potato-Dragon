using System.Collections.Generic;
using UnityEngine;

public class CharactersScript : MonoBehaviour {
    public int charactersToSpawn = 5;

    List<CharacterScript> spawnedCharacters;

    public GameObject characterTemplate;
    public Sprite[] sprites;

    // Use this for initialization
    void Start () {
        spawnedCharacters = new List<CharacterScript>();
        for (int i = 0; i < charactersToSpawn; i++) {
            GameObject spawnedCharacter = Instantiate(characterTemplate, transform);
            spawnedCharacter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[Random.Range(0,sprites.Length)];
            spawnedCharacter.transform.position = new Vector3(1, 2, 1) * i;
            CharacterScript newCharacter = spawnedCharacter.GetComponent<CharacterScript>();
            spawnedCharacters.Add(newCharacter);
            newCharacter.SetFlight(GetComponent<FlightsScript>().ChooseFlight());
        }
        ChooseTarget();
	}

    //Choose a random charactrer to be the suspect
    void ChooseTarget() {
        spawnedCharacters[Random.Range(0, spawnedCharacters.Count)].target = true;
    }
}
