using Gurren_Core.Logging;
using MelonLoader;

namespace Gurren_Core
{
    internal class MelonMain : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            MelonLogger.Log("Mod has successfully loaded");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            NotificationMgr.CheckForNotifications();
        }
    }
}
