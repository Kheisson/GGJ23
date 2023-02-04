using System;

namespace UI
{
    public class UiManager
    {
        
        public event Action<int> OnItemSelectedEvent;
        
        public void OnItemSelected(int index)
        {
            OnItemSelectedEvent?.Invoke(index);
        }
    }
}