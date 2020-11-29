using Gurren_Core.Logging;
using MelonLoader;

namespace Gurren_Core
{
    public class Main : MelonMod
    {
        public static readonly string currentVersion = "1.0.0";

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            MelonLogger.Log("Gurren Core has successfully loaded");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            NotificationMgr.CheckForNotifications();
        }
    }
}
