using System;
using System.Diagnostics;

namespace VSLiveConnectedApp.Utils
{
    public static class AsyncErrorHandler
    {
        public static void HandleException(Exception exception)
        {
            Debug.WriteLine(exception.Message);

            // TODO: handle the exception in your own way, e.g. reporting it to
            // an online application crash analytics service
        }
    }
}
