using ASUCloud.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service
{
    public class BlackUserCache
    {
        private const string KEY = "asucloud:cache:user_blacklist";

        private readonly IMemoryCache _memoryCache;
        private readonly BlackUserRepository _blackUserRepository;

        public BlackUserCache(IMemoryCache memoryCache, BlackUserRepository blackUserRepository)
        {
            _memoryCache = memoryCache;
            _blackUserRepository = blackUserRepository;
        }

        public IList<string> GetBlackUserList()
        {
            if (!_memoryCache.TryGetValue<IList<string>>(KEY, out IList<string>? blackUsers) && blackUsers == null)
            {
                blackUsers = _blackUserRepository.GetBlackUserIdList();

                _memoryCache.Set<IList<string>>(KEY, blackUsers);
            }

            return blackUsers ?? new List<string>(0);
        }

        public void Reset()
        {
            _memoryCache.Remove(KEY);
        }
    }
}
