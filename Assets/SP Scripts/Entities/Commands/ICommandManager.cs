
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandManager
{
    int CommandObjectCount { get; }
    
    void AddObject (ICommandable commandable);
    void RemoveObject (ICommandable commandable);
    bool ContainsObject (ICommandable commandable);



    void SetCommand (CommandAction command);
    void SetCommand (CommandAction command, object obj);

    bool HasCommand ();

}
