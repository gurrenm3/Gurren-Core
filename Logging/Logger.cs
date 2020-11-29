using Assets.Scripts.Unity.UI_New.Popups;
using Gurren_Core.Utils;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Gurren_Core.Logging
{
    public class Logger
    {
        public static void ShowMessage(string msg, [Optional] string title, [Optional]double displayTime)
        {
            NkhText nkhText = new NkhText()
            {
                Title = title,
                Body = msg,
            };

            ShowMessage(nkhText, displayTime);
        }

        public static void ShowMessage(NkhText nkhText, [Optional]double displayTime, [Optional]NkhImage nkhImage)
        {
            if (String.IsNullOrEmpty(nkhText.Title))
            {
                var callingModInfo = MelonModInfo.GetCallingModInfo();
                string callingModName = callingModInfo.Name;

                if (callingModInfo == null || callingModName.ToLower() == "nkhook6")
                    nkhText.Title = "NKHook6";
                else
                    nkhText.Title = callingModName;
            }


            NkhMsg nkhMsg = new NkhMsg()
            {
                NkhText = nkhText,
                NkhImage = nkhImage,
                MsgShowTime = displayTime
            };

            ShowMessage(nkhMsg);
        }

        public static void ShowMessage(NkhMsg nkhMsg)
        {
            NotificationMgr.AddNotification(nkhMsg);
        }
    }
}
