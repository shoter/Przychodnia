using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.enums
{
    public enum PopupMessageType
    {
        Success,
        Info,
        Warning,
        Error
    }

    public static class PopupMessageTypeExtension
    {
        /// <summary>
        /// returns CSS class associated with this message type.
        /// </summary>
        /// <param name="message">message type</param>
        /// <returns>CSS class</returns>
        public static string GetMessageClass(this PopupMessageType message)
        {
            switch (message)
            {
                case PopupMessageType.Success:
                    return "alert-success";
                case PopupMessageType.Error:
                    return "alert-danger";
                case PopupMessageType.Info:
                    return "alert-info";
                case PopupMessageType.Warning:
                    return "alert-warning";
            }
            return "";
        }

    }
}