public interface ISaveHandler
{
    public void Register();

    public void DoRegister()
    {
        SaveManager.Instance.Register(this);
    }
    
    /// <summary>
    /// Generate a SaveData
    /// </summary>
    /// <returns></returns>
    public SaveData GenerateSaveData();
    
    /// <summary>
    /// Reload GameData
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadSaveData(SaveData saveData);
}