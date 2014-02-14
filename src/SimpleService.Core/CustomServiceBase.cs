using System.ServiceProcess;

namespace SimpleService
{
    public class CustomServiceBase : ServiceBase
    {
        public void StartService(string[] args)
        {
            OnStart(args);
        }

        public void StopService()
        {
            OnStop();
        }
    }
}
