using DomainBase;

namespace Domain
{
    /// <summary>
    /// ����ʵ��
    /// </summary>
    public class GoodsCategory : Entity, IAggregateRoot
    {
        public string CategoryName { get; set; }
        public int Sort { get; set; }
        public void CreateOrUpdate(string categoryName, int sort)
        {
            if (!string.IsNullOrEmpty(categoryName))
                CategoryName = categoryName;
            if (sort < 0)
                throw new DomainException("��������������0");
            Sort = sort;
        }
    }
}
