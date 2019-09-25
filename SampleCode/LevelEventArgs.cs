using UnityEngine;
using System.Collections;
using System;

public class LevelEventArgs : EventArgs  {
    LevelUIManager leveluimanager;
    public LevelEventArgs(LevelUIManager l)
    {
        leveluimanager = l;
    }
}
