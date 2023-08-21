

namespace Models;

public class  MT : Entity
{
        public string status {get ; set;} = null!;
        public MeasurementRecording  measurementRecording {get ; set;} = null!;
        public Wafer wafer {get ; set;} = null!;
        public string technologist{get ; set;} = null!;
        public Stage stage  {get ; set;} = null!;
        public Parcel parcel   {get ; set;} = null!;
        public DieType dieType {get ; set;} = null!;

        public MonitorConfig monitorConfig{get;set;} = null!;
        public string comment {get ; set;} = null!;
}
