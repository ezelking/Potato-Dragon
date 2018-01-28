using UnityEngine;

public class CharacterScript : MonoBehaviour {
    public enum Direction { Up, Down, Right, Left }
    public Direction dir;
    public bool target = false;

    public int flightNumber = 0;
    public bool boarding = false;

    public bool suspected = false;

    public TextAsset script;

    public int currentline = 0;

    public Vector3 targetPosition = Vector3.zero;

    Vector3 gateA, gateB, gateC, gateD;

    private void Start()
    {
        GetTargetPosition();
        gateA = new Vector3(-4.8f, -1.2f, -4);
        gateB = new Vector3(-4.8f, 3.2f, -4);
        gateC = new Vector3(4.8f, -1.2f, -4);
        gateD = new Vector3(4.8f, 3.2f, -4);
    }

    // Update is called once per frame
    void Update () {
        if (!boarding && GetComponentInParent<FlightsScript>().GetDepartureTime(flightNumber) <= 0)
            BoardPlane();
        if (suspected && !target )//|| boarding)
            Debug.Log("You Lose");
        else if (target && suspected)
            Debug.Log("You Win");
        Movement();
        
        if (Vector3.Distance(transform.position, targetPosition) < 1.5f)
            if (!boarding)
            {
                GetTargetPosition();
            } else
            {
                this.enabled = false;
            }
	}

    //Assign a flight to this character
    public void SetFlight(int flightnr) {
        flightNumber = flightnr;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GetComponentInParent<TextBehaviourScript>().textBox.activeSelf == false)
                GetComponentInParent<TextBehaviourScript>().ShowText(this);
    }

    void Movement()
    {
        CheckMovementEnd();
        switch (dir)
        {
            case Direction.Left:
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
                break;
            case Direction.Right:
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
                break;
            case Direction.Up:
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
                break;
            case Direction.Down:
                transform.position += new Vector3(0, -1, 0) * Time.deltaTime;
                break;
        }
    }

    void BoardPlane()
    {
        boarding = true;
        switch (Random.Range(0, 3))
        {
            case 0:
                targetPosition = gateA;
                break;
            case 1:
                targetPosition = gateB;
                break;
            case 2:
                targetPosition = gateC;
                break;
            case 3:
                targetPosition = gateD;
                break;
        }
    }

    void CheckMovementEnd()
    {
        switch (dir)
        {
            case Direction.Left:
                if (transform.position.x <= targetPosition.x)
                    if (transform.position.y < targetPosition.y)
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(0, 0.5f, 0)))
                        {
                            dir = Direction.Up;
                            ChangeSprite();
                        }
                        }
                    else
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(0, -0.5f, 0)))
                        {
                            dir = Direction.Down;
                            ChangeSprite();
                        }
                    }                    
                break;
            case Direction.Right:
                if (transform.position.x >= targetPosition.x)
                    if (transform.position.y < targetPosition.y)
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(0, 0.5f, 0)))
                        {
                            dir = Direction.Up;
                            ChangeSprite();
                        }
                    }
                    else
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(0, -0.5f, 0)))
                        {
                            dir = Direction.Down;
                            ChangeSprite();
                        }
                    }
                break;
            case Direction.Up:
                if (transform.position.y > 3.1)
                    if (transform.position.x < targetPosition.x)
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(0.5f, 0, 0))) { 
                            dir = Direction.Right;
                        ChangeSprite();
                    }
                    }
                    else
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(-0.5f, 0, 0)))
                        {
                            dir = Direction.Left;
                            ChangeSprite();
                        }
                    }

                break;
            case Direction.Down:
                if (transform.position.y < -0.64f)
                    if (transform.position.x < targetPosition.x)
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(0.5f, 0, 0)))
                        {
                            dir = Direction.Right;
                            ChangeSprite();
                        }
                    }
                    else
                    {
                        if (GetComponentInParent<CharactersScript>().LegalPosition(transform.position + new Vector3(-0.5f, 0, 0)))
                        {
                            dir = Direction.Left;
                            ChangeSprite();
                        }
                    }

                break;
        }
    }

    void GetTargetPosition()
    {
        targetPosition = Vector3.zero;
        while (!GetComponentInParent<CharactersScript>().LegalPosition(targetPosition))
            targetPosition = new Vector3(Random.Range(-4.3f, 4.8f), Random.Range(-1.2f, 3.2f), -5f);
        if (System.Math.Abs(targetPosition.x - transform.position.x) < 1.6)
            if (targetPosition.x - transform.position.x > 0.01)
            {
                dir = Direction.Right;
                ChangeSprite();

            }
            else if (targetPosition.x - transform.position.x < 0.01)
            {
                dir = Direction.Left;
                ChangeSprite();
            }
            else
            {
                if (targetPosition.y - transform.position.y < 0.01)
                {
                    dir = Direction.Up;
                    ChangeSprite();
                }
                else if (targetPosition.y - transform.position.y > 0.01)
                {
                    dir = Direction.Down;
                    ChangeSprite();
                }
            }
    }

    void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = GetComponentInParent<CharactersScript>().GetSprite(this);
    }
}
