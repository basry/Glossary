
using Glossary.Models;
using Glossary.Models.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Glossary.Utils
{
    public static class Extensions
    {
        public static List<GlossaryModel> Contains
        (this List<GlossaryModel> query, string keyword = null)

        {
            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.FindAll(j => j.Term.Contains(keyword) || j.Text.Contains(keyword));
            return query;
        }


        public static List<GlossaryModel> StartsWith
            (this List<GlossaryModel> query, char character = 'a')

        {
            query = query.FindAll(j => j.Term.ToLower().StartsWith(char.ToLower(character)));
            return query;
        }


        public static bool IsValid(this GlossaryModel g)
        {
            if (string.IsNullOrEmpty(g.Text) || string.IsNullOrEmpty(g.Term))
                return false;

            return true;
        }

        public static ResponseModel<TData> Paginate<TData>
            (this IEnumerable<TData> query, int pageSize, int pageNumber)
        {
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = query.ToList().Count;
            if (totalRecords == 0)
            {
                return new ResponseModel<TData>
                {
                    Count = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0,
                    Records = null
                };


            }

            var totalPages = (int)(totalRecords / pageSize == 0 ? 1 : Math.Ceiling((double)totalRecords / pageSize));

            pageNumber = pageNumber <= 0 || pageNumber > totalPages ? 1 : pageNumber;

            var result = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();


            return new ResponseModel<TData>
            {
                Count = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                Records = result
            };
        }
    }
}
