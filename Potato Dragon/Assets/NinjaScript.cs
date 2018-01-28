using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaScript : MonoBehaviour {
    Vector3 target = new Vector3(100,100,100);
    Transform suspect;
	
    public void ChooseTarget(Transform position)
    {
        suspect = position;
        this.transform.position = new Vector3(position.position.x + 0.35f,6.2f,-5);
        target = new Vector3(position.position.x + 0.35f, position.position.y + 2.4f, -5);
    }

	// Update is called once per frame
	void Update () {
        if (suspect != null)
        {
            if (suspect.parent == transform)
            {
                transform.Translate(new Vector3(0, 0.1f, 0));
                if (transform.position.y >6.3f)
                    GameObject.Find("GameManager").GetComponent<GameManagerScript>().Win();


            } else if (transform.position.y > target.y)
            {
                transform.Translate(new Vector3(0, -0.1f, 0));
            }
            else
            {
                suspect.parent = transform;
                suspect.GetComponent<CharacterScript>().enabled = false;
            }
        }

	}
}
