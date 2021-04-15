using System;

namespace DungeonExplorer
{
    class Event
    {
        public Character Source
        {
            get; private set;
        }
        public Character Target
        {
            get; private set;
        }
        public string Description
        {
            get; private set;
        }

        public Event(Character source, Character target, string description)
        {
            Source = source;
            Target = target;
            Description = description;
        }

        override public string ToString()
        {
            string sourceTitle;
            if (Source != null)
            {
                sourceTitle = Source.Name + "(" + Source.HealthPoints + ")";
            }
            else
            {
                sourceTitle = "";
            }

            string targetTitle;
            if (Target != null)
            {
                targetTitle = Target.Name + "(" + Target.HealthPoints + ")";
            }
            else
            {
                targetTitle = "";
            }

            return String.Format(
                "{0} | {1} | {2}",
                sourceTitle,
                targetTitle,
                Description
            );
        }
    }
} // namespace DungeonExplorer

