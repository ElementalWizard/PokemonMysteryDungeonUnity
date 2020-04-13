using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{

    public int type; //0 is Floor 1 is wall 2 for stairs
    public SpriteRenderer sprite;
    public GameObject item = null;
    public GameObject pokemon = null;
    public FloorCreator.TileType tileType;


    //checks for connected textures
    public bool isT = false;
    public bool isD = false;
    public bool isL = false;
    public bool isR = false;

    public bool isTL = false;
    public bool isTR = false;
    public bool isBL = false;
    public bool isBR = false;

    //sprites for checks
    public Sprite bottomRight3;
    public Sprite bottomLeft3;
    public Sprite topRight3;
    public Sprite topLeft3;
    public Sprite[] bottomHalf5;
    public Sprite[] topHalf5;
    public Sprite[] leftHalf5;
    public Sprite[] rightHalf5;
    public Sprite[] fullySurrounded;

    public Sprite DR2;
    public Sprite LR2;
    public Sprite LD2;
    public Sprite UD2;
    public Sprite UR2;
    public Sprite LU2;
    public Sprite non;

    public Sprite D1;
    public Sprite R1;
    public Sprite U1;
    public Sprite L1;
    public Sprite LRUD4;

    public Sprite LRU3;
    public Sprite LRD3;
    public Sprite LUD3;
    public Sprite RUD3;

    public Sprite bottomHalfUp6;
    public Sprite topHalfD6;
    public Sprite leftHalfRight6;
    public Sprite rightHalfLeft5;

    public Sprite allButBR;
    public Sprite allButBL;
    public Sprite allButTR;
    public Sprite allButTL;

    public Sprite bottomRightUp4;
    public Sprite bottomLeftUp4;
    public Sprite topRightDown4;
    public Sprite topLeftDown4;

    public Sprite bottomRightLeft4;
    public Sprite bottomLeftRight4;
    public Sprite topRightLeft4;
    public Sprite topLeftRight4;

    public Sprite bottomRightupLeft5;
    public Sprite bottomLeftupRight5;
    public Sprite topRightbottomLeft5;
    public Sprite topLeftbottomRight5;

    public Sprite bottomRighttopLeft6;
    public Sprite bottomLefttopRight6;



    // Start is called before the first frame update
    void Start()
    {
        setSprite();

    }

    public void setSprite()
    {
        if(type != 2)
        {

        

            int x = (int)transform.localPosition.x;
            int y = (int)transform.localPosition.y;

            GameObject floor = GameObject.FindGameObjectWithTag("Dungeon") as GameObject;
            FloorCreator fc = floor.GetComponent<FloorCreator>();
            FloorCreator.TileType[][] tiles = fc.tiles;
            if (x < 0 || y < 0 || x >= fc.col || y >= fc.rows)
            {
                if (x == -1 && y > -1 && y < fc.rows)
                {
                    isT = true;
                    isTL = true;
                    isBL = true;
                    isL = true;
                    isD = true;
                    if (tiles[x + 1][y].Equals(tileType))
                    {
                        isR = true;
                    }


                    if (y == 0)
                    {
                        isBR = true;
                    }
                    else
                    {
                        if (tiles[x + 1][y - 1].Equals(tileType))
                        {
                            isBR = true;
                        }
                    }
                    if (y == fc.rows - 1)
                    {
                        isTR = true;
                    }
                    else
                    {
                        if (tiles[x + 1][y + 1].Equals(tileType))
                        {
                            isTR = true;
                        }
                    }
                }
                else if (x != -1 && y > -1 && y < fc.rows)
                {
                    isTR = true;
                    isBR = true;
                    isR = true;
                    isD = true;
                    isT = true;
                    if (tiles[x - 1][y].Equals(tileType))
                    {
                        isL = true;
                    }
                    if (y == 0)
                    {
                        isBL = true;
                    }
                    else
                    {
                        if (tiles[x - 1][y - 1].Equals(tileType))
                        {
                            isBL = true;
                        }
                    }
                    if (y == fc.rows - 1)
                    {
                        isTL = true;
                    }
                    else
                    {
                        if (tiles[x - 1][y + 1].Equals(tileType))
                        {
                            isTL = true;
                        }
                    }
                }
                if (y == -1 && x > -1 && x < fc.col)
                {
                    isL = true;
                    isBL = true;
                    isBR = true;
                    isR = true;
                    isD = true;
                    if (tiles[x][y + 1].Equals(tileType))
                    {
                        isT = true;
                    }
                    if (x == 0)
                    {
                        isTL = true;
                    }
                    else
                    {
                        if (tiles[x - 1][y + 1].Equals(tileType))
                        {
                            isTL = true;
                        }
                    }
                    if (x == fc.col - 1)
                    {
                        isTR = true;
                    }
                    else
                    {
                        if (tiles[x + 1][y + 1].Equals(tileType))
                        {
                            isTR = true;
                        }
                    }
                }
                else if (y != -1 && x > -1 && x < fc.col)
                {
                    isL = true;
                    isTL = true;
                    isTR = true;
                    isR = true;
                    isT = true;
                    if (tiles[x][y - 1].Equals(tileType))
                    {
                        isD = true;
                    }
                    if (x == 0)
                    {
                        isBL = true;
                    }
                    else
                    {
                        if (tiles[x - 1][y - 1].Equals(tileType))
                        {
                            isBL = true;
                        }
                    }
                    if (x == fc.col - 1)
                    {
                        isBR = true;
                    }
                    else
                    {
                        if (tiles[x + 1][y - 1].Equals(tileType))
                        {
                            isBR = true;
                        }
                    }
                }

                if (x == -1 && y == -1)
                {
                    isT = true;
                    isL = true;
                    isD = true;
                    isR = true;
                    isTL = true;
                    isBR = true;
                    isBL = true;
                    if (tiles[x + 1][y + 1].Equals(tileType))
                    {
                        isTR = true;
                    }
                }
                if (x == fc.col && y == -1)
                {
                    isT = true;
                    isL = true;
                    isD = true;
                    isR = true;
                    isTR = true;
                    isBR = true;
                    isBL = true;
                    if (tiles[x - 1][y + 1].Equals(tileType))
                    {
                        isTL = true;
                    }
                }
                if (x == fc.col && y == fc.rows)
                {
                    isT = true;
                    isL = true;
                    isD = true;
                    isR = true;
                    isTL = true;
                    isBR = true;
                    isTR = true;
                    if (tiles[x - 1][y - 1].Equals(tileType))
                    {
                        isBL = true;
                    }
                }
                if (x == -1 && y == fc.rows)
                {
                    isT = true;
                    isL = true;
                    isD = true;
                    isR = true;
                    isTL = true;
                    isBL = true;
                    isTR = true;
                    if (tiles[x + 1][y - 1].Equals(tileType))
                    {
                        isBR = true;
                    }
                }


                sprite.sprite = fullySurrounded[Random.Range(0, rightHalf5.Length)];
            }
            else
            {
                if (x < fc.col - 1 && tiles[x + 1][y].Equals(tileType))
                {
                    isR = true;
                }
                if (x > 0 && tiles[x - 1][y].Equals(tileType))
                {
                    isL = true;
                }
                if (y < fc.rows - 1 && tiles[x][y + 1].Equals(tileType))
                {
                    isT = true;
                }
                if (y > 0 && tiles[x][y - 1].Equals(tileType))
                {
                    isD = true;
                }

                if (x < fc.col - 1 && y < fc.rows - 1 && tiles[x + 1][y + 1].Equals(tileType))
                {
                    isTR = true;
                }
                if (x < fc.col - 1 && y > 0 && tiles[x + 1][y - 1].Equals(tileType))
                {
                    isBR = true;
                }
                if (x > 0 && y < fc.rows - 1 && tiles[x - 1][y + 1].Equals(tileType))
                {
                    isTL = true;
                }
                if (x > 0 && y > 0 && tiles[x - 1][y - 1].Equals(tileType))
                {
                    isBL = true;
                }

                if (x == 0)
                {
                    isL = true;
                    isTL = true;
                    isBL = true;
                }
                if (x == fc.col - 1)
                {
                    isR = true;
                    isTR = true;
                    isBR = true;
                }
                if (y == 0)
                {
                    isD = true;
                    isBL = true;
                    isBR = true;
                }
                if (y == fc.rows - 1)
                {
                    isT = true;
                    isTL = true;
                    isTR = true;
                }

            }





            //now we change sprite based on flag to trigger connected textures

            if (!isL && isR && isD && !isT && !isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomRight3;
            }
            else if (isL && isR && isD && !isT && isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomHalf5[Random.Range(0, bottomHalf5.Length)];
            }
            else if (isL && !isR && isD && !isT && isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomLeft3;
            }

            else if (!isL && isR && isD && isT && !isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = rightHalf5[Random.Range(0, rightHalf5.Length)];
            }
            else if (isL && isR && isD && isT && isBL && isBR && isTL && isTR)
            {
                sprite.sprite = fullySurrounded[Random.Range(0, fullySurrounded.Length)];
            }
            else if (isL && !isR && isD && isT && isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = leftHalf5[Random.Range(0, leftHalf5.Length)];
            }

            else if (!isL && isR && !isD && isT && !isBL && !isBR && !isTL && isTR)
            {
                sprite.sprite = topRight3;
            }
            else if (isL && isR && !isD && isT && !isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = topHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && !isD && isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = topLeft3;
            }
            else if (!isL && isR && isD && !isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = DR2;
            }
            else if (isL && isR && !isD && !isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = LR2;
            }
            else if (isL && !isR && isD && !isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = LD2;
            }
            else if (!isL && !isR && isD && isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = UD2;
            }
            else if (isL && isR && isD && isT && isBL && isBR && isTL && isTR)
            {
                sprite.sprite = non;
            }
            else if (!isL && isR && !isD && isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = UR2;
            }
            else if (isL && !isR && !isD && isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = LU2;
            }
            else if (!isL && !isR && isD && !isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = D1;
            }
            else if (!isL && isR && !isD && !isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = R1;
            }
            else if (isL && isR && isD && isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = LRUD4;
            }
            else if (isL && !isR && !isD && !isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = L1;
            }
            else if (!isL && !isR && !isD && isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = U1;
            }
            else if (isL && isR && isD && !isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = LRD3;
            }
            else if (!isL && isR && isD && isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = RUD3;
            }
            else if (isL && isR && !isD && isT && !isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = LRU3;
            }
            else if (isL && isR && isD && isT && !isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = topHalfD6;
            }
            else if (isL && isR && isD && isT && isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = leftHalfRight6;
            }
            else if (isL && isR && isD && isT && !isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = rightHalfLeft5;
            }
            else if (isL && isR && isD && isT && isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomHalfUp6;
            }
            else if (isL && isR && isD && isT && isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = allButBR;
            }
            else if (isL && isR && isD && isT && !isBL && isBR && isTL && isTR)
            {
                sprite.sprite = allButBL;
            }
            else if (isL && isR && isD && isT && isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = allButTR;
            }
            else if (isL && isR && isD && isT && isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = allButTL;
            }
            else if (!isL && isR && isD && isT && !isBL && !isBR && !isTL && isTR)
            {
                sprite.sprite = topRightDown4;
            }
            else if (isL && !isR && isD && isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = topLeftDown4;
            }
            else if (!isL && isR && isD && isT && !isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomRightUp4;
            }
            else if (isL && !isR && isD && isT && isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomLeftUp4;
            }
            else if (isL && isR && isD && !isT && isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomLeftRight4;
            }
            else if (isL && isR && isD && !isT && !isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomRightLeft4;
            }
            else if (isL && isR && !isD && isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = topLeftRight4;
            }
            else if (isL && isR && !isD && isT && !isBL && !isBR && !isTL && isTR)
            {
                sprite.sprite = topRightLeft4;
            }
            else if (isL && isR && isD && isT && !isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomRighttopLeft6;
            }
            else if (isL && isR && isD && isT && isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomLefttopRight6;
            }
            else if (isL && isR && isD && isT && !isBL && !isBR && !isTL && isTR)
            {
                sprite.sprite = topRightbottomLeft5;
            }
            else if (isL && isR && isD && isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = topLeftbottomRight5;
            }
            else if (isL && isR && isD && isT && isBL && !isBR && !isTL && isTR)
            {
                sprite.sprite = bottomLefttopRight6;
            }
            else if (isL && isR && isD && isT && !isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = bottomRighttopLeft6;
            }
            else if (isL && isR && isD && !isT && isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = bottomHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && isD && isT && isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = leftHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && isD && !isT && isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomLeft3;
            }
            else if (isL && isR && isD && !isT && isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = bottomHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && !isD && isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = bottomRight3;
            }
            else if (isL && !isR && !isD && isT && !isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = topLeft3;
            }
            else if (!isL && !isR && !isD && isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = topHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && !isD && !isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = L1;
            }
            else if (isL && !isR && isD && !isT && isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = bottomLeft3;
            }
            else if (!isL && isR && isD && isT && isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = rightHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && !isD && isT && isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = topLeft3;
            }
            else if (isL && isR && !isD && isT && !isBL && isBR && isTL && isTR)
            {
                sprite.sprite = topHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (!isL && isR && isD && isT && !isBL && isBR && isTL && isTR)
            {
                sprite.sprite = rightHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && isR && !isD && isT && isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = topHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && isR && isD && !isT && isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = bottomHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (!isL && isR && isD && !isT && isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = bottomRight3;
            }
            else if (isL && isR && isD && !isT && isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = bottomHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && isD && isT && isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = leftHalf5[Random.Range(0, topHalf5.Length)];
            }
            else if (isL && !isR && !isD && isT && isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = topLeft3;
            }
            else if (!isL && isR && !isD && isT && !isBL && isBR && isTL && isTR)
            {
                sprite.sprite = topRight3;
            }
            else if (!isL && isR && !isD && isT && !isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = topRight3;
            }
            else if (!isL && isR && isD && !isT && isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = bottomRight3;
            }
            else if (!isL && isR && isD && !isT && !isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = bottomRight3;
            }
            else if (isL && !isR && !isD && isT && !isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = topLeft3;
            }
            else if (!isL && isR && !isD && isT && !isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = topRight3;
            }
            else if (isL && isR && isD && !isT && isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = U1;
            }
            else if (isL && isR && !isD && isT && !isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = D1;
            }
            else if (!isL && isR && isD && isT && !isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = L1;
            }
            else if (isL && !isR && isD && isT && isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = R1;
            }
            else if (isL && !isR && !isD && !isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = L1;
            }
            else if (!isL && isR && !isD && isT && !isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = topRight3;
            }
            else if (!isL && !isR && isD && isT && !isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = UD2;
            }
            else if (!isL && !isR && isD && !isT && !isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = D1;
            }
            else if (isL && !isR && isD && !isT && isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = bottomLeft3;
            }
            else if (isL && isR && !isD && !isT && !isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = LR2;
            }
            else if (isL && isR && !isD && !isT && !isBL && !isBR && !isTL && isTR)
            {
                sprite.sprite = LR2;
            }
            else if (isL && isR && !isD && !isT && !isBL && isBR && !isTL && !isTR)
            {
                sprite.sprite = LR2;
            }
            else if (isL && isR && isD && !isT && !isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = LRD3;
            }
            else if (isL && !isR && isD && isT && isBL && !isBR && isTL && isTR)
            {
                sprite.sprite = LR2;
            }
            else if (isL && isR && !isD && !isT && isBL && !isBR && !isTL && !isTR)
            {
                sprite.sprite = LR2;
            }
            else if (isL && isR && !isD && isT && !isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = LRU3;
            }
            else if (isL && isR && isD && !isT && !isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = LRD3;
            }
            else if (!isL && isR && !isD && !isT && !isBL && isBR && !isTL && isTR)
            {
                sprite.sprite = R1;
            }
            else if (isL && isR && !isD && !isT && !isBL && isBR && isTL && !isTR)
            {
                sprite.sprite = LR2;
            }
            else if (isL && isR && !isD && isT && isBL && !isBR && isTL && !isTR)
            {
                sprite.sprite = LRU3;
            }
            else if (!isL && isR && !isD && !isT && !isBL && !isBR && !isTL && isTR)
            {
                sprite.sprite = R1;
            }
        }

    }

    public bool hasItem()
    {
        return item!=null;
    }

    public bool hasPokemon()
    {
        return pokemon !=null;
    }

    public bool isWall()
    {
        return type == 1 || tileType == FloorCreator.TileType.Wall;
    }

    public bool isStairs()
    {
        return type == 2 && tileType == FloorCreator.TileType.Floor;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
    }
}
