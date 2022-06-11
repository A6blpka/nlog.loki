using System;
using System.Collections.Generic;

namespace NLog.Loki.Model;

internal class LokiLabels : IEquatable<LokiLabels>
{
    private readonly int _hashCode;

    public ISet<LokiLabel> Labels { get; }

    public LokiLabels(ISet<LokiLabel> labels)
    {
        Labels = labels;
        unchecked
        {
            var hash = 0;
            foreach(var label in labels)
                hash = (hash * 397) ^ label.GetHashCode();
            _hashCode = hash;
        }
    }

    public bool Equals(LokiLabels other)
    {
        if(ReferenceEquals(null, other))
            return false;
        if(ReferenceEquals(this, other))
            return true;
        return Labels.SetEquals(other.Labels);
    }

    public override bool Equals(object other)
    {
        if(ReferenceEquals(null, other))
            return false;
        if(ReferenceEquals(this, other))
            return true;
        if(other.GetType() != this.GetType())
            return false;
        return Equals((LokiLabels)other);
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public static bool operator ==(LokiLabels left, LokiLabels right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LokiLabels left, LokiLabels right)
    {
        return !Equals(left, right);
    }
}
