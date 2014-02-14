using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;

namespace SimpleService
{
    public delegate void ServiceManagerHandler(object sender, EventArgs args);

    public class ServiceManager<T> where T : CustomServiceBase, new()
    {
        public event ServiceManagerHandler ServiceDefaultModeStarting;
        public event ServiceManagerHandler ServiceInteractiveModeStarting;

        public void Run(bool showConsole = false)
        {
            if (!showConsole && !Debugger.IsAttached)
            {
                StartServiceDefault();
            }
            else
            {
                StartServiceInteractive();
            }
        }

        private void StartServiceDefault()
        {
            if (ServiceDefaultModeStarting != null)
                ServiceDefaultModeStarting(this, null);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
                { 
                    new T() 
                };
            ServiceBase.Run(ServicesToRun);
        }

        private void StartServiceInteractive()
        {
            if (ServiceInteractiveModeStarting != null)
                ServiceInteractiveModeStarting(this, null);
            Console.Title = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine(Assembly.GetEntryAssembly().FullName);
            T service = new T();

            Console.WriteLine();
            try
            {
                Console.WriteLine();
                Console.WriteLine("Calling StartService()");
                Console.WriteLine("-----------------------------------------------");

                service.StartService(null);

                Console.WriteLine();
                Console.WriteLine("Service running in foreground, press enter to exit...");
                Console.ReadLine();

                Console.WriteLine("Calling StopService()");
                Console.WriteLine("-----------------------------------------------");

                service.StopService();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
