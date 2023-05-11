using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Steamworks;

public class PlayerMovementController : NetworkBehaviour
{
    public float speed = .01f;
    public float jumpHeight = 1;
    public GameObject PlayerModel;
    bool canHopDown = false;
    bool hoppingDown = false;
    bool KeycodeW = false;
    public Abilities abilities;
    public ElementUI elementUI;
    public string CurrentElements;

    public delegate void DoEventHit();
    public List<DoEventHit> blankInteract = new List<DoEventHit>();

    [SyncVar] Color playerColor;
    [SyncVar] public bool flippedLookingRight = false;

    private void Start()
    {
        PlayerModel.SetActive(false);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        CurrentElements = "None";
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Lobby" && NetworkClient.ready)
        {
            if (PlayerModel.activeSelf == false)
            {
                transform.position = new Vector3(-1, GetComponent<PlayerObjectController>().ConnectionID * 2 - 2, 0);
                if (hasAuthority)
                    elementUI.gameObject.SetActive(true);
                PlayerModel.SetActive(true);
                CmdChangeColor();
                abilities.head.GetComponent<SpriteRenderer>().color = playerColor;
                abilities.body.GetComponent<SpriteRenderer>().color = playerColor;
                StartCoroutine(IncreaseGravity());
                abilities.UIShit.GetComponent<TextMesh>().text = this.GetComponent<PlayerObjectController>().PlayerName;
            }
            if (hasAuthority)
            {
                Movement();
                if (!abilities.animating)
                {
                    BodyAnims();
                    CallAbilities();
                    HeadAnims();
                }
                if (isLocalPlayer)
                {
                    // Allow the local player to change their color
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        CmdChangeColor();
                    }
                }
                else
                {
                    // Update the color of non-local players
                    abilities.head.GetComponent<SpriteRenderer>().color = playerColor;
                    abilities.body.GetComponent<SpriteRenderer>().color = playerColor;
                }
            }
            ScreenClamp();
        }
    }
    [Command]
    private void CmdChangeColor()
    {
        //playerColor = new Color(Random.value, Random.value, Random.value);
        playerColor = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        playerColor.a = 1;
        abilities.head.GetComponent<SpriteRenderer>().color = playerColor;
        abilities.body.GetComponent<SpriteRenderer>().color = playerColor;
    }    
    IEnumerator IncreaseGravity()
    {
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Rigidbody2D>().gravityScale++;
            yield return new WaitForSeconds(.03f);
        }
    }
    IEnumerator inputW()
    {
        KeycodeW = true;
        yield return new WaitForSeconds(.2f);
        KeycodeW = false;
    }
    public void Movement()
    {
        float xDirection = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(xDirection, 0, 0);
        if (!abilities.stopMovement)
        {
            if (!hoppingDown)
                transform.position += moveDirection * speed * 15f * Time.deltaTime;
            //makes player jump
            if (KeycodeW && GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                abilities.body.Play("Base Layer.Jump");
            }
            if (Input.GetKeyDown(KeyCode.W))
                StartCoroutine(inputW());
        }
        if (!abilities.animating)
        {
            if ((xDirection < 0 && !flippedLookingRight) || (xDirection > 0 && flippedLookingRight))
            {
                if (isLocalPlayer)
                {
                    CmdFlip();
                }
            }
            if (Input.GetKeyDown(KeyCode.S) && canHopDown)
            {
                StartCoroutine(HopDown());
            }
        }
    }
    [ClientRpc]
    private void RpcFlipModel()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        Vector3 newScale2 = abilities.UIShit.transform.localScale;
        newScale2.x *= -1;
        abilities.UIShit.transform.localScale = newScale2;
        flippedLookingRight = !flippedLookingRight;
    }
    [Command]
    private void CmdFlipModel()
    {
        RpcFlipModel();
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        Vector3 newScale2 = abilities.UIShit.transform.localScale;
        newScale2.x *= -1;
        abilities.UIShit.transform.localScale = newScale2;
        flippedLookingRight = !flippedLookingRight;
    }
    [Command]
    private void CmdFlip()
    {
        flippedLookingRight = !flippedLookingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        abilities.UIShit.transform.localScale = new Vector3(abilities.UIShit.transform.localScale.x * -1, abilities.UIShit.transform.localScale.y, abilities.UIShit.transform.localScale.z);
    }
    public void CallAbilities()
    {
        if (((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Alpha7)) && !flippedLookingRight) || ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Alpha9)) && flippedLookingRight))
        {
            if (isLocalPlayer)
            {
                CmdFlip();
            }
        }
        switch (CurrentElements)
        {
            case "None":
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Alpha7)) || (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Alpha9)))
                {
                    if (isServer)
                    {
                        abilities.RpcMeleeHorizontal();
                    }
                    else
                    {
                        abilities.CmdMeleeHorizontal();
                    }
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Alpha0))
                {
                    if (isServer)
                    {
                        abilities.RpcMeleeUp();
                    }
                    else
                    {
                        abilities.CmdMeleeUp();
                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Alpha8))
                {
                    if (isServer)
                    {
                        abilities.RpcMeleeDown();
                    }
                    else
                    {
                        abilities.CmdMeleeDown();
                    }
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    for (int i = 0; i < blankInteract.Count; i++)
                    {
                        blankInteract[i]();
                    }
                }
                break;
        }
    }
    public void ScreenClamp()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + .5f, maxScreenBounds.x - .5f), transform.position.y, transform.position.z);
    }
    IEnumerator HopDown()
    {
        hoppingDown = true;
        abilities.head.Play("Base Layer.Down");
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(.04f);
        }
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        hoppingDown = false;
        canHopDown = false;
    }
    
    //collisions (interactables)
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bridge"))
            canHopDown = true;
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bridge"))
            canHopDown = true;
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bridge"))
            canHopDown = false;
    }

    //Head Anims
    public void HeadAnims()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Alpha0)) && hasAuthority)
        {
            abilities.head.Play("Base Layer.Up");
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Alpha8)) && hasAuthority)
        {
            abilities.head.Play("Base Layer.Down");
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && GetComponent<Rigidbody2D>().velocity.y == 0)
            abilities.head.Play("Base Layer.Sprint");
        else if (!hoppingDown)
        {
            switch (GetComponent<Rigidbody2D>().velocity.y)
            {
                case > 2:
                    abilities.head.Play("Base Layer.Up");
                    break;
                case < -6:
                    abilities.head.Play("Base Layer.Down");
                    break;
                default:
                    abilities.head.Play("Base Layer.Forward");
                    break;
            }
        }
    }
    public void BodyAnims()
    {
        if (GetComponent<Rigidbody2D>().velocity.y == 0) {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                abilities.body.CrossFade("Base Layer.Sprint", 0);
            }
            else if (GetComponent<Rigidbody2D>().velocity.x == 0)
                abilities.body.Play("Base Layer.Idle");
        }
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
            abilities.body.Play("Base Layer.Falling");
    }
}
