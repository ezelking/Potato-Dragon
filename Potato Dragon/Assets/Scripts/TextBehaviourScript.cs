using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextBehaviourScript : MonoBehaviour
{
	public GameObject textBox;
    public Image characterImage;
	public Text theText;
	public List<TextAsset> suspiciousTextFiles;
    public List<TextAsset> normalTextFiles;

    public string[] textLines;
	public int endAtLine;

	private float TextTimer;

    CharacterScript selectedPerson;

	public PlayerController player;
	
	// Function for mouseclicks
	private void Start()
	{
		TextTimer = 0f;
	}

	public void ShowText(CharacterScript target)
    {
        selectedPerson = target;
        
        if (selectedPerson.script == null)
            if (selectedPerson.target)
                selectedPerson.script = suspiciousTextFiles[Random.Range(0, suspiciousTextFiles.Count)];
            else
                selectedPerson.script = normalTextFiles[Random.Range(0, normalTextFiles.Count)];

        textBox.SetActive(true);
        characterImage.gameObject.SetActive(true);

        characterImage.sprite = GetComponent<CharactersScript>().GetSprite(selectedPerson,true);

        if (selectedPerson.script != null)
        {
            textLines = (selectedPerson.script.text.Split('\n'));
        }
            endAtLine = textLines.Length - 1;

        NextLine();


    }

    private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Z))
            Reset();

        if (Input.GetKey(KeyCode.X))
            selectedPerson.suspected = true;

		TextTimer -= Time.deltaTime;
	}

	private void NextLine()
	{
		if (selectedPerson.currentline <= endAtLine)
		{
			TextTimer = textLines[selectedPerson.currentline].Length * 0.1f + 1;
			Invoke("NextLine", TextTimer);
            StartTextDisplay();
            selectedPerson.currentline++;
        }
		else
		{
            theText.text = "This phone conversation has ended, press X to accuse this person or press Z to return.";
            //textBox.SetActive(false);
        }
    }

    private void Reset() {
        textLines = null;
        textBox.SetActive(false);
        characterImage.gameObject.SetActive(false);
        CancelInvoke("NextLine");
        StopAllCoroutines();
    }

    private string str;
    void StartTextDisplay()
    {
        StartCoroutine(AnimateText(textLines[selectedPerson.currentline]));
    }


    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            theText.text = str;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
