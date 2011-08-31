// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SequencesToPagedListEnhancer.cs" company="Centro de Cálculo do ISEL - CCISEL">
//   Luís Falcão - 2011
// </copyright>
// <summary>
//   extension methods to create <see cref="PagedList{T}" /> given
//   <see cref="IEnumerable{T}" /> /&gt; or <see cref="IQueryable{T}" /> sequences.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace Mod03_ChelasMovies.Rep.Helpers.Collections 
{
    /// <summary>
    /// extension methods to create <see cref="PagedList{T}"/> given 
    /// <see cref="IEnumerable{T}"/> /> or <see cref="IQueryable{T}"/> sequences.
    /// </summary>
    public static class SequencesToPagedListEnhancer 
    {
        private static int _defaultPageSize = 4;

        public static void SetDefaultPageSize<T>(this IQueryable<T> source, int defaultPageSize) 
        {
            _defaultPageSize = defaultPageSize;
        }

        public static void SetDefaultPageSize<T>(this IEnumerable<T> source, int defaultPageSize) 
        {
            _defaultPageSize = defaultPageSize;
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index)
        {
            return new PagedList<T>(source, index, _defaultPageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int total, int index, int pageSize)
        {
            return new PagedList<T>(source, total, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int total, int index)
        {
            return new PagedList<T>(source, total, index, _defaultPageSize);
        }

        public static IPagedList<T1> Cast<T, T1>(this IPagedList<T> source) where T : T1 
        {
            IEnumerable<T1> enumDest = source.Cast<T1>();
            return new PagedList<T1>(enumDest, source.TotalCount, source.PageIndex, source.PageSize);
        }
    }
}