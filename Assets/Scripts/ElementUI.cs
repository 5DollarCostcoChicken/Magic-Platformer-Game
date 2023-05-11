using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ElementUI : NetworkBehaviour
{
    public PlayerMovementController player;
    public GameObject castingCircle;
    string direction = "Center";
    string directionMode = "Cardinal";
    public Sprite[] castingDirectionals = new Sprite[6];
    public Texture[] runeSprites = new Texture[14];
    public GameObject[] runes = new GameObject[6];
    public GameObject elementSymbol;
    public GameObject[] greyDirections = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
    }

    // Update is called once per frame
    void Update()
    {
        updateCastingDirection();
        updateElement();
    }
    void updateCastingDirection()
    {
        if (player.hasAuthority && !player.CurrentElements.Equals("None"))
        {
            switch (direction)
            {
                case "Center":
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha7)) && !directionMode.Equals("Light"))
                    {
                        direction = "Left";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[2];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha9)) && !directionMode.Equals("Light"))
                    {
                        direction = "Right";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[3];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha0)) && !directionMode.Equals("Horizontal"))
                    {
                        direction = "Up";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[4];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha8)) && !directionMode.Equals("Light") && !directionMode.Equals("Horizontal") && !directionMode.Equals("NoDown"))
                    {
                        direction = "Down";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[5];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    break;
                case "Left":
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha7)) && !directionMode.Equals("Light"))
                    {
                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha9)) && !directionMode.Equals("Light"))
                    {
                        direction = "Right";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[3];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha0)) && !directionMode.Equals("Horizontal"))
                    {
                        direction = "Up";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[4];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha8)) && !directionMode.Equals("Light") && !directionMode.Equals("Horizontal") && !directionMode.Equals("NoDown"))
                    {
                        direction = "Down";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[5];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    break;
                case "Right":
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha7)) && !directionMode.Equals("Light"))
                    {
                        direction = "Left";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[2];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha9)) && !directionMode.Equals("Light"))
                    {
                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha0)) && !directionMode.Equals("Horizontal"))
                    {
                        direction = "Up";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[4];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha8)) && !directionMode.Equals("Light") && !directionMode.Equals("Horizontal") && !directionMode.Equals("NoDown"))
                    {
                        direction = "Down";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[5];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    break;
                case "Up":
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha7)) && !directionMode.Equals("Light"))
                    {
                        direction = "Left";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[2];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha9)) && !directionMode.Equals("Light"))
                    {
                        direction = "Right";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[3];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha0)) && !directionMode.Equals("Horizontal"))
                    {
                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha8)) && !directionMode.Equals("Light") && !directionMode.Equals("Horizontal") && !directionMode.Equals("NoDown"))
                    {
                        direction = "Down";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[5];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    break;
                case "Down":
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha7)) && !directionMode.Equals("Light"))
                    {
                        direction = "Left";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[2];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha9)) && !directionMode.Equals("Light"))
                    {
                        direction = "Right";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[3];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha0)) && !directionMode.Equals("Horizontal"))
                    {
                        direction = "Up";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[4];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha8)) && !directionMode.Equals("Light") && !directionMode.Equals("Horizontal") && !directionMode.Equals("NoDown"))
                    {
                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                    }
                    break;
            }
        }
    }
    void updateElement()
    {
        if (player.hasAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !player.CurrentElements.Equals("None"))
            {
                foreach(GameObject rune in runes)
                {
                    if (rune.GetComponent<RawImage>().texture == runeSprites[1])
                    {
                        StartCoroutine(ButtonPulse(rune));
                        rune.GetComponent<RawImage>().texture = runeSprites[0];
                    }
                }
                StartCoroutine(ButtonPulse(castingCircle));
                castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
                player.abilities.SendMessage(player.CurrentElements + direction);
                player.CurrentElements = "None";
                greyDirections[0].SetActive(false);
                greyDirections[1].SetActive(false);
                greyDirections[2].SetActive(false);
                greyDirections[3].SetActive(false);
                elementSymbol.GetComponent<RawImage>().enabled = false;
            }
            switch (player.CurrentElements)
            {
                //None
                case "None":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];

                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];

                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];

                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];

                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];

                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];

                        direction = "Center";
                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
                        StartCoroutine(ButtonPulse(castingCircle));
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Earth
                case "Earth":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "None";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];

                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fireballs";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Nature";
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Ice
                case "Ice":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "None";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];

                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Water";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Icicles";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Fire
                case "Fire":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Fireballs";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Water";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "None";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];

                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Arcane
                case "Arcane":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Icicles";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "None";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];

                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "DarkMagic";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Lightning
                case "Lightning":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "DarkMagic";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "None";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];

                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Healing";
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Light
                case "Light":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Nature";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Healing";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "None";
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];

                        castingCircle.GetComponent<Image>().sprite = castingDirectionals[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //
                //
                //
                //Nature
                case "Nature":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Fireballs
                case "Fireballs":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Water
                case "Water":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Icicles
                case "Icicles":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //DarkMagic
                case "DarkMagic":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
                //Healing
                case "Healing":
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        player.CurrentElements = "Earth";
                        StartCoroutine(ButtonPulse(runes[0]));
                        runes[0].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        player.CurrentElements = "Ice";
                        StartCoroutine(ButtonPulse(runes[1]));
                        runes[1].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        player.CurrentElements = "Fire";
                        StartCoroutine(ButtonPulse(runes[2]));
                        runes[2].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        player.CurrentElements = "Arcane";
                        StartCoroutine(ButtonPulse(runes[3]));
                        runes[3].GetComponent<RawImage>().texture = runeSprites[1];
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        player.CurrentElements = "Light";
                        StartCoroutine(ButtonPulse(runes[4]));
                        runes[4].GetComponent<RawImage>().texture = runeSprites[0];
                        StartCoroutine(ElementSymbolPulse());
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        player.CurrentElements = "Lightning";
                        StartCoroutine(ButtonPulse(runes[5]));
                        runes[5].GetComponent<RawImage>().texture = runeSprites[0];

                        StartCoroutine(ElementSymbolPulse());
                    }
                    break;
            }
        }
    }
    void updateElementDirection()
    {
        if (player.CurrentElements.Equals("Ice") || player.CurrentElements.Equals("Lightning"))
        {
            directionMode = "NoDown";
            greyDirections[0].SetActive(false);
            greyDirections[1].SetActive(false);
            greyDirections[2].SetActive(true);
            greyDirections[3].SetActive(false);
            if (direction.Equals("Down"))
            {
                direction = "Center";
                castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
            }
        }
        else if (player.CurrentElements.Equals("Light"))
        {
            directionMode = "Light";
            greyDirections[0].SetActive(false);
            greyDirections[1].SetActive(true);
            greyDirections[2].SetActive(true);
            greyDirections[3].SetActive(true);
            if (direction.Equals("Down") || direction.Equals("Left") || direction.Equals("Right"))
            {
                direction = "Center";
                castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
            }
        }
        else if (player.CurrentElements.Equals("Water") || player.CurrentElements.Equals("Healing"))
        {
            directionMode = "Horizontal";
            greyDirections[0].SetActive(true);
            greyDirections[1].SetActive(false);
            greyDirections[2].SetActive(true);
            greyDirections[3].SetActive(false);
            if (direction.Equals("Down") || direction.Equals("Up"))
            {
                direction = "Center";
                castingCircle.GetComponent<Image>().sprite = castingDirectionals[1];
            }
        }
        else
        {
            directionMode = "Cardinal";
            greyDirections[0].SetActive(false);
            greyDirections[1].SetActive(false);
            greyDirections[2].SetActive(false);
            greyDirections[3].SetActive(false);
        }
    }
    IEnumerator ButtonPulse(GameObject button)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.01f);
            button.transform.localScale += new Vector3(.03f, .03f, .03f);
        }
        for (int i = 0; i < 9; i++)
        {
            yield return new WaitForSeconds(.01f);
            button.transform.localScale -= new Vector3(.01f, .01f, .01f);
        }
    }
    IEnumerator ElementSymbolPulse()
    {
        updateElementDirection();
        if (!player.CurrentElements.Equals("None"))
        {
            foreach (Texture sprite in runeSprites)
            {
                if (sprite.name.Equals(player.CurrentElements))
                {
                    elementSymbol.GetComponent<RawImage>().texture = sprite;
                }
            }
            elementSymbol.GetComponent<RawImage>().enabled = true;
        }
        else
        {
            elementSymbol.GetComponent<RawImage>().enabled = false;
        }
        if (player.CurrentElements.Equals("Nature") || player.CurrentElements.Equals("Fireballs") || player.CurrentElements.Equals("Water") || player.CurrentElements.Equals("Icicles") || player.CurrentElements.Equals("DarkMagic") || player.CurrentElements.Equals("Healing")) {
            elementSymbol.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
            for (int i = 0; i < 15; i++)
            {
                yield return new WaitForSeconds(.01f);
                elementSymbol.transform.localScale -= new Vector3(.1f, .1f, .1f);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(.01f);
                elementSymbol.transform.localScale += new Vector3(.03f, .03f, .03f);
            }
            for (int i = 0; i < 9; i++)
            {
                yield return new WaitForSeconds(.01f);
                elementSymbol.transform.localScale -= new Vector3(.01f, .01f, .01f);
            }
        }
    }
}
