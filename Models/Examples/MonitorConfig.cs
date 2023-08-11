

namespace Models;

public class  MonitorConfig : Entity
{
    public string name {get ; set;} = null!;
    public IList<string> elements {get ; set;} = null!;
 
}
