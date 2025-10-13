using Godot;

[GlobalClass]
internal sealed partial class PeriodEdit : LineEdit
{
  [Export] private Timer? _timer;
  
  public override void _Ready()
    => TextSubmitted += EditPeriod;

  private void EditPeriod(string newText)
  {
    if (_timer is null)
      return;

    if (!float.TryParse(newText, out float num))
      return;

    _timer.WaitTime = num;

    Clear();
  }
}
