
public class Program
{
    public static void Main(string[] args)
    {
        Animal Plinker, Croncher;
        var animals = new List<Animal>
        {
            (Plinker = new Animal(0,  "Plinker", "evil", 999, "orange")),
            (Croncher = new Animal(1, "Croncher", "mischief", 999, "white"))
        };
        var visits = new List<Visit>
        {
            new Visit(0, DateTime.Now, Plinker, "Cat plinked", 0),
            new Visit(1, DateTime.Now, Croncher, "Cat cronched", 1111)
        };

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        WebApplication app = builder.Build();



        // Animals
        app.MapGet("/animals", () =>
        {
            return Results.Ok(animals);
        });
        app.MapGet("/animals/{id}", (int id) =>
        {
            var animal = animals.FirstOrDefault(e => e.Id == id);
            if (animal is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(animal);
        });
        app.MapPost("/animals", (Animal animal) =>
        {
            animal.Id = animals.Count + 1;
            animals.Add(animal);
            return Results.Created($"/animals/{animal.Id}", animal);
        });
        app.MapPut("/animals/{id}", (int id, Animal animal) =>
        {
            var existingAnimal = animals.FirstOrDefault(e => e.Id == id);
            if (existingAnimal is null)
                return Results.NotFound();

            existingAnimal.Name = animal.Name;
            existingAnimal.Category = animal.Category;
            existingAnimal.Weight = animal.Weight;
            existingAnimal.FurColor = animal.FurColor;

            return Results.Ok(existingAnimal);
        });
        app.MapDelete("/animals/{id}", (int id) =>
        {
            var animal = animals.FirstOrDefault(e => e.Id == id);
            if (animal is null)
                return Results.NotFound();

            animals.Remove(animal);
            return Results.NoContent();
        });


        // Visits
        app.MapGet("/visits", () =>
        {
            Results.Ok(visits); 
        });
        app.MapGet("/visits/{animalId}", (int animalId) =>
        {
            var animalVisits = visits.Where(e => e.Animal.Id == animalId).ToList();
            return Results.Ok(animalVisits);
        });
        app.MapPost("/visits", (Visit visit) =>
        {
            visit.Id = visits.Count + 1;
            visits.Add(visit);
            return Results.Created($"/visits/{visit.Id}", visit);
        });



        app.UseSwagger();
        app.UseSwaggerUI();
        app.Run();
    }
}
