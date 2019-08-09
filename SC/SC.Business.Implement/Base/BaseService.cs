using AutoMapper;

using KMHC.SLTC.Business.Entity;
using SC.Business.Implement.Base;
using SC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SC.Business.Implement
{
    public class BaseService
    {
        public IUnitOfWork unitOfWork = IOCContainer.Instance.Resolve<IUnitOfWork>();

        public virtual BaseResponse<IList<T>> Query<S, T>(BaseRequest request, Func<IQueryable<S>, IQueryable<S>> whereAndOrderBy) where S : class
        {
            BaseResponse<IList<T>> response = new BaseResponse<IList<T>>();
            Mapper.CreateMap<S, T>();
            var q = from m in unitOfWork.GetRepository<S>().dbSet
                    select m;

            if (whereAndOrderBy != null)
            {
                q = whereAndOrderBy(q);
            }
            response.RecordsCount = q.Count();
            List<S> list = null;
            if (request != null && request.PageSize > 0)
            {
                list = q.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
                response.PagesCount = GetPagesCount(request.PageSize, response.RecordsCount);
            }
            else
            {
                list = q.ToList();
            }

            response.Data = Mapper.Map<IList<T>>(list);
            return response;
        }

        public virtual BaseResponse<T> Get<S, T>(Func<S, bool> where)
            where S : class
            where T : class
        {
            Mapper.CreateMap<S, T>();
            BaseResponse<T> response = new BaseResponse<T>();
            var findItem = unitOfWork.GetRepository<S>().dbSet.FirstOrDefault(where);
            if (findItem != null)
            {
                response.Data = Mapper.Map<T>(findItem);
            }
            return response;
        }

        public BaseResponse<T> Save<S, T>(T request, Func<S, bool> where)
            where S : class
            where T : class
        {
            return this.Save<S, T>(request, where, null, false);
        }

        public BaseResponse<T> Save<S, T>(T request, Func<S, bool> where, List<string> fields)
            where S : class
            where T : class
        {
            return this.Save<S, T>(request, where, fields, false);
        }

        public BaseResponse<T> Save<S, T>(T request, Func<S, bool> where, List<string> fields, bool reverse)
            where S : class
            where T : class
        {
            BaseResponse<T> response = new BaseResponse<T>();
            Mapper.Reset();
            var cm = Mapper.CreateMap<T, S>();
            if (fields != null)
            {
                if (reverse)
                {
                    cm.ForAllMembers(it => it.Condition(m => !fields.Contains(m.PropertyMap.SourceMember.Name)));
                }
                else
                {
                    cm.ForAllMembers(it => it.Condition(m => fields.Contains(m.PropertyMap.SourceMember.Name)));
                }
            }
            Mapper.CreateMap<S, T>();
            var model = unitOfWork.GetRepository<S>().dbSet.FirstOrDefault(where);
            if (model == null)
            {
                model = Mapper.Map<S>(request);
                unitOfWork.GetRepository<S>().Insert(model);
            }
            else
            {
                Mapper.Map(request, model);
                unitOfWork.GetRepository<S>().Update(model);
            }
            unitOfWork.Save();
            Mapper.Map(model, request);
            response.Data = request;
            return response;
        }

        public BaseResponse<IList<T>> Save<S, T>(IList<T> request, Func<S, bool> where, List<string> fields = null, bool reverse = false)
            where S : class
            where T : class
        {
            BaseResponse<IList<T>> response = new BaseResponse<IList<T>>();
            var cm = Mapper.CreateMap<T, S>();
            if (fields != null)
            {
                if (reverse)
                {
                    cm.ForAllMembers(it => it.Condition(m => !fields.Contains(m.PropertyMap.SourceMember.Name)));
                }
                else
                {
                    cm.ForAllMembers(it => it.Condition(m => fields.Contains(m.PropertyMap.SourceMember.Name)));
                }
            }
            Mapper.CreateMap<S, T>();
            foreach (var item in request)
            {
                var model = unitOfWork.GetRepository<S>().dbSet.FirstOrDefault(where);
                if (model == null)
                {
                    model = Mapper.Map<S>(item);
                    unitOfWork.GetRepository<S>().Insert(model);
                }
                else
                {
                    Mapper.Map(item, model);
                    unitOfWork.GetRepository<S>().Update(model);
                }
            }
            unitOfWork.Save();
            response.Data = request;
            return response;
        }



        public virtual BaseResponse Delete<S>(object key) where S : class
        {
            BaseResponse response = new BaseResponse();
            unitOfWork.GetRepository<S>().Delete(key);
            unitOfWork.Save();
            return response;
        }

        public int GetPagesCount(int pageSize, int total)
        {
            if (pageSize <= 0)
            {
                return 1;
            }
            var count = total / pageSize;
            if (total % pageSize > 0)
            {
                count += 1;
            }
            return count;
        }
    }
}
