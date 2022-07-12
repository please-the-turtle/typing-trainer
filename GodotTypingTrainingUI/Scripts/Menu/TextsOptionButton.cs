using System.Collections.Generic;
using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Menu
{
    public class TextsOptionButton : OptionButton
    {
        public override void _Ready()
        {
            base._Ready();
            UpdateItems();
        }

        private void UpdateItems()
        {
            Clear();

            foreach (var item in GetItems())
            {
                AddItem(item);
            }

            int lastTypingTextsindex = this.GetGlobal().ApplicationSettings.LastTypingTextsIndex;
            if (lastTypingTextsindex < Items.Count)
            {
                Selected = lastTypingTextsindex;
            }
        }

        private IEnumerable<string> GetItems()
        {
            var items = new List<string>();

            using (var directory = new Directory())
            {
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
            }

            return items;
        }
    }
}

