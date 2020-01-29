namespace Glossary.Models.PaginationModels
{
    public class RequestModel<T>
    {
        public RequestModel() { }
        public RequestModel(T filterModel)
        {
            FilterModel = filterModel;
        }
        public T FilterModel { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;

    }
}