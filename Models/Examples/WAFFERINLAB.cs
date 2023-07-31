namespace Models;

public class  WAFFERINLAB<T> : Entity  
{
  
    public string LabEntryStage { get; set; } = null!;
    public string MStartStage { get; set; } = null!;
    public string UploadingStage { get; set; } = null!;
    //id + list stages + в формате
    public List<T> Stages  { get; set; }
}