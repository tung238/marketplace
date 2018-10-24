using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.Infrastructure;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface ISettingDictionaryService : IService<SettingDictionary>
    {
        void SaveSettingDictionary(SettingDictionary setting);
        SettingDictionary GetSettingDictionary(int settingID, string settingKey);
    }

    public class SettingDictionaryService : Service<SettingDictionary>, ISettingDictionaryService
    {
        public SettingDictionaryService(IRepositoryAsync<SettingDictionary> repository)
            : base(repository)
        {
        }

        public void SaveSettingDictionary(SettingDictionary setting)
        {
            if (setting.ID == 0)
            {
                setting.ObjectState = ObjectState.Added;
                Insert(setting);
            }
            else
            {
                setting.ObjectState = ObjectState.Modified;
                Update(setting);
            }
        }

        public SettingDictionary GetSettingDictionary(int settingID, string settingKey)
        {
            var settingQuery = Query(x => x.Name == settingKey && x.SettingID == settingID).Select();
            var setting = settingQuery.FirstOrDefault();

            if (setting == null)
                return new SettingDictionary()
                {
                    Name = settingKey.ToString(),
                    Value = string.Empty
                };

            return setting;
        }
    }
}
