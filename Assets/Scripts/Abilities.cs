using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Abilities : NetworkBehaviour
{
    [Header("Stats")]
    public int maxHealth;
    [SyncVar] public int health;
    [SyncVar] public bool animating = false;
    [SyncVar] public bool stopMovement = false;
    
    [Header("Animators")]
    public Animator head;
    public Animator body;
    public Animator arms;

    [Header("UI Elements")]
    public GameObject UIShit;
    public GameObject MeleeSwingHorizontal;
    public GameObject bloodParticles;
    public TextMesh healthText;
    public GameObject HealthbarPivot;
    public SpriteRenderer HealthbarSprite;

    string meleeState = "up";
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    GameObject target;

    [Header("Spell Objects")]
    public GameObject LightUpOrb;
    public GameObject ArcaneBolt;
    void Start()
    {
        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.yellow;
        colorKey[1].time = .5f;
        colorKey[2].color = Color.green;
        colorKey[2].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }
    private void Update()
    {
        if (health < 0)
        {
            health = 0;
        }
        HealthbarSprite.color = gradient.Evaluate((float)health / maxHealth);
        HealthbarPivot.transform.localScale = new Vector3((float)health / maxHealth * 4.509876f, HealthbarPivot.transform.localScale.y, HealthbarPivot.transform.localScale.z);
        healthText.text = "" + health;
    }
    //Try and figure out how to make Melee Specifically collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Environment") || collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Spell"))
        {
            int force = 1;
            if (UIShit.transform.localScale.x > 0)
                force *= -1;
            if (collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Spell"))
            {
                force *= 2;
                target = collision.gameObject;
                NPCCollide(force, 5);
            }
            else
                force *= 5;
            if (meleeState.Equals("horizontal"))
                GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
            else if (meleeState.Equals("up"))
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force) * -1), ForceMode2D.Impulse);
            else if (meleeState.Equals("down"))
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force)), ForceMode2D.Impulse);
        }
    }
    public void NPCCollide(int force, int damage)
    {
        if (target.tag.Equals("Player"))
        {
            if (isServer)
            {
                RpcNPCCollide(force, damage);
            }
            else
            {
                CmdNPCCollide(force, damage);
            }
        }
        else
        {
            if (meleeState.Equals("horizontal"))
                target.GetComponent<Rigidbody2D>().AddForce(new Vector2(force * -1, 3), ForceMode2D.Impulse);
            else if (meleeState.Equals("up"))
                target.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force) * 2), ForceMode2D.Impulse);
            else if (meleeState.Equals("down"))
                target.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force) * -1), ForceMode2D.Impulse);

            if (target.GetComponent<Abilities>() != null)
            {
                target.SendMessage("OnHit", damage);
            }
            target = null;
        }
    }
    [ClientRpc]
    private void RpcNPCCollide(int force, int damage)
    {

        if (target.GetComponent<Abilities>() != null)
        {
            if (isServer)
            {
                target.GetComponent<Abilities>().RpcOnHit(damage, force, meleeState);
            }
            else
            {
                target.GetComponent<Abilities>().CmdOnHit(damage, force, meleeState);
            }
        }
        target = null;
    }
    [Command]
    private void CmdNPCCollide(int force, int damage)
    {
        RpcNPCCollide(force, damage);
        if (target.GetComponent<Abilities>() != null)
        {
            if (isServer)
            {
                target.GetComponent<Abilities>().RpcOnHit(damage, force, meleeState);
            }
            else
            {
                target.GetComponent<Abilities>().CmdOnHit(damage, force, meleeState);
            }
        }
        target = null;
    }
    public void SpellHit(int force, int damage, string direction, bool magicalDamage)
    {
        switch (direction)
        {
            case "up":
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
                break;
            case "left":
                GetComponent<Rigidbody2D>().AddForce(new Vector2(force * -1, 3), ForceMode2D.Impulse);
                break;
            case "right":
                GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 3), ForceMode2D.Impulse);
                break;
        }

        SendMessage("OnHit", damage);
    }
    public void OnHit(int damage)
    {
        health -= damage;
        StartCoroutine(TakeDamage());
    }
    [ClientRpc]
    private void RpcOnHit(int damage, int force, string direction)
    {
        health -= damage;
        if (direction.Equals("horizontal"))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(force * -1, 3), ForceMode2D.Impulse);
        else if (direction.Equals("up"))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force) * 2), ForceMode2D.Impulse);
        else if (direction.Equals("down"))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force) * -1), ForceMode2D.Impulse);
        StartCoroutine(TakeDamage());
    }
    [Command]
    private void CmdOnHit(int damage, int force, string direction)
    {
        RpcOnHit(damage, force, direction);
        health -= damage;
        if (direction.Equals("horizontal"))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(force * -1, 3), ForceMode2D.Impulse);
        else if (direction.Equals("up"))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force) * 2), ForceMode2D.Impulse);
        else if (direction.Equals("down"))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Abs(force) * -1), ForceMode2D.Impulse);
        StartCoroutine(TakeDamage());
    }
    private IEnumerator TakeDamage()
    {
        animating = true;
        GameObject blood = NetworkIdentity.Instantiate(bloodParticles, this.transform);
        blood.transform.position = this.transform.position;
        body.Play("Base Layer.Hit");
        head.Play("Base Layer.Up");
        yield return new WaitForSeconds(.2f);
        animating = false;
        yield return new WaitForSeconds(.1f);
        Destroy(blood.gameObject);
    }
    [ClientRpc]
    public void RpcMeleeHorizontal()
    {
        meleeState = "horizontal";
        StartCoroutine(Melee());
    }
    [ClientRpc]
    public void RpcMeleeUp()
    {
        meleeState = "up";
        StartCoroutine(Melee());
    }
    [ClientRpc]
    public void RpcMeleeDown()
    {
        meleeState = "down";
        StartCoroutine(Melee());
    }
    [Command]
    public void CmdMeleeHorizontal()
    {
        meleeState = "horizontal";
        StartCoroutine(Melee());
    }
    [Command]
    public void CmdMeleeUp()
    {
        meleeState = "up";
        StartCoroutine(Melee());
    }
    [Command]
    public void CmdMeleeDown()
    {
        meleeState = "down";
        StartCoroutine(Melee());
    }
    public IEnumerator Melee()
    {
        int force = 1;
        if (UIShit.transform.localScale.x < 0)
            force *= -1;
        animating = true;
        MeleeSwingHorizontal.GetComponent<BoxCollider2D>().enabled = true;
        body.Play("Base Layer.Idle");
        if (meleeState.Equals("up"))
        {
            head.Play("Base Layer.Up");
            MeleeSwingHorizontal.transform.RotateAround(body.transform.position + Vector3.left * .2f * force, new Vector3(0, 0, 1), 90 * force);
        }
        else if (meleeState.Equals("down"))
        {
            head.Play("Base Layer.Down");
            MeleeSwingHorizontal.transform.RotateAround(body.transform.position + (Vector3.left * .2f * force) + Vector3.down * .3f, new Vector3(0, 0, 1), -90 * force);
        }
        if (GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            body.Play("Base Layer.AttackHorizontalFalling");
            arms.Play("Base Layer.SwordHorizontal");
        }
        else
        {
            if (Input.GetAxis("Horizontal") == 0)
            {
                body.Play("Base Layer.AttackHorizontal");
                arms.Play("Base Layer.SwordHorizontal");
            }
            else
            {
                body.Play("Base Layer.AttackHorizontalSprinting");
                arms.Play("Base Layer.SwordHorizontalSprinting");
            }
        }
        yield return new WaitForSeconds(.1f);
        MeleeSwingHorizontal.SetActive(true);
        MeleeSwingHorizontal.GetComponent<Animator>().Play("Base Layer.Swing");
        yield return new WaitForSeconds(.1f);
        MeleeSwingHorizontal.GetComponent<Animator>().Play("Base Layer.Blank");
        MeleeSwingHorizontal.SetActive(false);
        arms.Play("Base Layer.Blank");
        MeleeSwingHorizontal.GetComponent<BoxCollider2D>().enabled = false;
        if (meleeState.Equals("up"))
        {
            head.Play("Base Layer.Forward");
            MeleeSwingHorizontal.transform.RotateAround(body.transform.position + Vector3.left * .2f * force, new Vector3(0, 0, 1), -90 * force);
        }
        else if (meleeState.Equals("down"))
        {
            head.Play("Base Layer.Forward");
            MeleeSwingHorizontal.transform.RotateAround(body.transform.position + (Vector3.left * .2f * force) + Vector3.down * .3f, new Vector3(0, 0, 1), 90 * force);
        }
        MeleeSwingHorizontal.transform.localPosition = new Vector3(0.07f, -0.01f, 1);
        MeleeSwingHorizontal.transform.rotation = new Quaternion(0,0,0,0);
        animating = false;
    }
    // / / / / / / / / / / / / / / / / / / / / / / / / / / / Spells / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
    
    //Light
    public void LightUp()
    {
        if (isServer)
        {
            RpcLightUp();
        }
        else
        {
            CmdLightUp();
        }
    }
    [ClientRpc]
    private void RpcLightUp()
    {
        StartCoroutine(LightUpRoutine());
    }
    [Command]
    private void CmdLightUp()
    {
        RpcLightUp();
        StartCoroutine(LightUpRoutine());
    }
    public IEnumerator LightUpRoutine()
    {
        animating = true;
        stopMovement = true;
        body.Play("Base Layer.LightUp");
        head.Play("Base Layer.Down");
        yield return new WaitForSeconds(.2f);
        head.Play("Base Layer.Forward");
        GameObject orb = Instantiate(LightUpOrb);
        orb.transform.position = this.transform.position + Vector3.up * .1f;
        yield return new WaitForSeconds(.1f);
        head.Play("Base Layer.Up");
        yield return new WaitForSeconds(.2f);
        head.Play("Base Layer.Forward");
        animating = false;
        stopMovement = false;
    }

    //Arcane
    public void ArcaneUp()
    {
        if (isServer)
        {
            RpcArcaneCardinal("up");
        }
        else
        {
            CmdArcaneCardinal("up");
        }
    }
    public void ArcaneDown()
    {
        if (isServer)
        {
            RpcArcaneCardinal("down");
        }
        else
        {
            CmdArcaneCardinal("down");
        }
    }
    public void ArcaneLeft()
    {
        if (isServer)
        {
            RpcArcaneCardinal("left");
        }
        else
        {
            CmdArcaneCardinal("left");
        }
    }
    public void ArcaneRight()
    {
        if (isServer)
        {
            RpcArcaneCardinal("right");
        }
        else
        {
            CmdArcaneCardinal("right");
        }
    }
    [ClientRpc]
    private void RpcArcaneCardinal(string direction)
    {
        StartCoroutine(ArcaneCardinal(direction));
    }
    [Command]
    private void CmdArcaneCardinal(string direction)
    {
        RpcArcaneCardinal(direction);
        StartCoroutine(ArcaneCardinal(direction));
    }
    public IEnumerator ArcaneCardinal(string direction)
    {
        animating = true;
        if (GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            body.Play("Base Layer.AttackHorizontalFalling");
            arms.Play("Base Layer.ArcaneWindup");
        }
        else
        {
            if (Input.GetAxis("Horizontal") == 0)
            {
                body.Play("Base Layer.AttackHorizontal");
                arms.Play("Base Layer.ArcaneWindup");
            }
            else
            {
                body.Play("Base Layer.AttackHorizontalSprinting");
                arms.Play("Base Layer.ArcaneWindup");
            }
        }
        yield return new WaitForSeconds(.2f);
        GameObject bolt = NetworkIdentity.Instantiate(ArcaneBolt);
        bolt.transform.position = this.transform.position + Vector3.up * -.1f;
        bolt.GetComponent<ArcaneBolt>().direction = direction;
        bolt.GetComponent<ArcaneBolt>().creator = this.gameObject;
        bolt.GetComponent<ArcaneBolt>().Begin();
        yield return new WaitForSeconds(.1f);
        head.Play("Base Layer.Forward");
        arms.Play("Base Layer.Blank");
        animating = false;
    }
}
