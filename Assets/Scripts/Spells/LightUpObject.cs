using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpObject : MonoBehaviour
{
    public GameObject explosion;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(Time.time * 200), 0), ForceMode2D.Force);
    }
    public void OnHit(bool magicalDmg)
    {
        GameObject explode = Instantiate(explosion);
        explode.transform.position = this.transform.position;
        explode.GetComponent<Animator>().Play("Base Layer.LightExplosion");
        Destroy(this.gameObject);
    }
}
