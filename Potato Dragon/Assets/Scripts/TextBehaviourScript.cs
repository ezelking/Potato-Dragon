using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextBehaviourScript : MonoBehaviour
{
	public GameObject textBox;
	public Text theText;
	public List<TextAsset> suspiciousTextFiles;
    public List<TextAsset> normalTextFiles;

    public string[] textLines;
	public int currentLine;
	public int endAtLine;

	private float TextTimer;

	public PlayerController player;
	
	// Function for mouseclicks
	private void Start()
	{
		TextTimer = 0f;
		//Init();
	}

	public void ShowText(bool target)
	{
        TextAsset textFile;
        if (target)
            textFile = suspiciousTextFiles[Random.Range(0, suspiciousTextFiles.Count)];
        else
            textFile = normalTextFiles[Random.Range(0, normalTextFiles.Count)];

        textBox.SetActive(true);
		
		if (textFile != null)
		{
			textLines = (textFile.text.Split('\n'));
		}

		if (endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}
		
		NextLine();
	}

	private void Update()
	{


		//if (Input.GetMouseButtonDown(0) && !textBox.activeSelf && currentLine < endAtLine && textFile != null)
		//{
			//Init();
		//}

		TextTimer -= Time.deltaTime;
	}

	private void NextLine()
	{
		if (currentLine <= endAtLine)
		{
			TextTimer = textLines[currentLine].Length * 0.25f + 1;
			Invoke("NextLine", TextTimer);
            theText.text = textLines[currentLine];
            currentLine++;
        }
		else
		{
			textBox.SetActive(false);
        }
    }
}
