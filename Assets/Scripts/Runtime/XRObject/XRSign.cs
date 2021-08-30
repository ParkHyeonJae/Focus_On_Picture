using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class XRSign : XRObject
{
    [Range(0, 127)]
    public sbyte sequence = 0;
}
