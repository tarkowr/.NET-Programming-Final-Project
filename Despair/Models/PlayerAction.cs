using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// enum of all possible player actions
    /// </summary>
    public enum PlayerAction
    {
        None,
        Setup,
        //---
        SearchArea,         //LookAround
        ViewObject,         //LookAt
        AddToInventory,     //PickUp
        Travel,
        Inventory,
        //---
        CloseDoor,
        EnterRoom,
        Caught,
        //---
        PlayerMenu,         //Menu
        PlayerInfo,
        PlayerEdit,
        LocationsVisited,
        //---
        GameInformation,    //AdminMenu
        ListKnownObjects,
        ListKnownNpcs,
        ListLocations,
        GameRules,
        //---
        NpcMenu,
        TalkTo,
        ReturnToMainMenu,
        Exit
    }
}
