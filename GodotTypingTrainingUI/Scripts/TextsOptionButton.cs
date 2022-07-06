using Godot;
using System.Collections.Generic;

namespace GodotTypingTrainerUI.Scripts
{
    public class TextsOptionButton : OptionButton
    {
        public override void _Ready()
        {
            UpdateItems();
        }

        private void UpdateItems()
        {
            foreach (var item in GetItems())
            {
                AddItem(item);
            }
        }

        private IEnumerable<string> GetItems()
        {
            var items = new List<string>();

            Directory directory = new Directory();
            var error = directory.Open(this.GetGlobal().ApplicationSettings.TextsPath);
            if (error != Error.Ok)
            {
                return null;
            }

            string textsExtension = this.GetGlobal().ApplicationSettings.TypingTextsExtention;
            directory.ListDirBegin(true);
            string dirContent;
            do
            {
                dirContent = directory.GetNext();
                if (dirContent != string.Empty && dirContent.EndsWith(textsExtension))
                {
                    var item = dirContent.Remove(dirContent.Length - textsExtension.Length);
                    items.Add(item);
                }
            } while (dirContent != string.Empty);
            directory.ListDirEnd();

            return items;
        }
    }
}

