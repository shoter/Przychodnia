﻿using Przychodnia.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models
{
    public class PopupMessageViewModel
    {
        public string Content { get; set; }
        public PopupMessageType MessageType { get; set; }

        public PopupMessageViewModel(string content, PopupMessageType messageType = PopupMessageType.Info)
        {
            Content = content;
            MessageType = messageType;
        }

        public PopupMessageViewModel() { }
    }
}