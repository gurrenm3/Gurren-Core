using Boo.Lang;
using System;

namespace Gurren_Core.Updater
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class LatestVersionURLAttribute : Attribute
    {
        internal static List<LatestVersionURLAttribute> loaded;
        public string url;
        public Type type;
        public LatestVersionURLAttribute(Type type, string url)
        {
            this.url = url;
            this.type = type;

            if (loaded == null)
                loaded = new List<LatestVersionURLAttribute>();

            if (!loaded.Contains(this))
                loaded.Add(this);
        }
    }
}