using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Splat;

namespace Emnd
{
    public interface IDataService
    {
    }

    public class DataService : IDataService
    {

        private INetworkService _net;
        private ICache _cache;

        public DataService(INetworkService networkService = null, ICache cacheService = null)
        {
            _net = networkService ?? Locator.Current.GetService<INetworkService>();
            _cache = cacheService ?? Locator.Current.GetService<ICache>();
        }

    }

    //public static class DataServiceExtensions
    //{
    //    public static IdValue GetIdValueFromListById(this IList<IdValue> list, string Id)
    //    {
    //        return ID.ZeroHandler<IdValue>(Id) ?? list.FirstOrCustom((arg) => arg.Id == Id, new IdValue());
    //    }
    //    public static T GetRefTypeFromListById<T>(this IList<T> list, string Id) where T : RefType, new()
    //    {
    //        return ID.ZeroHandler<T>(Id) ?? list.FirstOrCustom((arg) => arg.Id == Id, new T());
    //    }
    //}
}