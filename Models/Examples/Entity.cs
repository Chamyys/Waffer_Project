

namespace Models;

public class  Entity
{
    public string id {get ; set;} = null!;
    public string name { get; set; } = null!;

        public int temperature { get; set; }

        public string location { get; set; } = null!;

        public bool isRainyToday { get; set; } = false;

}
