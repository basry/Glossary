using System.Collections.Generic;

namespace Glossary.Models.PaginationModels
{
    public class ResponseModel<T>
    {
        public int Count { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<T> Records { get; set; }
    }
}
