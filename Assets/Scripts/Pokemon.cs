using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Pokemon : MonoBehaviour
{
    public string pokeName;
    public int dexNum;
    public bool up=false, down=false, left=false, right=false;
    public bool Oup=false, Odown=false, Oleft=false, Oright=false;
    public int moveX = 0,moveY = 0;
    public float speed = 1;
    public int Pspeed = 1;
    public bool isEnemy;
    public bool isNPC;
    public bool isPlayer = false;
    public bool inDungeon;
    public Animator pokeAnimator;
    public FloorCreator fc;
    public Item itemOnHand = null;
    public int turn;
    public float health;
    public float Maxhealth;
    public int level = 1;

    public bool passTurn = false;
    public bool attacking = false;
    public bool moving = false;
    



    public float elapsed = 0f;
    public float timebetweenTurns = .3f;//1 is a second

    public TMP_Text Level;
    public TMP_Text Floor;
    public TMP_Text HP;
    public Slider HPBar;

    // Start is called before the first frame update
    void Start()
    {
        Maxhealth = health;
        pokeAnimator = GetComponent<Animator>();
        down = true;
    }

    private void Update()
    {
        if (fc.turn==turn)
        {
            if (inDungeon)
            {

                if (passTurn)
                {

                    passTurn = false;
                    moving = false;
                    attacking = false;
                    fc.increaseTurn();
                    return;
                    
                    
                }
                else if (isPlayer && !attacking && !moving)
                {
                    Level.text = "Lv." + level;
                    Floor.text = fc.floor + "F";
                    HP.text = "HP: " + health + "  /  " + Maxhealth;
                    HPBar.value = (health / Maxhealth);

                    int moveX = 0;
                    int moveY = 0;

                    if (Input.GetKey(KeyCode.W))
                    {
                        moveY++;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        moveX--;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        moveY--;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        moveX++;
                    }
                    int newX = Mathf.RoundToInt(moveX + transform.localPosition.x);
                    int newY = Mathf.RoundToInt(moveY + transform.localPosition.y);

                    if (Input.GetKey(KeyCode.F))
                    {
                        if (moveX == 0 && moveY == 0)
                        {
                            if (up)
                            {
                                moveY++;
                            }
                            if (left)
                            {
                                moveX--;
                            }
                            if (down)
                            {
                                moveY--;
                            }
                            if (right)
                            {
                                moveX++;
                            }
                        }
                         newX = Mathf.RoundToInt(moveX + transform.localPosition.x);
                         newY = Mathf.RoundToInt(moveY + transform.localPosition.y);
                        Debug.Log(newX);
                        Debug.Log(newY);
                        Debug.Log("");
                        if (newX >= 0 && newY >= 0 && newX < fc.col && newY < fc.rows)
                        {

                            StartCoroutine(AttackPokemon(moveX, moveY));
                            if (fc.tilesInfo[newX][newY] != null && fc.tilesInfo[newX][newY].hasPokemon())
                            {
                                fc.tilesInfo[newX][newY].pokemon.GetComponent<Pokemon>().damage(4, moveX * -1, moveY * -1);
                            }
                        }


                    }
                    else
                    {
                        if(moveX == 0 && moveY == 0)
                        {
                            return;
                        }
                        if(newX >= 0 && newY >= 0 && newX < fc.col && newY < fc.rows)
                        {
                            TileInfo info = fc.tilesInfo[newX][newY];
                            if(info !=null && !info.hasPokemon() && !info.isWall())
                            {
                                StartCoroutine(MovePokemon(moveX, moveY));
                            }
                        }
                    }


                }
                else if (isEnemy && !attacking && !moving)
                {
                    int moveX = 0;
                    int moveY = 0;
                    int newX = Mathf.RoundToInt(moveX + transform.localPosition.x);
                    int newY = Mathf.RoundToInt(moveY + transform.localPosition.y);

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            moveX = i;
                            moveY = j;
                             newX = Mathf.RoundToInt(moveX + transform.localPosition.x);
                             newY = Mathf.RoundToInt(moveY + transform.localPosition.y);
                            if (newX >= 0 && newY >= 0 && newX < fc.col && newY < fc.rows)
                            {
                                if(fc.tilesInfo[newX][newY] != null && fc.tilesInfo[newX][newY].hasPokemon() && fc.tilesInfo[newX][newY].pokemon.GetComponent<Pokemon>().isPlayer)
                                {
                                    StartCoroutine(AttackPokemon(moveX, moveY));
                                    fc.tilesInfo[newX][newY].pokemon.GetComponent<Pokemon>().damage(4,moveX*-1,moveY*-1);
                                    return;
                                }
                            }

                        }
                    }
                    moveX = UnityEngine.Random.Range(-1, 2);
                    moveY = UnityEngine.Random.Range(-1, 2);
                    newX = Mathf.RoundToInt(moveX + transform.localPosition.x);
                    newY = Mathf.RoundToInt(moveY + transform.localPosition.y);
                    int counter = 0;
                    while(counter<=18 && ((newX < 0 && newY < 0 && newX >= fc.col && newY >= fc.rows) || fc.tilesInfo[newX][newY].hasPokemon() || fc.tilesInfo[newX][newY].isWall()) )
                    {
                        counter++;
                        moveX = UnityEngine.Random.Range(-1, 2);
                        moveY = UnityEngine.Random.Range(-1, 2);
                        newX = Mathf.RoundToInt(moveX + transform.localPosition.x);
                        newY = Mathf.RoundToInt(moveY + transform.localPosition.y);
                    }
                    if (counter <= 18)
                    {
                        StartCoroutine(MovePokemon(moveX, moveY));
                    }
                    else
                    {
                        passTurn = true;
                    }
                }
            }
        }
    }

    IEnumerator MovePokemon(int nextX,int nextY)
    {
        int newX = Mathf.RoundToInt(transform.localPosition.x);
        int newY = Mathf.RoundToInt(transform.localPosition.y);
        TileInfo nexttileinfo = fc.tilesInfo[newX][newY];
        nexttileinfo.pokemon = null;

        newX = Mathf.RoundToInt(transform.localPosition.x + nextX);
        newY = Mathf.RoundToInt(transform.localPosition.y + nextY);
        nexttileinfo = fc.tilesInfo[newX][newY];

        nexttileinfo.pokemon = this.gameObject;
        if (nexttileinfo.hasItem() && itemOnHand == null)
        {
            itemOnHand = nexttileinfo.item.GetComponent<Item>();
            nexttileinfo.item.active = false;
            nexttileinfo.item = null;
        }
        up = false;
        down = false;
        left = false;
        right = false;
        if (nextX > 0)
        {
            right = true;
        }
        if (nextX < 0)
        {
            left = true;
        }
        if (nextY > 0)
        {
            up = true;
        }
        if (nextY < 0)
        {
            down = true;
        }

        pokeAnimator.SetBool("Left", left);
        pokeAnimator.SetBool("Right", right);
        pokeAnimator.SetBool("Up", up);
        pokeAnimator.SetBool("Down", down);
        pokeAnimator.SetTrigger("Walk");
        moving = true;

        yield return new WaitForSeconds(0.3f);

        if (fc.tilesInfo[newX][newY].isStairs() && isPlayer)
        {
            fc.nextFloor();
            moving = false;
        }
        else
        {
            passTurn = true;

        }

    }

    IEnumerator AttackPokemon(int nextX, int nextY)
    {
        transform.localPosition.Set(transform.localPosition.x, transform.localPosition.y, -1.3f);
        up = false;
        down = false;
        left = false;
        right = false;
        if (nextX > 0)
        {
            right = true;
        }
        if (nextX < 0)
        {
            left = true;
        }
        if (nextY > 0)
        {
            up = true;
        }
        if (nextY < 0)
        {
            down = true;
        }

        pokeAnimator.SetBool("Left", left);
        pokeAnimator.SetBool("Right", right);
        pokeAnimator.SetBool("Up", up);
        pokeAnimator.SetBool("Down", down);
        pokeAnimator.SetTrigger("Attack");

        attacking = true;

        yield return new WaitForSeconds(0.8f);
        passTurn = true;
        transform.localPosition.Set(transform.localPosition.x, transform.localPosition.y, -1f);


    }

    public void damage(int v,int nextX, int nextY)
    {
        health -= v;
        up = false;
        down = false;
        left = false;
        right = false;
        if (nextX > 0)
        {
            right = true;
        }
        if (nextX < 0)
        {
            left = true;
        }
        if (nextY > 0)
        {
            up = true;
        }
        if (nextY < 0)
        {
            down = true;
        }

        pokeAnimator.SetBool("Left", left);
        pokeAnimator.SetBool("Right", right);
        pokeAnimator.SetBool("Up", up);
        pokeAnimator.SetBool("Down", down);
        pokeAnimator.SetTrigger("Attacked");
    }

  
}
