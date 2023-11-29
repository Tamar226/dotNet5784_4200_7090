namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.Security.Cryptography;

internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        int IDReplace = Config.NextDependenceId;
        XDocument doc = XDocument.Load("../xml/dependences.xml");
        doc!.Element("ArrayOfDependence")
          !.Add(new XElement("Dependency",
                 new XElement("Id", IDReplace),
                 new XElement("DependentTask", item.DependentTask),
                 new XElement("previousIDTask", item.DependsOnTask)));
        doc.Save("../xml/dependences.xml");
        return IDReplace;
    }

     public void Delete(int id)
     {
        XDocument doc = XDocument.Load("../xml/dependences.xml");
        var dependencyItem = doc.Descendants("Dependency").Where(p => p.Element("Id")?.Value == id.ToString())
             .FirstOrDefault() ?? throw new DalDeletionImpossible($"Dependence with Id: {id} don't exist");
        dependencyItem!.Remove();
         doc.Save("../xml/dependences.xml");
     }
    /* public Dependence? Read(int id)//Reads dependency object by its ID
     {
         doc ??= XDocument.Load("../xml/dependences.xml");
         var x =
         var no = XMLTools.CreateDependenceFromXmlElement(x.First());
         return no;
     }*/
    public Dependence? Read(int id)//Reads dependency object by its ID
    {
        XDocument doc = XDocument.Load("../xml/dependences.xml");
        var dependencyItem = doc.Descendants("Dependency");
        var batata= dependencyItem.
                    FirstOrDefault(dependency => dependency.Element("Id")!.Value.Equals(Convert.ToString(id)))
                    ?? throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        Dependence dependency=XMLTools.CreateDependenceFromXmlElement(batata);
        return dependency;

    }
        //var x = from child in d!.Descendants("Family")
        //                    .First(f => f.Attribute("FamilyName")!.Value.Equals("Kohen"))
        //                    .Descendants("Child")
        //        select new { ChildName = child.Attribute("ChildName")!.Value, Age = child.Attribute("Age")?.Value ?? "אין תכונה גיל", Min = child.Attribute("Min")!.Value };


        public Dependence Read(Func<Dependence, bool> filter)
    {
        if (filter != null)
        {
            XDocument doc = XDocument.Load("../xml/dependences.xml");

            var oneDependency = doc.Descendants("Dependency");
            var filteredDependencies = oneDependency.Where(dependency => filter(XMLTools.CreateDependenceFromXmlElement(dependency)));

            if (filteredDependencies==null)
            {
                // Assuming you want to return the first matched dependence
                return XMLTools.CreateDependenceFromXmlElement(filteredDependencies!.First());
            }

            throw new DalNoFilterToQuery("No filter matched any dependencies.");
        }

        throw new DalNoFilterToQuery("No filter provided to query.");
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        if (filter != null)
        {
            XDocument doc = XDocument.Load("../xml/dependences.xml");
            var foundDependence = doc!.Descendants("Dependency")
                                     .Where(dependency => filter(XMLTools.CreateDependenceFromXmlElement(dependency)))
                                     .Select(dependency => XMLTools.CreateDependenceFromXmlElement(dependency));

            return foundDependence;
        }
        else
        {
            XDocument doc = XDocument.Load("../xml/dependences.xml");
            var allDependences = doc!.Descendants("Dependency")
                                    .Select(dependency => XMLTools.CreateDependenceFromXmlElement(dependency));

            return allDependences;
        }
    }

    //public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    //{
    //    if (filter != null)
    //    {
    //        var foundDependence = ;
    //        doc!.Descendants("ArrayOfDependence")!.Elements("Dependency").Where((p) => filter(p).ToString());
    //        return foundDependence!;
    //    }
    //    throw new DalNoFilterToQuery("no filther to query");
    //}

    public void Update(Dependence item)
    {
        XDocument doc = XDocument.Load("../xml/dependences.xml");
        var root=doc.Descendants("ArrayOfDependence");
        XElement dependentToUpdate = root.Elements("Dependency")
                                      .First(e => e.Element("Id")?.Value == item.IdNumberDependence.ToString());
        dependentToUpdate.Remove();
        doc.Element("ArrayOfDependence")
       !.Add(new XElement("Dependency",
        new XAttribute("Id", item.IdNumberDependence),
        new XAttribute("DependentTask", item.DependentTask),
        new XAttribute("previousIDTask", item.DependsOnTask)));
        doc.Save("../xml/dependences.xml");


    }

}

