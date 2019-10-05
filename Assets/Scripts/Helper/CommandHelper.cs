
using System;

public static class CommandHelper {

    public static CommandConfig GetCommandConfig(CommandTargetComponent _commandTargetComponent, CommandType _commandType) {
        CommandConfig actionConfig = null;

        for (int i = 0; i < _commandTargetComponent.commands.Count; i++) {
            if (_commandTargetComponent.commands[i].commandTypes.Contains(_commandType)) {

                actionConfig = _commandTargetComponent.commands[i].commandConfig;
                break;
            }
        }
        return actionConfig;
    }


    public static CommandConfig GetCommandConfigByType(CommandTargetComponent _commandTargetComponent, Type _type) {
        CommandConfig config = null;

        for (int i = 0; i < _commandTargetComponent.commands.Count; i++) {
            if (_commandTargetComponent.commands[i].commandConfig.GetType() == _type) {
                config = _commandTargetComponent.commands[i].commandConfig;
                break;
            }
        }
        return config;
    }

}
