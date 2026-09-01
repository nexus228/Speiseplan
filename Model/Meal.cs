namespace Speiseplan.Model
{
    public class Meal
    {
        public int Id { get; set; }

        public int DayId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ReceiptURL { get; set; }

        public bool HasReceipt
        {
            get 
            {
                return !String.IsNullOrEmpty(ReceiptURL);
            }
        }

        public string? ImageURL { get; set; }

        public MealIdentifier? Identifier { get; set; }
    }
}
