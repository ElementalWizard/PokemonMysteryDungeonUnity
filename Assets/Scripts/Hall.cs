using UnityEngine;

public enum Direction
{
    North,East,South, West,
}

public class Hall
{

    public int startXPos;
    public int startYPos;
    public int hallLenght;
    public Direction direction;


    public int EndPositionX
    {
        get
        {
            if (direction == Direction.North || direction == Direction.South) return startXPos;
            if (direction == Direction.East) return startXPos + hallLenght - 1 ;
            return startXPos - hallLenght + 1;
        }
    }

    public int EndPositionY
    {
        get
        {
            if (direction == Direction.East || direction == Direction.West) return startYPos;
            if (direction == Direction.North) return startYPos + hallLenght - 1;
            return startYPos - hallLenght + 1;
        }
    }

    public void SetupHalls(Room room, IntRange lenght, IntRange roomWidth, IntRange roomHeight, int col, int rows, bool firstHall)
    {
        direction = (Direction)Random.Range(0, 4);

        Direction opositeDirection = (Direction)(((int)room.enteringHall + 2) % 4);

        if(!firstHall && direction == opositeDirection)
        {

            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }

        hallLenght = lenght.Random;

        int maxLenght = lenght.max;

        switch (direction)
        {
            case Direction.North:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);
                startYPos = room.yPos + room.roomHeight;
                maxLenght = rows - startYPos - roomHeight.min;

                break;
            case Direction.South:

                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLenght = startYPos - roomHeight.min;

                break;
            case Direction.East:

                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLenght = col - startXPos - roomWidth.min;
                
                break;
            case Direction.West:

                startXPos = room.xPos ;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLenght = startXPos - roomWidth.min;

                break;
        }
        hallLenght = Mathf.Clamp(hallLenght, 1, maxLenght);
    }
}
