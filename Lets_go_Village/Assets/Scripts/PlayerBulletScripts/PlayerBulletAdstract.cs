using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum playerBulletType
{
    Normal,
    vehicle,
    Boomerang,
    HAIYORU,
    Mountain,
    Chicken
}


abstract public class PlayerBulletAdstract : MonoBehaviour
{
    abstract public float GetCooltime();

    abstract public float GetBulletPower();
}
