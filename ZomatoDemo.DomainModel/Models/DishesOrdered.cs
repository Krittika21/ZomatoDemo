namespace ZomatoDemo.DomainModel.Models
{
    public class DishesOrdered
    {
        public int ID { get; set; }
        public int ItemsCount { get; set; }

        public virtual Dishes Dishes { get; set; }
    }
}
