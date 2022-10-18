namespace Test
{
    internal static class Program
    {
        static void Main()
        {
            byte a = 15;
            byte[] b = new byte[4];
            Array.Copy(BitConverter.GetBytes(a), 0, b, 0, 1);
            var c = BitConverter.ToUInt16(b, 0);
            var d = (byte)c;
        }
    }
}