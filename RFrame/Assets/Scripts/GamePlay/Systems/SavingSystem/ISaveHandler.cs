public interface ISaveHandler
{
    public void DoRegister(ISaveHandler handler)
    {
        SaveManager.Instance.Register(handler);
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