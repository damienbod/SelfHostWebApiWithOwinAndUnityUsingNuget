using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;

namespace SelfHostWebApiOwin.Business.Logging
{
    [EventSource(Name = "UnityLogger")]
    public class UnityEventLogger : EventSource
    {
        public static readonly UnityEventLogger Log = new UnityEventLogger();

        private const int createUnityMessageEventId = 1;
        private const int disposeUnityMessageEventId = 2;
        private const int logUnityMessageEventId = 3;

        [Event(createUnityMessageEventId, Message = "CreateUnityMessage: {0}", Level = EventLevel.Informational)]
        public void CreateUnityMessage(string message)
        {
            if (IsEnabled()) WriteEvent(createUnityMessageEventId, message);
        }

        [Event(disposeUnityMessageEventId, Message = "DisposeUnityMessage {0}", Level = EventLevel.Informational)]
        public void DisposeUnityMessage(string message)
        {
            if (IsEnabled()) WriteEvent(disposeUnityMessageEventId, message);
        }

        [Event(logUnityMessageEventId, Message = "{0}", Level = EventLevel.Informational)]
        public void LogUnityMessage(string message)
        {
            if (IsEnabled()) WriteEvent(logUnityMessageEventId, message);
        }
    }



}