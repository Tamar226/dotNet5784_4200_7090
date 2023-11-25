namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

internal class DependenceImplementation : IDependence
{
    XDocument? doc;
    public int Create(Dependence item)
    {
        int IDReplace = Config.NextDependenceId;
        doc ??= XDocument.Load("..xml/DependenceImplementation.xml");
        doc.Descendants("ArrayOfDependence")
          !.Add(new XElement("Dependency",
                 new XAttribute("Id", IDReplace),
                 new XAttribute("DependentTask", item.DependentTask),
                 new XAttribute("previousIDTask", item.DependsOnTask)));
        doc.Save("..xml/DependenceImplementation.xml");
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
        doc ??= XDocument.Load("..xml/DependenceImplementation.xml");
        XElement? OneDependency =
           doc.Descendants("ArrayOfDependence")!.Elements("Dependency").Where(p => p.Element("Id")?.Value == id.ToString())
             .FirstOrDefault() ?? throw new DalDeletionImpossible($"Dependence with Id: {id} don't exist");

        doc.Save("..xml/DependenceImplementation.xml");
        return OneDependency;
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        if (filter != null)
        {
            var foundDependence =
           doc!.Descendants("ArrayOfDependence")!.Elements("Dependency").Where((p) => filter(p).ToString()
           .FirstOrDefault());
            return foundDependence!;
        }
        throw new DalNoFilterToQuery("no filther to query");
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        if (filter != null)
        {
            var foundDependence =
           doc!.Descendants("ArrayOfDependence")!.Elements("Dependency").Where((p) => filter(p).ToString());
            return foundDependence!;
        }
        throw new DalNoFilterToQuery("no filther to query");
    }

    public void Update(Dependence item)
    {
        doc ??= XDocument.Load("..xml/DependenceImplementation.xml");
        XElement? d = doc.Descendants("Dependency")
             .FirstOrDefault(elmn => elmn.Attribute("id")!.Value.Equals(item.IdNumberDependence)) ?? throw new DalDoesNotExistException($"Dependency with ID={item.IdNumberDependence} doesn't exist")
             d.Remove();

         doc.Descendants("ArrayOfDependence")
        !.Add(new XElement("Dependency",
         new XAttribute("Id", item.IdNumberDependence),
         new XAttribute("DependentTask", item.DependentTask),
         new XAttribute("previousIDTask", item.DependsOnTask)));
         doc.Save("..xml/DependenceImplementation.xml");

    }

}

