namespace Models;

public class  WAFFERINLAB : Entity
{
  
    public WELCOMEBACK WelcomeBack { get; set; } = null!;
    public string LabEntryStage { get; set; } = null!;
    public string MStartStage { get; set; } = null!;
    public string UploadingStage { get; set; } = null!;
    //id + list stages + в формате

}