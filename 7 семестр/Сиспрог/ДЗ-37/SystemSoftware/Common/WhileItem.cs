namespace SystemSoftware.Common
{
    public class WhileItem
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Count { get; set; }
        public int Level { get; set; }

        public override string ToString()
        {
            return $"StartIndex: {StartIndex} EndIndex: {EndIndex} Count: {Count} Level:{Level}";
        }
    }
}
