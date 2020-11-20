using Microsoft.Extensions.Configuration;

public class AppState
{
    public AppState(IConfiguration config)
    {
        Debug = ((ConfigurationSection)config).Value=="True" ? true : false;

    }   
    public string SelectedColour { get; private set; }
    public bool Debug { get; private set; }
    public static bool IsConnected { get; set; } = false;

    public event System.Action OnChange;

    public void SetConnectionState(bool state)
    {
        IsConnected = state;
        NotifyStateChanged();
    }

    public void SetColour(string colour)
    {
        SelectedColour = colour;
        NotifyStateChanged();
    }
    //public void SetDebug(bool debug)
    //{
    //    Debug = debug;
    //    NotifyStateChanged();
    //}

    private void NotifyStateChanged() => OnChange?.Invoke();
}