using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqTest : MonoBehaviour
{
    public string[] strings = { "game", "food", "ball", "sword", "sword", "ball", "sword" };
    public string StringToFind;
    public bool PerformSearch;
    List<Student> students = new List<Student>();

    private void Start()
    {
        students.Add(new Student { Id = 1, Name = "Alex", Age = 22 });
        students.Add(new Student { Id = 2, Name = "Tom", Age = 17 });
        students.Add(new Student { Id = 3, Name = "Harry", Age = 24 });
        students.Add(new Student { Id = 4, Name = "Bill", Age = 25 });
    }
    void Search()
    {
        foreach (string name in strings)
        {
            if (name == StringToFind)
                Debug.Log("Found " + StringToFind + " in the array!");
        }
    }
    void SearchLinq()
    {
        //Check whether any single element in a sequence satisfy a given condition (string length is > 4)
        if (strings.Any(name => name.Length > 4))
            Debug.Log("There is at least one string with len > 4!");

        //Contains
        if (strings.Contains(StringToFind))
            Debug.Log("Found " + StringToFind + " in the array!");
        //Contains, IEqualityComparer<T> version
        Student studentToFind = new Student { Name = "Alex" };
        if (students.Contains(studentToFind, new StudentNameComparer()))
            Debug.Log("Found " + studentToFind.Name + " in the students array!");

        //Distinct
        IEnumerable<string> distinctStrings = strings.Distinct();
        string outString = "";
        foreach (string currDistinctString in distinctStrings)
            outString += currDistinctString + ", ";
        Debug.Log("distinctStrings: " + outString);
        //Where
        IEnumerable<string> result = strings.Where(name => name[0] == 's');
        outString = "";
        foreach (string currString in result)
            outString += currString + ", ";
        Debug.Log("result: " + outString);

        //First - if StringToFind is not present, throws an exception
        string FirstElement = strings.Where(name => name == StringToFind).First(); //1
        Debug.Log("First: " + FirstElement); 
        //FirstOrDefault - if StringToFind is not present, handle it with a null (try to use a wrong StringToFind value: instruction //1 will throw an exception)
        string FirstDefaultElement = strings.Where(name => name == StringToFind).FirstOrDefault();
        Debug.Log("FirstOrDefault: " + FirstDefaultElement);
    }

    void Update()
    {
        if (PerformSearch)
        {
            PerformSearch = false;
            Search();
            SearchLinq();
        }
    }
}
