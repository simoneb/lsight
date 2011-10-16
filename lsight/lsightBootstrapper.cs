using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using lsight.Services;
using lsight.Shell;

namespace lsight
{
    public class lsightBootstrapper : Bootstrapper<IShell>
    {
        private CompositionContainer container;
        private readonly string addinsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Addins");
        readonly ILog debugLog = new DebugLog();
        private const string AddinNamePattern = "lsight.*.dll";

        protected override void Configure()
        {
            LogManager.GetLog = type => debugLog;

            container = new CompositionContainer(new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            Application.MainWindow.Height = 300;
            Application.MainWindow.Width = 300;
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return base.SelectAssemblies().Union(Addins);
        }

        private IEnumerable<Assembly> Addins
        {
            get
            {
                return Directory.Exists(addinsFolder)
                           ? Directory.EnumerateFiles(addinsFolder, AddinNamePattern).Select(Assembly.LoadFile)
                           : Enumerable.Empty<Assembly>();
            }
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            container.GetExportedValue<ISettingsStorage>().Persist();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }
    }
}