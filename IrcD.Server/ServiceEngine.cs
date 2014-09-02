using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace IrcD.Server
{
    public sealed class ServiceEngine : ServiceBase
    {
        public static string IrcdServiceName = "IRCd.net";
        private Thread botThread;

        public ServiceEngine()
        {
            ServiceName = IrcdServiceName;
            CanHandlePowerEvent = false;
            CanPauseAndContinue = false;
            CanHandleSessionChangeEvent = false;
            CanShutdown = true;
            CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            botThread = new Thread(Engine.Start) { IsBackground = true };
            botThread.Start();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            botThread.Abort();
            base.OnStop();
        }

        protected override void OnShutdown()
        {
            botThread.Abort();
            base.OnShutdown();
        }

        public static void WriteToLog(string message, EventLogEntryType eventLogEntryType = EventLogEntryType.Information)
        {
            if(!EventLog.SourceExists(IrcdServiceName))
            {
                EventLog.CreateEventSource(IrcdServiceName, "Application");
            }
            var eventLog = new EventLog { Source = IrcdServiceName };
            eventLog.WriteEntry(message, eventLogEntryType);
        }
    }
}