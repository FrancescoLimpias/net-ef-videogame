namespace net_ef_videogame
{
    /* UTILITIES for CONSOLE
     * 
     */
    internal static class UConsole
    {

        /* CURSOR LOCKING MECHANISM
         * (private, it's an internal mechanic)
         */
        protected class InputFieldLock : IDisposable
        {
            private static int FieldLength { get; set; }
            private static (int left, int top)? cursorStartPosition = null;
            private static int lockStack = 0;

            public InputFieldLock(int fieldLength = 100)
            {
                FieldLength = fieldLength;

                if (lockStack++ == 0)
                {
                    cursorStartPosition = Console.GetCursorPosition();
                }

                Clear();
            }

            public static void Clear()
            {
                Console.SetCursorPosition(cursorStartPosition.Value.left, cursorStartPosition.Value.top);
                Console.Write(new string(' ', FieldLength));
                Console.SetCursorPosition(cursorStartPosition.Value.left, cursorStartPosition.Value.top);
            }

            public static void UnlockCursor()
            {
                if (--lockStack == 0)
                {
                    cursorStartPosition = null;
                }
            }
            public void Dispose()
            {
                UnlockCursor();
            }
        }

        //ASK FOR INPUT STRING
        internal static string AskString()
        {
            //Lock cursor
            using InputFieldLock IFL = new();

            //Ask for string and check input
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return AskString();

            return input;
        }

        //ASK FOR INPUT STRING AND CAST
        internal static T AskStringToCast<T>(Func<string, T> CastingMethod)
        {
            //Lock cursor
            using InputFieldLock IFL = new();

            string input = AskString();
            T result;
            try
            {
                result = CastingMethod(input);
            }
            catch (Exception)
            {
                result = AskStringToCast(CastingMethod);
            }

            return result;
        }

        //ASK FOR INPUT STRING AND CAST TO ...
        internal static int AskInt()
        {
            return AskStringToCast((input) => Convert.ToInt32(input));
        }
        internal static long AskLong()
        {
            return AskStringToCast((input) => Convert.ToInt64(input));
        }
        internal static bool AskYesNo()
        {
            return AskStringToCast((input) =>
            {
                input = input.ToUpper();
                if (input.Equals("YES") || input.Equals("Y"))
                    return true;
                if (input.Equals("NO") || input.Equals("N"))
                    return false;
                throw new Exception();
            });
        }
    }
}
