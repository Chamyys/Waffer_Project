

namespace Models;

public class  MT : Entity
{
  public MeasurementRecording  measurementRecording {get ; set;} = null!;
        public Wafer wafer {get ; set;} = null!;
        public string technologist{get ; set;} = null!;
        public Stage stage  {get ; set;} = null!;
        public Parcel parcel   {get ; set;} = null!;
        public DieType dieType {get ; set;} = null!;
}
