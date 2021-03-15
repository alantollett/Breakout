using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour {

    private List<Command> commands = new List<Command>();

    public void Execute(Command c) {
        commands.Add(c);
        c.Execute();
    }

    public List<Command> clear() {
        List<Command> tmp = commands;
        commands = new List<Command>();
        return tmp;
    }

}
