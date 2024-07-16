using ASUCloud.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service
{
    public class BlackIPCache
    {
        private const string KEY = "asucloud:cache:ip_blacklist";

        private readonly IMemoryCache _memoryCache;

        private readonly BlackIPRepository _blackIPRepository;

        public BlackIPCache(IMemoryCache memoryCache, BlackIPRepository blackIPRepository)
        {
            _memoryCache = memoryCache;
            _blackIPRepository = blackIPRepository;
        }

        public IList<string> GetBlackIPList()
        {

            if (!_memoryCache.TryGetValue<IList<string>>(KEY, out IList<string>? blackIPList) && blackIPList == null)
            {
                blackIPList = _blackIPRepository.GetBlackIPList();

                _memoryCache.Set<IList<string>>(KEY, blackIPList);
            }

            return blackIPList ?? new List<string>(0);
        }

        public void Reset()
        {
            _memoryCache.Remove(KEY);
        }
    }
}
