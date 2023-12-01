using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRespone
{
    public LoginRespone(PlayerModel player, enermy enermy)
    {
        this.player = player;
        this.enermy = enermy;
    }

    //     {
    //     "player": {
    //         "hp": 100.0,
    //         "mana": 100.0,
    //         "position": [
    //             -6.21999979019165,
    //             6.096672534942627,
    //             0.0
    //         ]
    //     },
    //     "enermy": {
    //         "hp": 1.0,
    //         "mana": 2.0,
    //         "walkSpeed": 3.0
    //     }
    // }
    public PlayerModel player { get; set; }
    public enermy enermy { get; set; }
}
