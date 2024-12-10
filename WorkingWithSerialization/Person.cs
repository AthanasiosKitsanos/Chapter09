using System;
using System.Xml.Serialization; //  To use [XmlAttribute]

namespace Packt.Shared;

public class Person
{
    public Person() { } // To serialize a class, we need to create a parameterless Constructor, else an Unhandled Exception messege will appear.
    public Person(decimal initialSalary)
    {
        Salary = initialSalary;
    }

    // By using [XmlAttribute], we modify the way the Class Attributes appear inside the Xml. e.i. FirstName will appear as fname inside the Xml.
    [XmlAttribute("fname")]
    public string? FirstName { get; set; }
    [XmlAttribute("lname")]
    public string? LastName { get; set; }
    [XmlAttribute("DoD")]
    public DateTime DateOfBirth { get; set; }
    public HashSet<Person>? Children { get; set; }
    protected decimal Salary { get; set; }
}

