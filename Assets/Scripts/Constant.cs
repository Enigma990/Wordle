using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Constant : MonoBehaviour
{
    [Serializable]
    public enum GameMode { Classic, TimeTrail, Blind }

    public static GameMode currentGamemode;
}
