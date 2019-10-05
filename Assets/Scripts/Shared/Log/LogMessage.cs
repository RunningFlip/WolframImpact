using System;
using UnityEngine;


public static class LogMessage
{
    /// <summary>
    /// Prints an information into the console based on the given informations.
    /// </summary>
    /// <param name="_messageType"></param>
    /// <param name="_entityID"></param>
    /// <param name="_type"></param>
    public static void Info(MessageType _messageType, int _entityID = -1, Type _type = null)
    {
        PrintMessage(DebugType.Info, _messageType, _entityID, _type);
    }


    /// <summary>
    /// Prints a warning into the console based on the given informations.
    /// </summary>
    /// <param name="_messageType"></param>
    /// <param name="_entityID"></param>
    /// <param name="_type"></param>
    public static void Warning(MessageType _messageType, int _entityID = -1, Type _type = null)
    {
        PrintMessage(DebugType.Warning, _messageType, _entityID, _type);
    }


    /// <summary>
    /// Prints a fatal error into the console based on the given informations.
    /// Stops the application!
    /// </summary>
    /// <param name="_messageType"></param>
    /// <param name="_entityID"></param>
    /// <param name="_type"></param>
    public static void Error(MessageType _messageType, int _entityID = -1, Type _type = null)
    {
        PrintMessage(DebugType.Error, _messageType, _entityID, _type);
    }


    /// <summary>
    /// Builds the message.
    /// </summary>
    /// <param name="_debugType"></param>
    /// <param name="_messageType"></param>
    /// <param name="_entityID"></param>
    /// <param name="_type"></param>
    private static void PrintMessage(DebugType _debugType, MessageType _messageType, int _entityID, Type _type)
    {
        string message = _messageType.ToString() + ": ";

        switch (_messageType)
        {
            //Component
            case MessageType.ComponentAlreadyAttached:
                message += "The component that was tryied to attach already exists!";
                break;

            case MessageType.NoComponentFound:
                message += "Requested component was not found!";
                if (_type != null) message += " Type: " + _type.ToString() + "! ";
                break;


            //Entity
            case MessageType.EntityAlreadyExists:
                message += "An entity with this ID already exists!";
                if (_entityID != -1) message += " ID: " + _entityID + "! ";
                break;

            case MessageType.NoEntityFound:
                message += "Requested entity does not exist!";
                if (_entityID != -1) message += " Requested ID: " + _entityID + "! ";
                break;

            case MessageType.NoEntityGameObjectFound:
                message += "Requested gameObject of the entity does not exist!";
                if (_entityID != -1) message += " Requested ID: " + _entityID + "! ";
                break;


            //Type
            case MessageType.WrongType:
                message += "Wrong type used!";
                if (_type != null) message += " Type: " + _type.ToString() + "! ";
                break;
        }

        //Entity ID
        if (_entityID != -1)
        {
            message += " Found in Entity-ID: " + _entityID;
        }

        //Print message
        switch (_debugType)
        {
            case DebugType.Info:
                Debug.Log(message);
                break;
            case DebugType.Warning:
                Debug.LogWarning(message);
                break;
            case DebugType.Error:
                Debug.LogError(message);
                break;
        }
    }
}


/// <summary>
/// Type of message that should be printed.
/// </summary>
public enum MessageType
{
    //Components
    ComponentAlreadyAttached,
    NoComponentFound,

    //Entities
    EntityAlreadyExists,
    NoEntityFound,
    NoEntityGameObjectFound,

    //Types
    WrongType,
}


/// <summary>
/// Type of debug information.
/// </summary>
enum DebugType
{
    Info,
    Warning,
    Error,
}
