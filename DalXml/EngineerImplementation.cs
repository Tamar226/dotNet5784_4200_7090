namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
/// <summary>
/// CRUD functions of Engineer
/// </summary>
internal class EngineerImplementation : IEngineer
{

    public int Create(Engineer item)
    { 
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.IdNumberEngineer == item.IdNumberEngineer);
        if (engineer != null)
            throw new DalAlreadyExistsException($"Engineer with Id: {item.IdNumberEngineer} is already exist");
        Engineer new_engineer = item with { };
        engineers?.Add(new_engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        return new_engineer.IdNumberEngineer;
    }

    public void Delete(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.IdNumberEngineer == id);
        if (engineer is null)
            throw new DalDoesNotExistException($"engineer with ID={id} already not exists\n");
        engineers?.Remove(engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
    }

    public Engineer? Read(int? id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return (engineers!.Find(element => element!.IdNumberEngineer == id));
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers!.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
            return engineers!.Select(item => item);
        else
            return engineers!.Where(filter);
    }

    public void Update(Engineer item)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.IdNumberEngineer == item.IdNumberEngineer);
        if (engineer == null)
            throw new DalDoesNotExistException($"engineer with ID={item.IdNumberEngineer} already not exists\n");
        else
        {
            engineers?.Remove(engineer);
            engineers?.Add(item);
            XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        }
    }
    public void Reset()
    {
        XMLTools.ResetFile("engineers");
    }
}
