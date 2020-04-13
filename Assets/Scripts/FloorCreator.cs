using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FloorCreator : MonoBehaviour
{ 
    [Serializable]
    public enum TileType
{
    Wall,Floor,
}
     
    public int col = 100;
    public int rows = 100;
    public IntRange numRooms = new IntRange (15, 20);
    public IntRange roomWidth = new IntRange (3, 10);
    public IntRange roomHeight = new IntRange (3, 10);
    public IntRange hallLenght = new IntRange (6, 10);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public bool someOneAttacking = false;

    public GameObject stairs;

    public IntRange pokemons = new IntRange(3, 5);
    public IntRange items = new IntRange(3, 10);

    public List<GameObject> pokemonsInRoom;
    public List<GameObject> itemsInRoom;
    public GameObject[] itemsSpanableInRoom;
    public GameObject[] pokemonSpanableInRoom;


    public TileType[][] tiles;
    public TileInfo[][] tilesInfo;
    public Room[] rooms;
    public Hall[] halls;

    public GameObject floorHolder;
    public GameObject pokemonHolder;
    public GameObject teamHolder;
    public GameObject itemHolder;


    public Camera cam;
    public Vector3 offsetCam;


    public int turn =-1;
    public int maxTurns =-1;

    public GameObject[] team;
   

    float elapsed = 0f;
    float timePerTurn = 0.1f;//1 is a second

    public bool L = false;

    public TMP_Text Level;
    public TMP_Text Floor;
    public TMP_Text HP;
    public Slider HPBar;

    public int floor = 1;

    // Start is called before the first frame update
    void Start()
    {
        floorHolder = new GameObject("FloorHolder");
        floorHolder.tag = "Floor";

        SetupTilesArray();
        CreateRoomsAndHalls();

        SetTilesValuesForRooms();
        SetTilesValuesForHalls();

        InstantiateTiles();
        InstantiateOuterWalls();



        foreach (Transform child in floorHolder.transform)
        {
           TileInfo info = child.gameObject.GetComponent<TileInfo>();
           info.setSprite();
            if (((int)(child.localPosition.x) >= 0 &&
                                    (int)(child.localPosition.y) >= 0 &&
                                    (int)(child.localPosition.x ) < col &&
                                    (int)(child.localPosition.y) < rows))
            {

                    tilesInfo[(int)(child.localPosition.x)][(int)(child.localPosition.y)] = info;

                
            }


        }


        int randRoom = UnityEngine.Random.Range(0, rooms.Length);
        int randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
        int randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

        bool isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);
        while (isWall)
        {
            randRoom = UnityEngine.Random.Range(0, rooms.Length);
            randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
            randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

            isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);

        }

        GameObject stairs = Instantiate(this.stairs, new Vector3(randX, randY, -1), Quaternion.identity);
        stairs.transform.parent = floorHolder.transform;
        tilesInfo[(randX)][(randY)] = stairs.GetComponent<TileInfo>();
        InstantiateTeams();
        InstantiatePokemons();
        InstantiateItems();

    }

    private void InstantiateTeams()
    {
        teamHolder = new GameObject("TeamHolder");
        teamHolder.tag = "TeamHolder";

        int randRoom = UnityEngine.Random.Range(0, rooms.Length);
        int randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
        int randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

        bool hassPokemon = tilesInfo[randX][randY].hasPokemon();
        bool isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);
        while (hassPokemon || isWall)
        {
            randRoom = UnityEngine.Random.Range(0, rooms.Length);
            randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
            randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

            hassPokemon = tilesInfo[randX][randY].hasPokemon();
            isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);

        }
        GameObject pokemonInstance = Instantiate(team[0], new Vector3(randX, randY, -1), Quaternion.identity);
        tilesInfo[randX][randY].pokemon = pokemonInstance;
        pokemonInstance.transform.parent = teamHolder.transform;
        pokemonInstance.GetComponent<Pokemon>().fc = this;
        pokemonInstance.GetComponent<Pokemon>().turn = -1;
        pokemonInstance.GetComponent<Pokemon>().isEnemy = false;
        pokemonInstance.GetComponent<Pokemon>().isPlayer = true;
        pokemonInstance.GetComponent<Pokemon>().inDungeon = true;
        pokemonInstance.GetComponent<Pokemon>().Level = Level;
        pokemonInstance.GetComponent<Pokemon>().Floor = Floor;
        pokemonInstance.GetComponent<Pokemon>().HP = HP;
        pokemonInstance.GetComponent<Pokemon>().HPBar = HPBar;
        cam.gameObject.GetComponent<FollowPlayer>().player = pokemonInstance;
    }

    public void reSetUp()
    {
        floorHolder = new GameObject("FloorHolder");
        floorHolder.tag = "Floor";

        SetupTilesArray();
        CreateRoomsAndHalls();

        SetTilesValuesForRooms();
        SetTilesValuesForHalls();

        InstantiateTiles();
        InstantiateOuterWalls();

        int randRoom = UnityEngine.Random.Range(0, rooms.Length);
        int randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
        int randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

        bool isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);
        while (isWall)
        {
            randRoom = UnityEngine.Random.Range(0, rooms.Length);
            randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
            randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

            isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);

        }

        GameObject stairs = Instantiate(this.stairs, new Vector3(randX, randY, -1), Quaternion.identity);
        stairs.transform.parent = floorHolder.transform;
        tilesInfo[(randX)][(randY)] = stairs.GetComponent<TileInfo>();
        rePositionTeam();
        InstantiatePokemons();
        InstantiateItems();

    }

    private void rePositionTeam()
    {
        int randRoom = UnityEngine.Random.Range(0, rooms.Length);
        int randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
        int randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

        bool hassPokemon = tilesInfo[randX][randY].hasPokemon();
        bool isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);
        while (hassPokemon || isWall)
        {
            randRoom = UnityEngine.Random.Range(0, rooms.Length);
            randX = UnityEngine.Random.Range(rooms[randRoom].xPos, rooms[randRoom].xPos + rooms[randRoom].roomWidth);
            randY = UnityEngine.Random.Range(rooms[randRoom].yPos, rooms[randRoom].yPos + rooms[randRoom].roomHeight);

            hassPokemon = tilesInfo[randX][randY].hasPokemon();
            isWall = tilesInfo[randX][randY].tileType.Equals(TileType.Wall);

        }

        foreach (Transform child in teamHolder.transform)
        {
            child.position.Set(randX, randY, -1);
        }

        }

    public void increaseTurn()
    {
        if (turn >= maxTurns)
        {
            turn = -1;


        }
        else
        {
            turn++;
        }


    }

    private void InstantiateItems()
    {
        itemHolder = new GameObject("ItemHolder");
        itemHolder.tag = "ItemHolder";

        for (int i =0; i < items.Random; i++)
        {
            int randIndex = UnityEngine.Random.Range(0,rooms.Length);
            int x = UnityEngine.Random.Range(rooms[randIndex].xPos, rooms[randIndex].xPos+rooms[randIndex].roomWidth);
            int y = UnityEngine.Random.Range(rooms[randIndex].yPos, rooms[randIndex].yPos + rooms[randIndex].roomHeight);
            while (tilesInfo[x][y].hasItem())
            {
                randIndex = UnityEngine.Random.Range(0, rooms.Length);
                x = UnityEngine.Random.Range(rooms[randIndex].xPos, rooms[randIndex].xPos + rooms[randIndex].roomWidth);
                y = UnityEngine.Random.Range(rooms[randIndex].yPos, rooms[randIndex].yPos + rooms[randIndex].roomHeight);
            }
            GameObject itemInstance = Instantiate(itemsSpanableInRoom[UnityEngine.Random.Range(0, itemsSpanableInRoom.Length)], new Vector3(x, y), Quaternion.identity);
            itemInstance.transform.parent = itemHolder.transform;
            tilesInfo[x][y].item = itemInstance;
            itemsInRoom.Add(itemInstance);

        }

    }

    private void InstantiatePokemons()
    {

        

        pokemonHolder = new GameObject("PokemonHolder");
        pokemonHolder.tag = "PokemonHolder";

        for (int i = 0; i < pokemons.Random; i++)
        {
            int randIndex = UnityEngine.Random.Range(0, rooms.Length);
            int x = UnityEngine.Random.Range(rooms[randIndex].xPos, rooms[randIndex].xPos + rooms[randIndex].roomWidth);
            int y = UnityEngine.Random.Range(rooms[randIndex].yPos, rooms[randIndex].yPos + rooms[randIndex].roomHeight);
            while (tilesInfo[x][y].hasPokemon())
            {
                randIndex = UnityEngine.Random.Range(0, rooms.Length);
                x = UnityEngine.Random.Range(rooms[randIndex].xPos, rooms[randIndex].xPos + rooms[randIndex].roomWidth);
                y = UnityEngine.Random.Range(rooms[randIndex].yPos, rooms[randIndex].yPos + rooms[randIndex].roomHeight);
            }
            GameObject pokemonInstance = Instantiate(pokemonSpanableInRoom[UnityEngine.Random.Range(0, pokemonSpanableInRoom.Length)], new Vector3(x, y,-1), Quaternion.identity);
            tilesInfo[x][y].pokemon = pokemonInstance;
            pokemonInstance.transform.parent = pokemonHolder.transform;
            maxTurns++;
            pokemonInstance.GetComponent<Pokemon>().turn = maxTurns;
            pokemonInstance.GetComponent<Pokemon>().fc = this;
            pokemonInstance.GetComponent<Pokemon>().isEnemy = true;
            pokemonInstance.GetComponent<Pokemon>().inDungeon = true;
            pokemonsInRoom.Add(pokemonInstance);

        }
    }



    private void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = floorHolder.transform;
    }

    private void InstantiateOuterWalls()
    {
        float leftEdge = -1f;
        float rightEdge = col + 0f;
        float bottomEdge = -1f;
        float topEdge = rows + 0f;

        InstantiateVerticalOuterWall(leftEdge, bottomEdge,topEdge);
        InstantiateVerticalOuterWall(rightEdge, bottomEdge,topEdge);

        InstantiateHorizontalOuterWall(topEdge, leftEdge, rightEdge);
        InstantiateHorizontalOuterWall(bottomEdge, leftEdge, rightEdge);
    }

    private void InstantiateHorizontalOuterWall(float yCoord, float startingX, float endingX)
    {
        float currX = startingX;

        while (currX <= endingX)
        {
            InstantiateFromArray(outerWallTiles, currX, yCoord);
            currX++;
        }
    }


    private void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        float currY = startingY;

        while(currY <= endingY)
        {
            InstantiateFromArray(outerWallTiles, xCoord, currY);
            currY++;
        }
    }

    internal void nextFloor()
    {
        Destroy(floorHolder);
        Destroy(pokemonHolder);
        Destroy(itemHolder);
        reSetUp();
    }

    private void InstantiateTiles()
    {


        for (int i = 0; i < tiles.Length; i++)
        {

            for (int j = 0; j < tiles[i].Length; j++)
            {
                if (tiles[i][j]==(TileType.Floor))
                {

                    InstantiateFromArray(floorTiles, i, j);
                    

                }
            
                if (tiles[i][j] == TileType.Wall)
                {
                    InstantiateFromArray(wallTiles, i, j);

                }
            }
        }
        


    }

    private void SetTilesValuesForHalls()
    {
        for (int i = 0; i < halls.Length; i++)
        {
            Hall currentHall = halls[i];

            for (int j = 0; j < currentHall.hallLenght; j++)
            {

                int xCoord = currentHall.startXPos;
                int yCoord = currentHall.startYPos;

                switch (currentHall.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                tiles[xCoord][yCoord] = TileType.Floor;


            }

        }
    }

    private void SetTilesValuesForRooms()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for (int j = 0; j < currentRoom.roomWidth; j++)
            {

                int xCoord = currentRoom.xPos + j;

                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    tiles[xCoord][yCoord] = TileType.Floor;
                }

            }

        }

    }

    private void CreateRoomsAndHalls()
    {
        rooms = new Room[numRooms.Random];

        halls = new Hall[rooms.Length - 1];

        rooms[0] = new Room();
        halls[0] = new Hall();

        rooms[0].SetupRoom(roomWidth, roomHeight, col, rows);

        halls[0].SetupHalls(rooms[0], hallLenght, roomWidth,roomHeight,col, rows,true);


        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();

            rooms[i].setUpRoom(roomWidth, roomHeight, col, rows,halls[i-1]);

            if (i < halls.Length)
            {
                halls[i] = new Hall();
                halls[i].SetupHalls(rooms[i], hallLenght, roomWidth, roomHeight, col, rows, false);

            }
        }
    }

    private void SetupTilesArray()
    {
        tiles = new TileType[col][];
        tilesInfo = new TileInfo[col][];
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
            tilesInfo[i] = new TileInfo[rows];
        }

    }


}
