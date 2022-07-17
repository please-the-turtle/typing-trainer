using Godot;
using GodotTypingTrainerUI.Scripts.Globals;

namespace GodotTypingTrainerUI.Scripts.Extentions
{
    public static class NodeExtensions
    {
        public static Global GetGlobal(this Node node)
        {
            var root = node.GetTree().Root;
            Global global = root.GetNode<Global>("Global");

            return global;
        }
    }
}
