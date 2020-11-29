using Gurren_Core.Updater;
using Gurren_Core.Web;
using System;
using System.Security.Policy;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Gurren_Core.Utils
{
    class UpdateMgr
    {
        /// <summary>
        /// This method will check all of the loaded mods for updates and post notifications in console
        /// </summary>
        public static async void HandleUpdates()
        {
            await Task.Run(() =>
            {
                foreach (var item in LatestVersionURLAttribute.loaded)
                {
                    var melonInfo = MelonModInfo.GetModInfo(item.type.Assembly);

                    UpdateHandler u = new UpdateHandler()
                    {
                        VersionURL = item.url,
                        ProjectName = melonInfo.Name,
                        CurrentVersion = melonInfo.Version
                    };
                    u.HandleUpdates(false, false);
                }
            });
        }
    }
}