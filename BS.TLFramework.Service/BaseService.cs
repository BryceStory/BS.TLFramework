﻿using BS.TLFramework.IService;
using BS.TLFramework.Model;
using EntityFramework.Extensions;
using Framework.Common;
using Framework.Expression;
using Framework.Models;
using Framework.Web.IOC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;
using Unity;

namespace BS.TLFramework.Service
{
    public class BaseService<E> : IBaseService<E> where E : BaseEntity
    {
        public CurrentUser currentUser { get { return UserHelper.GetCurrentUser() ?? UserHelper.CurrentUser; } }


        public BaseService()
        {
        }

        protected DbContext dbContext
        {
            get
            {
                //获取BaseDbContext的完全限定名，其实这个名字没什么特别的意义，仅仅是一个名字而已，也可以取别的名字的  
                string threadName = typeof(DbContext).FullName;

                //获取key为threadName的这个线程缓存（CallContext就是线程缓存容器类）  
                object dbObj = CallContext.GetData(threadName);

                //如果key为threadName的线程缓存不存在  
                if (dbObj == null)
                {
                    //创建BaseDbContext类的对象实例  
                    dbObj = new TLFrameworkDb();

                    //将这个BaseDbContext类的对象实例保存到线程缓存当中(以键值对的形式进行保存的，我这就将key设为当前线程的完全限定名了)  
                    CallContext.SetData(threadName, dbObj);

                    return dbObj as DbContext;
                }
                return dbObj as DbContext;
            }

        }


        //------------------------------------------------------------查询------------------------------------------------------------



        public E Get(long ID)
        {
            var result = this.dbContext.Set<E>().Find(ID);

            return result;

        }

        public E Get(Expression<Func<E, bool>> expression)
        {
            var result = this.dbContext.Set<E>().FirstOrDefault(expression);

            return result;
        }

        public E GetLast(Expression<Func<E, bool>> expression)
        {
            IQueryable<E> query = this.dbContext.Set<E>().Where(expression);

            var result = this.ApplyOrderBy<E>(query, new OrderByExpression<E, long>(t => t.ID, true)).FirstOrDefault();

            return result;
        }

        public long Count()
        {
            var result = dbContext.Set<E>().Count();

            return result;
        }

        public long Count(Expression<Func<E, bool>> expression)
        {
            var result = dbContext.Set<E>().Count(expression);

            return result;
        }

        public bool Exists(Expression<Func<E, bool>> updateExpression)
        {
            var result = this.dbContext.Set<E>().Any(updateExpression);

            return result;
        }

        public E Get(Expression<Func<E, bool>> expression, params IOrderByExpression<E>[] orderbys)
        {
            IQueryable<E> query = this.dbContext.Set<E>().Where(expression);

            var result = this.ApplyOrderBy<E>(query, orderbys).FirstOrDefault();

            return result;
        }

        public E GetAsNoTracking(Expression<Func<E, bool>> expression)
        {
            var result = this.dbContext.Set<E>().Where(expression).AsNoTracking().FirstOrDefault();

            return result;
        }

        public IQueryable<E> Gets()
        {
            var result = this.dbContext.Set<E>();

            return result;
        }

        public IQueryable<E> Gets(Expression<Func<E, bool>> expression)
        {
            var result = this.dbContext.Set<E>().Where(expression);

            return result;
        }

        public IQueryable<E> GetsNoTracking(Expression<Func<E, bool>> expression)
        {
            var result = this.dbContext.Set<E>().Where(expression).AsNoTracking();

            return result;
        }

        public IQueryable<E> Gets(Expression<Func<E, bool>> expression, params IOrderByExpression<E>[] orderbys)
        {
            var result = this.ApplyOrderBy<E>(this.dbContext.Set<E>().Where(expression), orderbys);

            return result;
        }

        public IList<E> Gets(IQueryable<E> query, int PageIndex, int PageSize, params IOrderByExpression<E>[] orderByExpressions)
        {
            var result = this.ApplyOrderBy<E>(query, orderByExpressions).ToPagedList<E>(PageIndex, PageSize);

            return result;
        }

        public IList<LM> Gets<LM>(IQueryable<E> query, Expression<Func<E, LM>> selector, int PageIndex, int PageSize, params IOrderByExpression<E>[] orderByExpressions) where LM : BaseListModel
        {
            var result = this.ApplyOrderBy<E>(query, orderByExpressions).Select(selector).ToPagedList<LM>(PageIndex, PageSize);

            return result;
        }

        public IList<E> Gets(Expression<Func<E, bool>> expression, int PageIndex, int PageSize, params IOrderByExpression<E>[] orderbys)
        {
            IQueryable<E> query = this.dbContext.Set<E>().Where<E>(expression.Compile()).AsQueryable();

            var result = this.ApplyOrderBy<E>(query, orderbys).ToPagedList<E>(PageIndex, PageSize);

            return result;
        }

        public IList<LM> Gets<LM>(Expression<Func<E, bool>> expression, Expression<Func<E, LM>> selector, int PageIndex, int PageSize, params IOrderByExpression<E>[] orderbys) where LM : BaseListModel
        {
            // IQueryable<E> query = this.dbContext.Set<E>().Where<E>(expression);
            IQueryable<E> query = this.dbContext.Set<E>().Where<E>(expression.Compile()).AsQueryable();
            var result = this.ApplyOrderBy<E>(query, orderbys).Select(selector).ToPagedList<LM>(PageIndex, PageSize);

            return result;
        }

