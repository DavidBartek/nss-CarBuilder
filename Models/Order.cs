using Microsoft.Net.Http.Headers;

public class Order
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int WheelId { get; set; }
    public Wheels Wheels { get; set; }
    public int TechnologyId { get; set; }
    public Technology Technology { get; set; }
    public int PaintId { get; set; }
    public PaintColor PaintColor { get; set; }
    public int InteriorId { get; set; }
    public Interior Interior { get; set; }
    public bool Fulfilled { get; set; }
    public decimal TotalCost
    {
        get
        {
            decimal price = 0;

            if (Wheels != null)
                price += Wheels.Price;

            if (Technology != null)
                price += Technology.Price;

            if (PaintColor != null)
                price += PaintColor.Price;

            if (Interior != null)
                price += Interior.Price;
                
            return price;
        }
    }
}