using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTest.Data
{
    public sealed class ValueKey
    {
        public readonly object[] DimKeys;
        public ValueKey(params object[] dimKeys)
        {
            DimKeys = dimKeys;
        }
        public override int GetHashCode()
        {
            if (DimKeys == null) return 0;
            int hashCode = DimKeys.Length;
            for (int i = 0; i < DimKeys.Length; i++)
            {
                hashCode ^= DimKeys[i].GetHashCode();
            }
            return hashCode;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ValueKey))
                return false;
            var x = DimKeys;
            var y = ((ValueKey)obj).DimKeys;
            if (ReferenceEquals(x, y))
                return true;
            if (x.Length != y.Length)
                return false;
            for (int i = 0; i < x.Length; i++)
            {
                if (!x[i].Equals(y[i]))
                    return false;
            }
            return true;
        }
    }
}
