using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Door : NetworkBehaviour
{
    bool canBeActivated = false;
    bool activated = false;
    public BoxCollider2D frame;
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !activated)
        {
            collision.GetComponent<PlayerMovementController>().blankInteract.Add(Open);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!activated)
        {
            canBeActivated = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerMovementController>().blankInteract.Remove(Open);
        }
    }
    [ClientRpc][Command]
    public void Open()
    {
        GetComponent<Animator>().Play("Base Layer.DoorOpen");
        GetComponent<SpriteRenderer>().color = Color.gray;
        activated = true;
        canBeActivated = false;
        frame.enabled = false;
    }
}
