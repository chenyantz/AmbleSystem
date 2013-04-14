using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Collections;


namespace PasswordServer
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
                       BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
provider.TypeFilterLevel = TypeFilterLevel.Full;
// Creating the IDictionary to set the port on the channel instance.
IDictionary props = new Hashtable();
props["port"] = 1111;
            
            TcpServerChannel channel = new TcpServerChannel(props,provider);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(PasswordServer.DatabaseInfo), "DatabaseInfo", WellKnownObjectMode.Singleton);
        }

        protected override void OnStop()
        {
        }
    }
}
