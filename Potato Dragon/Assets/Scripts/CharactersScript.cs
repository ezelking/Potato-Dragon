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
            spawnedCharacter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[Random.Range(0,sprites.Length/3) *3];
            while(!LegalPosition(spawnedCharacter.transform.position))
                spawnedCharacter.transform.position = new Vector3(Random.Range(-4.5f,4.8f), Random.Range(-1.2f,3.2f), -5f);
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

    public bool LegalPosition(Vector3 position) {
        return !(position.x > -3f && position.x < -1.4f && position.y < 3.1f && position.y > -0.64f) && !(position.x > -0.55f && position.x < 1.05f && position.y < 3.1f && position.y > -0.64f) && !(position.x > 1.8f && position.x < 3.4 && position.y < 3.1f && position.y > -0.64f);
    }

    public Sprite GetSprite(CharacterScript character, bool baseSprite)
    {
        int spriteBaseIndex = (int)System.Math.Floor((double)(System.Array.IndexOf(sprites, character.GetComponent<SpriteRenderer>().sprite) / 3))*3;
        if (baseSprite)
            return sprites[spriteBaseIndex];
        switch (character.dir)
        {
            case CharacterScript.Direction.Left:
                return sprites[spriteBaseIndex + 2];
            case CharacterScript.Direction.Right:
                return sprites[spriteBaseIndex + 1];
            default:
                return sprites[spriteBaseIndex];
        }
    }
}
