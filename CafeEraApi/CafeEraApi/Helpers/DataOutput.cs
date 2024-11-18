namespace CafeEraApi.Helpers
{
    public class DataOutput
    {
        public int codeStatus { get; set; }

        public string message { get; set; }

        public IEnumerable<object> data { get; set; }

        public DataOutput() { }
    }
}
