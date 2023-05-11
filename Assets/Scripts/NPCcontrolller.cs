using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCcontrolller : MonoBehaviour
{
    public Animator head;
    public Animator body;
    public Abilities abilities;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (!abilities.animating)
        {
            HeadAnims();
            BodyAnims();
        }

    }
    public void HeadAnims()
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
    public void BodyAnims()
    {
        if (GetComponent<Rigidbody2D>().velocity.y == 0 && GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            abilities.body.Play("Base Layer.Idle");
        }
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
            abilities.body.Play("Base Layer.Falling");
    }

}
