public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public string FurColor { get; set; }
    public Animal(int id, string name, string category, double weight, string furColor)
    {
        Id = id;
        Name = name;
        Category = category;
        Weight = weight;
        FurColor = furColor;
    }
}

public class Visit
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Animal Animal { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Visit(int id, DateTime date, Animal animal, string description, decimal price)
    {
        Id = id;
        Date = date;
        Animal = animal;
        Description = description;
        Price = price;
    }
}