using System.Collections.Generic;
using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Menu
{
    public class TextsOptionButton : OptionButton
    {
        public override void _Ready()
        {
            UpdateItems();
        }

        private void UpdateItems()
        {
            Clear();
            Text = "No one text found";

            var items = GetItems();
            if (items is not null)
            {
                foreach (var item in items)
                {
                    AddItem(item);
                }

                int lastTypingTextsIndex = this.GetGlobal().ApplicationSettings.LastTypingTextsIndex;
                if (lastTypingTextsIndex < Items.Count)
                {
                    Selected = lastTypingTextsIndex;
                }
            }
        }

        private IEnumerable<string> GetItems()
        {
            List<string> items;

            using (var directory = new Directory())
            {
                var error = directory.Open(this.GetGlobal().ApplicationSettings.TextsPath);
                if (error != Error.Ok)
                {
                    return null;
                }

                items = new List<string>();
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

