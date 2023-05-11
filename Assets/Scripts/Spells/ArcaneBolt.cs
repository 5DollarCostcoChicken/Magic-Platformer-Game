using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBolt : MonoBehaviour
{
    public GameObject creator;
    public string direction;
    public GameObject explosion;
    public void Begin()
    {
        this.GetComponent<Collider2D>().enabled = true;
        Vector3 boltVelocity = Vector3.zero;
        switch (direction)
        {
            case "up":
                boltVelocity = new Vector3(0, 24, 0);
                break;
            case "down":
                boltVelocity = new Vector3(0, -24, 0);
                break;
            case "left":
                boltVelocity = new Vector3(-24, 0, 0);
                break;
            case "right":
                boltVelocity = new Vector3(24, 0, 0);
                break;
        }
        GetComponent<Rigidbody2D>().velocity = boltVelocity;
        transform.Rotate(0, 0, Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) / (Mathf.PI / 180));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != creator)
        {
            if ((collision.tag.Equals("Player") || collision.tag.Equals("NPC")))
            {
                collision.GetComponent<Abilities>().SpellHit(6, 20, direction, true);
                GameObject explode = Instantiate(explosion);
                explode.transform.position = this.transform.position;
                explode.GetComponent<Animator>().Play("Base Layer.ArcaneExplosion");
                Destroy(this.gameObject);
            }
            else if (collision.tag.Equals("Environment"))
            {
                GameObject explode = Instantiate(explosion);
                explode.transform.position = this.transform.position;
                explode.GetComponent<Animator>().Play("Base Layer.ArcaneExplosion");
                Destroy(this.gameObject);
            }
            else if (collision.tag.Equals("Spell"))
            {
                collision.SendMessage("OnHit", true);
                GameObject explode = Instantiate(explosion);
                explode.transform.position = this.transform.position;
                explode.GetComponent<Animator>().Play("Base Layer.ArcaneExplosion");
                Destroy(this.gameObject);
            }
        }
    }
}
