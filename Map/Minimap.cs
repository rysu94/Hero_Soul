using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    //=========================================================
    //                       MINIMAP                          
    //=========================================================

    //These vairables manage the minimap component of the HUD
    public static int playerCurrentX;
    public static int playerCurrentY;

    public GameObject miniParent;

    public GameObject miniCenter;
    public GameObject miniNorthWest;
    public GameObject miniNorth;
    public GameObject miniNorthEast;
    public GameObject miniWest;
    public GameObject miniEast;
    public GameObject miniSouthWest;
    public GameObject miniSouth;
    public GameObject miniSouthEast;

    string tileAddress;

    public void GenMinimap(Room[,] levelGrid, int playerX, int playerY)
    {

        miniCenter = null;
        miniNorthWest = null;
        miniNorth = null;
        miniNorthEast = null;
        miniWest = null;
        miniEast = null;
        miniSouthWest = null;
        miniSouth = null;
        miniSouthEast = null;


        //Get the center tile
        tileAddress = DetermineTile(levelGrid, playerX, playerY, true);
        miniCenter = Instantiate(Resources.Load(tileAddress)) as GameObject;
        miniCenter.transform.SetParent(miniParent.transform, false);
        miniCenter.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX, playerY);
        miniCenter.gameObject.SetActive(true);
        CheckExplored(levelGrid, playerX, playerY, ref miniCenter);

        //Get North Tile
        if (playerX - 1 >= 0 && levelGrid[playerX - 1, playerY] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX - 1, playerY, false);
            miniNorth = Instantiate(Resources.Load(tileAddress), new Vector2(0, 91), Quaternion.identity) as GameObject;
            miniNorth.transform.SetParent(miniParent.transform, false);
            miniNorth.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX - 1, playerY);
            miniNorth.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX - 1, playerY, ref miniNorth);
        }

        //Get South Tile
        if (playerX + 1 < LevelCreator.gridX && levelGrid[playerX + 1, playerY] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX + 1, playerY, false);
            miniSouth = Instantiate(Resources.Load(tileAddress), new Vector2(0, -91), Quaternion.identity) as GameObject;
            miniSouth.transform.SetParent(miniParent.transform, false);
            miniSouth.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX + 1, playerY);
            miniSouth.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX + 1, playerY, ref miniSouth);
        }

        //Get West Tile
        if (playerY - 1 >= 0 && levelGrid[playerX, playerY - 1] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX, playerY - 1, false);
            miniWest = Instantiate(Resources.Load(tileAddress), new Vector2(-91, 0), Quaternion.identity) as GameObject;
            miniWest.transform.SetParent(miniParent.transform, false);
            miniWest.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX, playerY - 1);
            miniWest.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX, playerY - 1, ref miniWest);
        }

        //Get East Tile
        if (playerY + 1 < LevelCreator.gridY && levelGrid[playerX, playerY + 1] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX, playerY + 1, false);
            miniEast = Instantiate(Resources.Load(tileAddress), new Vector2(91, 0), Quaternion.identity) as GameObject;
            miniEast.transform.SetParent(miniParent.transform, false);
            miniEast.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX, playerY + 1);
            miniEast.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX, playerY + 1, ref miniEast);
        }

        //Get Northwest Tile
        if (playerX - 1 >= 0 && playerY - 1 >= 0 && levelGrid[playerX - 1, playerY - 1] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX - 1, playerY - 1, false);
            miniNorthWest = Instantiate(Resources.Load(tileAddress), new Vector2(-91, 91), Quaternion.identity) as GameObject;
            miniNorthWest.transform.SetParent(miniParent.transform, false);
            miniNorthWest.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX - 1, playerY - 1);
            miniNorthWest.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX - 1, playerY - 1, ref miniNorthWest);
        }

        //Get Northeast Tile
        if (playerX - 1 >= 0 && playerY + 1 < LevelCreator.gridY && levelGrid[playerX - 1, playerY + 1] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX - 1, playerY + 1, false);
            miniNorthEast = Instantiate(Resources.Load(tileAddress), new Vector2(91, 91), Quaternion.identity) as GameObject;
            miniNorthEast.transform.SetParent(miniParent.transform, false);
            miniNorthEast.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX - 1, playerY + 1);
            miniNorthEast.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX - 1, playerY + 1, ref miniNorthEast);
        }

        //Get Southwest Tile
        if (playerX + 1 < LevelCreator.gridX && playerY - 1 >= 0 && levelGrid[playerX + 1, playerY - 1] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX + 1, playerY - 1, false);
            miniSouthWest = Instantiate(Resources.Load(tileAddress), new Vector2(-91, -91), Quaternion.identity) as GameObject;
            miniSouthWest.transform.SetParent(miniParent.transform, false);
            miniSouthWest.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX + 1, playerY - 1);
            miniSouthWest.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX + 1, playerY - 1, ref miniSouthWest);
        }

        //Get Southeast Tile
        if (playerX + 1 < LevelCreator.gridX && playerY + 1 < LevelCreator.gridY && levelGrid[playerX + 1, playerY + 1] != null)
        {
            tileAddress = DetermineTile(levelGrid, playerX + 1, playerY + 1, false);
            miniSouthEast = Instantiate(Resources.Load(tileAddress), new Vector2(91, -91), Quaternion.identity) as GameObject;
            miniSouthEast.transform.SetParent(miniParent.transform, false);
            miniSouthEast.GetComponentInChildren<Text>().text += CheckRoomType(levelGrid, playerX + 1, playerY + 1);
            miniSouthEast.gameObject.SetActive(true);
            CheckExplored(levelGrid, playerX + 1, playerY + 1, ref miniSouthEast);
        }
    }


    //Determines the Path of the correct room for the minimap tile
    public string DetermineTile(Room[,] levelGrid, int x, int y, bool isCenter)
    {
        string pickedTile = "Prefabs/MiniMapTiles/Mini_4_On";
        bool roomNorth = false;
        bool roomSouth = false;
        bool roomEast = false;
        bool roomWest = false;
        int surroundRoom = GetSurroundingRooms(levelGrid, x, y, ref roomNorth, ref roomWest, ref roomEast, ref roomSouth);

        //1 Room tiles
        if (surroundRoom == 1)
        {
            if (isCenter)
            {
                if (roomNorth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1A_On";
                }
                else if (roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1C_On";
                }
                else if (roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1D_On";
                }
                else if (roomEast)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1B_On";
                }
            }
            else if (!isCenter)
            {
                if (roomNorth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1A_Off";
                }
                else if (roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1C_Off";
                }
                else if (roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1D_Off";
                }
                else if (roomEast)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_1B_Off";
                }
            }
        }

        //2 Room Tiles
        else if (surroundRoom == 2)
        {
            if (isCenter)
            {
                if (roomNorth && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2E_On";
                }
                else if (roomNorth && roomEast)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2C_On";
                }
                else if (roomNorth && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2B_On";
                }
                else if (roomSouth && roomEast)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2D_On";
                }
                else if (roomSouth && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2A_On";
                }
                else if (roomEast && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2F_On";
                }
            }
            else if (!isCenter)
            {
                if (roomNorth && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2E_Off";
                }
                else if (roomNorth && roomEast)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2C_Off";
                }
                else if (roomNorth && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2B_Off";
                }
                else if (roomSouth && roomEast)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2D_Off";
                }
                else if (roomSouth && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2A_Off";
                }
                else if (roomEast && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_2F_Off";
                }
            }
        }

        //3 Room Tiles
        else if (surroundRoom == 3)
        {
            if (isCenter)
            {
                if (roomNorth && roomEast && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3C_On";
                }
                else if (roomNorth && roomEast && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3D_On";
                }
                else if (roomNorth && roomWest && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3B_On";
                }
                else if (roomEast && roomWest && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3A_On";
                }
            }
            else if (!isCenter)
            {
                if (roomNorth && roomEast && roomWest)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3C_Off";
                }
                else if (roomNorth && roomEast && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3D_Off";
                }
                else if (roomNorth && roomWest && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3B_Off";
                }
                else if (roomEast && roomWest && roomSouth)
                {
                    pickedTile = "Prefabs/MiniMapTiles/Mini_3A_Off";
                }
            }
        }
        else if (surroundRoom == 4)
        {
            if (isCenter)
            {
                pickedTile = "Prefabs/MiniMapTiles/Mini_4_On";
            }
            else if (!isCenter)
            {
                pickedTile = "Prefabs/MiniMapTiles/Mini_4_Off";
            }
        }
        return pickedTile;
    }

    string CheckRoomType(Room[,] levelGrid, int x, int y)
    {
        string roomType = "";
        if (levelGrid[x, y].isStart)
        {
            roomType = "S";
        }
        else if (levelGrid[x, y].isEnd)
        {
            roomType = "E";
        }
        else if (levelGrid[x, y].isSpecial)
        {
            roomType = "?";
        }
        else if(levelGrid[x, y].isBoss)
        {
            roomType = "B";
        }

        return roomType;
    }

    //Determines how many Rooms border a room
    public static int GetSurroundingRooms(Room[,] levelGrid, int roomX, int roomY, ref bool n, ref bool w, ref bool e, ref bool s)
    {
        int numBorder = 0;

        //Check North
        if ((roomX - 1) >= 0)
        {
            if (levelGrid[roomX - 1, roomY] != null)
            {
                numBorder++;
                n = true;
            }
        }

        //Check East
        if ((roomY + 1) < LevelCreator.gridY)
        {
            if (levelGrid[roomX, roomY + 1] != null)
            {
                numBorder++;
                e = true;
            }
        }

        //Check West
        if ((roomY - 1) >= 0)
        {
            if (levelGrid[roomX, roomY - 1] != null)
            {
                numBorder++;
                w = true;
            }
        }

        //Check South
        if ((roomX + 1) < LevelCreator.gridX)
        {
            if (levelGrid[roomX + 1, roomY] != null)
            {
                numBorder++;
                s = true;
            }
        }

        //print(numBorder);


        return numBorder;
    }

    void CheckExplored(Room[,] levelGrid, int x, int y, ref GameObject tile)
    {
        if (levelGrid[x, y].isExplored)
        {
            Image tempImage = tile.GetComponent<Image>();
            tempImage.color = new Color(.5f, 1f, 0f, 1);
        }
    }

    //=========================================================
    //                    MINIMAP END                         
    //=========================================================

}
