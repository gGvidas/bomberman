using System.Collections.Generic;

namespace BombermanClasses.Command
{
    public class MovementInvoker
    {
        private Dictionary<string,IMoveCommand> commands;
        private IMoveCommand currentCommand;

        public MovementInvoker()
        {
            commands = new Dictionary<string, IMoveCommand>();
        }
        public void setCommand(IMoveCommand command)
        {
            currentCommand = command;
        }
        public void undo(string playerId)
        {
            IMoveCommand command = commands.ContainsKey(playerId) ? commands[playerId] : null;

            if (command != null)
            {
                command.undo();
                command.undo();
                commands.Remove(playerId);
            }
        }
        public void move()
        {
            if (currentCommand != null)
            {
                currentCommand.move();
                commands[currentCommand.getPlayerId()] = currentCommand;
                currentCommand = null;
            }
        }
    }
}
