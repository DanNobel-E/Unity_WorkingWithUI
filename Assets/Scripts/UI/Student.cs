using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rank { get; set; }
    public int Age { get; set; }
}

class StudentNameComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.Name == y.Name)
        {
            return true;
        }
        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.Name.GetHashCode();
    }
}

/* GetHashCode Notes:
Every object in .NET has an Equals method and a GetHashCode method.
The Equals method is used to compare one object with another object - to see if the two objects are equivalent.
The GetHashCode method generates a 32-bit integer representation of the object. Since there is no limit to how much information an object can contain, certain hash codes are shared by multiple objects - so the hash code is not necessarily unique.
A dictionary is a data structure that trades a higher memory footprint in return for (more or less) constant costs for Add/Remove/Get operations. It is a poor choice for iterating over though. Internally, a dictionary contains an array of buckets, where values can be stored. When you add a Key and Value to a dictionary, the GetHashCode method is called on the Key. The hashcode returned is used to determine the index of the bucket in which the Key/Value pair should be stored.
When you want to access the Value, you pass in the Key again. The GetHashCode method is called on the Key, and the bucket containing the Value is located.
When an IEqualityComparer is passed into the constructor of a dictionary, the IEqualityComparer.Equals and IEqualityComparer.GetHashCode methods are used instead of the methods on the Key objects.
 */

