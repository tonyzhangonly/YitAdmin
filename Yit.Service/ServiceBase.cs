using System;
using YiSha.Data.Repository;
using SqlSugar;

namespace Yit.Service
{
    public class ServiceBase<T>: RepositoryBase<T> where T:class,new()
    {
    }
}
