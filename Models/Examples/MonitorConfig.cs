

namespace Models;

public class  MonitorConfig : Entity
{
    public string name {get ; set;} = null!;

    public IList<string> matchableMonitors {get;set;} = null!;
    public IList<MonitorElement> elements {get ; set;} = null!;
 
}
