using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int roomWidth;
    public int roomHeight;
    public int xPos;
    public int yPos;

    public Direction enteringHall;

    internal void SetupRoom(IntRange WidthRange, IntRange HeightRange, int col, int rows)
    {
        roomWidth = WidthRange.Random;
        roomHeight = HeightRange.Random;

        xPos = Mathf.RoundToInt(col / 2f - roomWidth / 2f);
        yPos = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);

    }

    internal void setUpRoom(IntRange WidthRange, IntRange HeightRange, int col, int rows, Hall hall)
    {
        enteringHall = hall.direction;

        roomWidth = WidthRange.Random;
        roomHeight = HeightRange.Random;

        switch (enteringHall)
        {
            case Direction.North:
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - hall.EndPositionY);

                yPos = hall.EndPositionY;

                xPos = UnityEngine.Random.Range(hall.EndPositionX - roomWidth + 1, hall.EndPositionX);

                xPos = Mathf.Clamp(xPos, 0, col - roomWidth);
                break;
            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1,hall.EndPositionY);

                yPos = hall.EndPositionY-roomHeight+1;

                xPos = UnityEngine.Random.Range(hall.EndPositionX - roomWidth + 1, hall.EndPositionX);

                xPos = Mathf.Clamp(xPos, 0, col - roomWidth);
                break;
            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, col - hall.EndPositionX);

                xPos = hall.EndPositionX;

                yPos = UnityEngine.Random.Range(hall.EndPositionY - roomHeight + 1, hall.EndPositionY);

                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1,hall.EndPositionX);

                xPos = hall.EndPositionX - roomWidth+1;

                yPos = UnityEngine.Random.Range(hall.EndPositionY - roomHeight + 1, hall.EndPositionY);

                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
        }


    }
}
