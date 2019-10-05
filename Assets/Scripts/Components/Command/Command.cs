using System;
using System.Collections.Generic;


[Serializable]
public class Command
{
    public CommandConfig commandConfig;
    public List<CommandType> commandTypes;


    public Command(CommandConfig _commandConfig, List<CommandType> _commandTypes)
    {
        commandConfig = _commandConfig;
        commandTypes = _commandTypes;
    }
}
