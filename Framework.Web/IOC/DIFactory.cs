using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Framework.Web.IOC
{
    public class DIFactory
    {
        private static object _SyncHelper = new object();
        private static Dictionary<string, IUnityContainer> _UnityContainerDictionary = new Dictionary<string, IUnityContainer>();

        /// <summary>
        /// 根据containerName获取指定的Container
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static IUnityContainer GetContainer(string containerName = "Containerl")
        {
            if (!_UnityContainerDictionary.ContainsKey(containerName))
            {
                lock (_SyncHelper)
                {
                    if (!_UnityContainerDictionary.ContainsKey(containerName))
                    {
                        //配置 UnityContainer
                        IUnityContainer container = new UnityContainer();
                        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                        fileMap.ExeConfigFilename = Path.Combine(GetConfigFile());
                        System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                        UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
                        configSection.Configure(container, containerName);

                        _UnityContainerDictionary.Add(containerName, container);
                    }
                }
            }
            return _UnityContainerDictionary[containerName];
        }

        private static string GetConfigFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config";
            if (File.Exists(path))
                return path;

            path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "CfgFiles\\Unity.Config";
            if (File.Exists(path))
                return path;

            throw new Exception("未找到配置文件Unity.Config");
        }
    }
}
