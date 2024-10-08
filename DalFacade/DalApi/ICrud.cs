﻿using DO;

public interface ICrud<T> where T : class
{
    public int Create(T item);//Creates new entity object in DAL
    public  T? Read(int? id); //Reads entity object by its ID
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); // stage 2, Reads all entity objects                                              
    public void Update(T item); //Updates entity object
    public void Delete(int id); //Deletes an object by its Id
    T? Read(Func<T, bool> filter); // stage 2
    public void Reset();//reset the lists of the entities from level 2+3- list+xml
    
}