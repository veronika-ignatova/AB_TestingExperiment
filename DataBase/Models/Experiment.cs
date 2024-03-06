using Core.Interfaces.Models;

namespace DataBase.Models;

public partial class Experiment
{
    public int Id { get; set; }

    public string DeviceToken { get; set; } = null!;

    public int KeyId { get; set; }

    public int ValueId { get; set; }

    public virtual Key Key { get; set; } = null!;

    public virtual Value Value { get; set; } = null!;
}
