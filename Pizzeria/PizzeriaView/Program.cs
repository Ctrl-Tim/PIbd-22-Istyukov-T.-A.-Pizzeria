using PizzeriaBusinessLogic.BusinessLogics;
using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.StoragesContracts;
using PizzeriaListImplement.Implements;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace PizzeriaView
{
    static class Program
    {
        private static IUnityContainer container = null;

        public static IUnityContainer Container { get { if (container == null) { container = BuildUnityContainer(); } return container; } }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<IIngredientStorage, IngredientStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaStorage, PizzaStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageStorage, StorageStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IIngredientLogic, IngredientLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaLogic, PizzaLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageLogic, StorageLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
