#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CRUDelicioso.Models;
public class MyContext : DbContext 
{   
    //un dbset por cada tabla
    public DbSet<Dish> Dishes { get; set; } 
    // This line will always be here. It is what constructs our context upon initialization  
    public MyContext(DbContextOptions options) : base(options) { }    

}
