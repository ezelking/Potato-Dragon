using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextBehaviourScript : MonoBehaviour
{
	public GameObject textBox;
	public Text theText;
	public TextAsset textFile;

	public string[] textLines;
	public int currentLine;
	public int endAtLine;

	private float TextTimer;

	public PlayerController player;
	
	// Function for mouseclicks
	private void Start()
	{
		TextTimer = 0f;
		Init();
	}

	private void Init()
	{
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
		theText.text = textLines[currentLine];

		if (Input.GetMouseButtonDown(0) && !textBox.activeSelf && currentLine < endAtLine && textFile != null)
		{
			Init();
		}

		TextTimer -= Time.deltaTime;
	}

	private void NextLine()
	{
		currentLine++;
		if (currentLine < endAtLine)
		{
			TextTimer = textLines[currentLine].Length * 0.25f + 1;
			Invoke("NextLine", TextTimer);
		}
		else
		{
			textBox.SetActive(false);
		}
	}
}
