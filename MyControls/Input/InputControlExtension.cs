namespace MyControls.Input
{
    using System;

    public static class InputControlExtension
    {
        public static void Write(this InputControl inputControl, string text)
        {
            if (inputControl == null) throw new ArgumentNullException(nameof(inputControl));
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));

            inputControl.Clear();
            inputControl.AssertValueIs(string.Empty);
            inputControl.SendKeys(text);
            inputControl.AssertValueIs(text);
        }
    }
}