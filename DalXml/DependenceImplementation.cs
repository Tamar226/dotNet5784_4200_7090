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
    XDocument? doc;
    public int Create(Dependence item)
    {
        int IDReplace = Config.NextDependenceId;
        doc ??= XDocument.Load("C:/Users/PC/source/Repos/dotNet5784_4200_7090/xml/dependences.xml");
        doc!.Element("ArrayOfDependence")
          !.Add(new XElement("Dependency",
                 new XAttribute("Id", IDReplace),
                 new XAttribute("DependentTask", item.DependentTask),
                 new XAttribute("previousIDTask", item.DependsOnTask)));
        doc.Save("C:/Users/PC/source/Repos/dotNet5784_4200_7090/xml/dependences.xml");
        return IDReplace;
    }

    public void Delete(int id)
    {
        doc ??= XDocument.Load("..xml/DependenceImplementation.xml");
        var removeDependency =
            doc.Descendants("ArrayOfDependence")!.Elements("Dependency").Where(p => p.Element("Id")?.Value == id.ToString())
            .FirstOrDefault() ?? throw new DalDeletionImpossible($"Dependence with Id: {id} don't exist");
        removeDependency!.Remove();
        doc.Save("..xml/DependenceImplementation.xml");
    }
    public Dependence? Read(int id)
    {
        XDocument doc = XDocument.Load("C:/Users/PC/source/Repos/dotNet5784_4200_7090/xml/dependences.xml");
        var root = doc.Descendants("ArrayOfDependence");
        XElement dependenceElement = root.Elements("Dependency")
                                      .FirstOrDefault(e => e.Element("Id")!.Value.Equals(Convert.ToString(id)))??throw new DalDeletionImpossible($"Dependence with Id: {id} don't exist");
        //var oneDependency = doc.Descendants("Dependency");
        //var filteredDependencies = oneDependency.Where(d => d.Element("Id")?.Value == id.ToString());

        if (dependenceElement == null)
        {
            throw new DalDoesNotExistException($"Dependence with Id: {id} doesn't exist");
        }

        return XMLTools.CreateDependenceFromXmlElement(dependenceElement);
    }
    public Dependence Read(Func<Dependence, bool> filter)
    {
        if (filter != null)
        {
            XDocument doc = XDocument.Load("C:/Users/PC/source/Repos/dotNet5784_4200_7090/xml/dependences.xml");

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
            var foundDependence = doc!.Descendants("Dependency")
                                     .Where(dependency => filter(XMLTools.CreateDependenceFromXmlElement(dependency)))
                                     .Select(dependency => XMLTools.CreateDependenceFromXmlElement(dependency));

            return foundDependence;
        }
        else
        {
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
        XDocument doc = XDocument.Load("C:/Users/PC/source/Repos/dotNet5784_4200_7090/xml/dependences.xml");
        var root=doc.Descendants("ArrayOfDependence");
        XElement dependentToUpdate = root.Elements("Dependency")
                                      .First(e => e.Element("Id")?.Value == item.IdNumberDependence.ToString());
        dependentToUpdate.Remove();
        doc.Element("ArrayOfDependence")
       !.Add(new XElement("Dependency",
        new XAttribute("Id", item.IdNumberDependence),
        new XAttribute("DependentTask", item.DependentTask),
        new XAttribute("previousIDTask", item.DependsOnTask)));
        doc.Save("..xml/DependenceImplementation.xml");


    }

}

