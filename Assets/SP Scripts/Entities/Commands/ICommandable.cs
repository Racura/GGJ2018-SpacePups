
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandable
{
    bool IsActive { get; }
    bool HasCommand { get; }

    bool IsAcceptingCommands { get; }

    CommandAction CommandAction { get; }

    bool Is (object obj);

    void UnsetCommand ();
    bool TrySetCommand (CommandAction command);

}
