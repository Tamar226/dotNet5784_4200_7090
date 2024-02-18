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
    /// <summary>
    /// Creating a new engineer and inserting it serially into an XML file
    /// </summary>
    public int Create(Engineer item)
    {
        int idEng=item.IdNumberEngineer;
        if (item.IdNumberEngineer == 0)
        {
            idEng = Config.NextEngineerId;
        }
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.IdNumberEngineer == item.IdNumberEngineer);
        if (engineer != null)
            throw new DalAlreadyExistsException($"Engineer with Id: {item.IdNumberEngineer} is already exist");
        Engineer new_engineer = item with {IdNumberEngineer=idEng };
        engineers?.Add(new_engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        return new_engineer.IdNumberEngineer;
    }
    /// <summary>
    /// Deleting an engineer from the XML list by identifying his ID number
    /// </summary>
    public void Delete(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.IdNumberEngineer == id);
        if (engineer is null)
            throw new DalDoesNotExistException($"engineer with ID={id} already not exists\n");
        engineers?.Remove(engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
    }
    /// <summary>
    /// Calling a single engineer from the XML file by serial number
    /// </summary>
    public Engineer? Read(int? id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return (engineers!.Find(element => element!.IdNumberEngineer == id));
    }
    /// <summary>
    /// Calling a single engineer from the XML file by serial number+filter
    /// </summary>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers!.FirstOrDefault(filter);
    }
    /// <summary>
    /// Returning the entire list of engineers from the XML file
    /// </summary>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
            return engineers!.Select(item => item);
        else
            return engineers!.Where(filter);
    }
    /// <summary>
    /// Updating engineer attributes in the XML file according to the client's request and requirement
    /// </summary>
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
    /// <summary>
    /// Clear the enguneers's list from the data
    /// </summary>
    public void Reset()
    {
        XMLTools.ResetFile("engineers");
    }
}
