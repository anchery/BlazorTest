public class AppState
{
    public string SelectedColour { get; private set; }
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

    private void NotifyStateChanged() => OnChange?.Invoke();
}