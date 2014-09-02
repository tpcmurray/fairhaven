using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace IrcD.Server
{
    [RunInstaller(true)]
    public class IrcdServiceInstaller : Installer
    {
        private readonly ServiceProcessInstaller processInstaller;
        private readonly ServiceInstaller serviceInstaller;

        public IrcdServiceInstaller()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;
            processInstaller.Username = null;
            processInstaller.Password = null;
            //# Service Information
            serviceInstaller.DisplayName = ServiceEngine.IrcdServiceName;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = ServiceEngine.IrcdServiceName;
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}