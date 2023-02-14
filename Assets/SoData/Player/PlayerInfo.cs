using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    public bool isDashEnabled;
    public bool isFireballEnabled;
    public bool isIceEnabled;
    public bool isVineEnabled;
    public bool isJumpEnabled;

    public Vector3 position;
    public int life;
}
