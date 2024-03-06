namespace DataBase.Models;

public partial class Value
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Probability { get; set; }

    public int KeyId { get; set; }

    public virtual ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();

    public virtual Key Key { get; set; } = null!;
}
