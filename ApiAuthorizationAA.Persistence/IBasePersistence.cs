
namespace ApiAuthorizationAA.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IBasePersistence<T> where T : class
    {
        /// <summary>
        /// Encuentra todos los elementos de un conjunto
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> FindAllAsync();

        /// <summary>
        /// Encuentra todos los elementos de un conjunto, basandose en filtros e includ
        /// </summary>
        /// <param name="filter">flitro de consultas basado en Where</param>
        /// <param name="navigationProperties">Prodpiedades navegables desde la entidad</param>
        /// <returns></returns>
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Encuentra todos los elementos de un conjunto, basandose en filtros y porpiedades navegables
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="navigationProperties">Prodpiedades navegables desde la entidad</param>
        /// <returns></returns>
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>>[] filters = null, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Encuentra todos los elementos de un conjunto, basandose en filtros y porpiedades navegables
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Encuentra el primer elemento de un conjunto, basandose en filtros y porpiedades navegables
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        Task<T> FindFirstAsync(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Crea un nuevo registro en base de datos a partir de una entidad
        /// </summary>
        /// <param name="entity">Objeto generico</param>
        /// <returns></returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Edita registro en base de datos a partir de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Edit(T entity);

        Task<bool> Edit(IEnumerable<T> entities);

        /// <summary>
        /// Elmina registro en base de datos a partir de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Remove(T entity);

        /// <summary>
        /// Obtiene la cantidad de restros que cumplen con un filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Inseerta varios registros en base de datos a partir de una entidad
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Create(IEnumerable<T> entities);
    }
}
