namespace DataBase.Models;

public partial class Key
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();

    public virtual ICollection<Value> Values { get; set; } = new List<Value>();
}
