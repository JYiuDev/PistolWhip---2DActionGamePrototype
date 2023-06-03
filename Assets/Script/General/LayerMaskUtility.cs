using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskUtility
{
    public static LayerMask Only(params string[] ids)
    {
        LayerMask mask = new LayerMask();

        for (int i = 0; i < ids.Length; i++)
        {
            mask = Add(mask, Shift(Get(ids[i])));
        }

        return mask;
    }

    public static LayerMask AllBut(params string[] ids)
    {
        return Invert(Only(ids));
    }

    public static LayerMask Invert(LayerMask mask)
    {
        return ~mask;
    }

    public static LayerMask Add(LayerMask mask1, LayerMask mask2)
    {
        return mask1 | mask2;
    }

    public static LayerMask Shift(LayerMask mask)
    {
        return 1 << mask;
    }

    public static LayerMask Get(string id)
    {
        return LayerMask.NameToLayer(id);
    }
}


