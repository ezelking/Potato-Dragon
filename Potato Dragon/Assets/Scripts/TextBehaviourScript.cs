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

	public PlayerController player;
	
	// Function for mouseclicks
	private void Start()
	{
		Init();
	}

	private void Init()
	{
		if (textFile != null)
		{
			textLines = (textFile.text.Split('\n'));
		}

		if (endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}
	}

	private void Update()
	{
		theText.text = textLines[currentLine];

		if (Input.GetMouseButtonDown(0) && !textBox.activeSelf && currentLine < endAtLine)
		{
			Debug.Log("poep");
			if (textFile == null) return;
			textBox.SetActive(true);
			Init();
		} else if (Input.GetKeyDown(KeyCode.Return) && currentLine < endAtLine)
		{
			currentLine++;
		} else if (Input.GetKeyDown(KeyCode.Return) && currentLine >= endAtLine)
		{
			textBox.SetActive(false);
		} else if (Input.GetMouseButtonDown(0) && currentLine < endAtLine)
		{
			currentLine++;
		} else if (Input.GetMouseButtonDown(0) && currentLine >= endAtLine)
		{
			textBox.SetActive(false);
		}
	}
}
