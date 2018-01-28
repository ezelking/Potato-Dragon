using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextBehaviourScript : MonoBehaviour
{
	public GameObject textBox;
    public Image characterImage;
    public Image phoneImage;
	public Text theText;
	public List<TextAsset> suspiciousTextFiles;
    public List<TextAsset> normalTextFiles;
    public List<TextAsset> briefingTextFiles;
    public List<TextAsset> winTextFiles;
    public List<TextAsset> loseTextFiles;


    public string[] textLines;
	public int endAtLine;
	int currentLine;

    private float TextTimer;

    public CharacterScript selectedPerson;

	public PlayerController player;
	
	private void Start()
	{
		TextTimer = 0f;
	}

	public void ShowCharacterText(CharacterScript target)
    {
        selectedPerson = target;
        
        if (selectedPerson.script == null)
            if (selectedPerson.target)
                selectedPerson.script = suspiciousTextFiles[Random.Range(0, suspiciousTextFiles.Count)];
            else
                selectedPerson.script = normalTextFiles[Random.Range(0, normalTextFiles.Count)];

        textBox.SetActive(true);
        characterImage.gameObject.SetActive(true);
        phoneImage.gameObject.SetActive(true);

        characterImage.sprite = GetComponent<GameManagerScript>().level.GetComponent<CharactersScript>().GetSprite(selectedPerson,true);

        if (selectedPerson.script != null)
        {
            textLines = (selectedPerson.script.text.Split('\n'));
        }
            endAtLine = textLines.Length - 1;
        currentLine = selectedPerson.currentline;
        NextLine();


    }

    public void ShowBriefingText()
    {
        
                //selectedPerson.script = briefingTextFiles[Random.Range(0, briefingTextFiles.Count)];

        textBox.SetActive(true);
        
            textLines = (briefingTextFiles[Random.Range(0, briefingTextFiles.Count)].text.Split('\n'));
        endAtLine = textLines.Length - 1;

        NextLine();
    }
    public void ShowWinningText()
    {
        theText.alignment = TextAnchor.MiddleLeft;

        //selectedPerson.script = briefingTextFiles[Random.Range(0, briefingTextFiles.Count)];

        textBox.SetActive(true);

        textLines = (winTextFiles[Random.Range(0, winTextFiles.Count)].text.Split('\n'));
        endAtLine = textLines.Length - 1;

        NextLine();
    }
    public void ShowLosingText()
    {

        theText.alignment = TextAnchor.MiddleLeft;
        //selectedPerson.script = briefingTextFiles[Random.Range(0, briefingTextFiles.Count)];

        textBox.SetActive(true);

        textLines = (loseTextFiles[Random.Range(0, loseTextFiles.Count)].text.Split('\n'));
        endAtLine = textLines.Length - 1;

        NextLine();
    }

    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().state == GameManagerScript.GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                Reset();

            if (Input.GetKey(KeyCode.X))
                selectedPerson.suspected = true;

            TextTimer -= Time.deltaTime;
        }
	}

	private void NextLine()
	{
            if (currentLine <= endAtLine)
		{
            if (textLines[currentLine][0] == '>')
            {
                
                    theText.alignment = TextAnchor.MiddleRight;
                textLines[currentLine] = textLines[currentLine].Substring(1, textLines[currentLine].Length-1);
               } else { theText.alignment = TextAnchor.MiddleLeft; }
            TextTimer = textLines[currentLine].Length * 0.1f + 2;
            
            Invoke("NextLine", TextTimer);
            StartTextDisplay();
            if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().state == GameManagerScript.GameState.Playing)
                selectedPerson.currentline++;
            currentLine++;
        }
		else
		{
            if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().state == GameManagerScript.GameState.Playing)
                theText.text = "This phone conversation has ended, press X to accuse this person or press Z to return.";
            else if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().state == GameManagerScript.GameState.Briefing)
            {
                currentLine = 0;
                GameObject.Find("GameManager").GetComponent<GameManagerScript>().StartLevel();
                textBox.SetActive(false);
            }
            else
            {
                currentLine = -1;
                GameObject.Find("GameManager").GetComponent<GameManagerScript>().Replay();
            }
            //textBox.SetActive(false);
        }
    }

    public void Reset() {
        textLines = null;
        textBox.SetActive(false);
        characterImage.gameObject.SetActive(false);
        phoneImage.gameObject.SetActive(false);
        currentLine = 0;
        CancelInvoke("NextLine");
        StopAllCoroutines();
    }

    private string str;
    void StartTextDisplay()
    {
        StartCoroutine(AnimateText(textLines[currentLine]));
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
                GetComponent<AudioSource>().Play();
        }
    }
}