        public IList<E> GetsNoTracking(Expression<Func<E, bool>> expression, int PageIndex, int PageSize, params IOrderByExpression<E>[] orderbys)
        {

            // IQueryable<E> query = this.dbContext.Set<E>().Where<E>(expression).AsNoTracking();
            IQueryable<E> query = this.dbContext.Set<E>().Where<E>(expression.Compile()).AsQueryable().AsNoTracking();
            var result = this.ApplyOrderBy<E>(query, orderbys).ToPagedList<E>(PageIndex, PageSize);

            return result;


        }

        //------------------------------------------------------------新增------------------------------------------------------------
        public virtual void Add(E e)
        {
            e.CreateDatetime = DateTime.Now;

            dbContext.Set<E>().Add(e);
        }


        //------------------------------------------------------------修改------------------------------------------------------------


        public virtual void Save(E e)
        {
            if (e.ID == 0)
            {
                Add(e);
            }
            else
            {
                Update(e);
            }
        }

        public virtual void Update(E e)
        {
            if (this.dbContext.Entry(e).State == EntityState.Modified)
            {
                e.ModifyDatetime = DateTime.Now;
            }
        }

        public void Update(IQueryable<E> source, Expression<Func<E, E>> updateExpression)
        {
            source.Update<E>(updateExpression);
        }

        //------------------------------------------------------------删除------------------------------------------------------------

        public void Delete(long ID)
        {
            E t = this.Get(ID);

            if (t == null)
                throw new Exception(t.GetType().Name + " ID：" + ID + "不存在");

            this.dbContext.Set<E>().Remove(t);

        }


        public void Delete(Expression<Func<E, bool>> expression)
        {
            this.dbContext.Set<E>().Where(expression).Delete();
        }


        //------------------------------------------------------------提交------------------------------------------------------------
        public int Commit()
        {
            int count = 0;
            try
            {
                count = this.dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //FileHelper.WirteLine(message: ex.Entries.First().Entity.GetType().Name);
                throw new Exception(ex.Entries.First().Entity.GetType().Name + "保存失败,未影响任何行.");
            }
            catch (DbUpdateException ex)
            {
                //FileHelper.WirteLine(message: ex.InnerException.InnerException.Message);
                throw new Exception(ex.InnerException.InnerException.Message);
            }
            catch (DbEntityValidationException ex)
            {
                if (ex.EntityValidationErrors.Count() > 0 && ex.EntityValidationErrors.FirstOrDefault() != null && ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault() != null)
                {
                    //FileHelper.WirteLine(message: ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                    throw new Exception(ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                }
                throw ex;
            }

            return count;


        }


        #region 其它


        public IQueryable<TEntity> ApplyOrderBy<TEntity>(IQueryable<TEntity> query, params IOrderByExpression<TEntity>[] orderByExpressions) where TEntity : class
        {
            if (orderByExpressions == null)
            {
                return query;
            }
            IOrderedQueryable<TEntity> queryable = null;
            foreach (IOrderByExpression<TEntity> expression in orderByExpressions)
            {
                if (queryable == null)
                {
                    queryable = expression.ApplyOrderBy(query);
                }
                else
                {
                    queryable = expression.ApplyThenBy(queryable);
                }
            }
            IQueryable<TEntity> queryable3 = queryable;
            return (queryable3 ?? query);
        }

        public virtual BaseModel GetViewModel<M>(long? ID = null) where M : BaseModel, new()
        {
            var e = this.Get(t => t.ID == ID);
            if (e != null)
            {
                return EmitMapper.ObjectMapperManager.DefaultInstance.GetMapper<E, M>().Map(e);
            }

            return new M();
        }


        public virtual ResultModel AjaxDelete(long ID)
        {
            ResultModel result = new ResultModel();
            try
            {
                Delete(ID);
                this.Commit();
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("REFERENCE") > -1)
                {
                    result.ResultMessage = "该数据正在使用,无法删除";
                }
                else
                {
                    result.ResultMessage = ex.Message;
                }
            }
            return result;
        }

        #endregion


        /// <summary>
        /// 获取用户的数据权限
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<long?> GetDepartmentUsers(long UserID)
        {
            List<long?> list = new List<long?>();

            //IUserService userService = DIFactory.GetContainer().Resolve<IUserService>();
            IUserService userService = DIFactory.GetContainer().Resolve<IUserService>();

            var user = userService.Get(t => t.ID == UserID);

            if (user != null)
            {
                if (user.IsMore > 0)
                {
                    var DepartmentArray = user.AuthDepartmentIDs.ToLongList();
                    foreach (var m in DepartmentArray)
                    {
                        var users = userService.Gets(t => t.DepartmentID == m).Select(t => t.ID).ToList();

                        foreach (var c in users)
                        {
                            list.Add(c);
                        }

                        if (m == null)
                        {
                            list.Add(m);
                        }
                    }
                }
                else
                {
                    list.Add(user.ID);
                }

            }
            return list;
        }

    }
}
