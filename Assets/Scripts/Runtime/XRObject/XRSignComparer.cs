using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRSignComparer : IComparer<XRSign>
{
    public int Compare(XRSign x, XRSign y)
    {
        return x.sequence.CompareTo(y.sequence);
    }
}
