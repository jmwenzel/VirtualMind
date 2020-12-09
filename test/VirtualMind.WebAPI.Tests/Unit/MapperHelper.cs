using AutoMapper;
using VirtualMind.WebAPI.App.Mappings;

namespace VirtualMind.WebAPI.Tests.Unit
{
    public class MapperHelper
    {
        private static readonly object SyncObj = new object();
        private static bool _created;

        private static IMapper mapper;
        public static IMapper InitMappings()
        {
            lock (SyncObj)
                if (!_created)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.AddProfile<TransactionMaps>();
                    });
                    //
                    mapper = config.CreateMapper();
                    _created = true;
                }
            //
            return mapper;
        }
    }
}
