using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Location : IComparable<Location>
{
    public int Row { get; set; }
    public int Column { get; set; }

    public override bool Equals(object obj) {
        if (obj.GetType() == typeof(Location))
        {
            return this.CompareTo(obj as Location) == 0;
        }
        else
        {
            return base.Equals(obj);
        }
    }

    public override int GetHashCode() {
        return Row.GetHashCode() * Column.GetHashCode();
    }

    public int CompareTo(Location other) {
        int result = this.Row.CompareTo(other.Row);
        return result != 0 ? result : this.Column.CompareTo(other.Column);
    }
}
