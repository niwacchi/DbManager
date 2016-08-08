using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DbManager.Common
{
    class Utility
    {

        /// <summary>
        /// 指定したキーの設定値を取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        
        /// <summary>
        /// 管理対象のテーブルリスト
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTableList()
        {
            return GetConfigValue("TABLE_LIST").Split(',').ToList();
        }
    }
}
