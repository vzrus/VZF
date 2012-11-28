using System;

public class ConnectionParams
{
    public string Name { get; set; }

    public Type Type { get; set; }

    public object Value { get; set; }

    private bool _visible;

    public bool Visible
    {
        get { return true; }
        set { _visible = value; }
    }

    
}
