using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public PlayerState()
    {
        Hp = 100;
        Ammo = 60;
    }

    public int Hp { get; set; }
    public int Ammo { get; set; }

}
