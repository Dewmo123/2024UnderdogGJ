using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarNode : IComparable<AstarNode>
{
    public Vector3Int pos;
    public AstarNode parent;
    public float G;
    public float F;

    public int CompareTo(AstarNode other)
    {
        //같으면 0, 대소비교를 통해 -1, 1을 리턴해주는 함수
        if (other.F == F)
            return 0;
        else
            return other.F < F ? -1 : 1;
    }

    public override bool Equals(object obj) => this.Equals(obj as AstarNode);
    public override int GetHashCode() => pos.GetHashCode();
    
    public bool Equals(AstarNode other)
    {
        if (other == null) return false;
        if (this.GetType() != other.GetType()) return false;

        return pos == other.pos;
    }

    public static bool operator == (AstarNode lhs, AstarNode rhs)
    {
        if(lhs is null)
        {
            if (rhs is null)
                return true;

            return false;
        }
        return lhs.Equals(rhs);
    }
    public static bool operator !=(AstarNode lhs, AstarNode rhs) => !(lhs == rhs);
}
