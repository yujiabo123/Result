using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IP2Region;

namespace Y_Utils
{
    public class U_IP
    {

        /// <summary>
        /// 获取IP地区
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>中国|0|香港|香港|香港宽频</returns>
        public static async Task<string> GetRegionAsync(string ip, string path = "ip2region.db")
        {
            using (DbSearcher searchIp = new DbSearcher(path))
            {
                var region = await searchIp.MemorySearchAsync(ip);
                return region.Region;
            }
        }

        /// <summary>
        /// 获取IP地区
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>中国|0|香港|香港|香港宽频</returns>
        public static string GetRegion(string ip, string path = "ip2region.db")
        {
            using (DbSearcher searchIp = new DbSearcher(path))
            {
                var region = searchIp.MemorySearch(ip);
                return region.Region;
            }
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns>IP</returns>
        public static string GetIp(HttpContext context)
        {
            var headers = context.Request.Headers;
            if (!headers.AllKeys.Contains("X-Forwarded-For"))
            {
                if (!headers.AllKeys.Contains("HTTP_X_FORWARDED_FOR"))
                {
                    if (!headers.AllKeys.Contains("REMOTE_ADDR"))
                    {
                        return context.Request.UserHostAddress.ToString();
                    }
                    else
                    {
                        return headers.Get("REMOTE_ADDR");
                    }
                }
                else
                {
                    return headers.Get("HTTP_X_FORWARDED_FOR");
                }
            }
            else
            {
                return headers.Get("X-Forwarded-For");
            }
        }
    }
}
