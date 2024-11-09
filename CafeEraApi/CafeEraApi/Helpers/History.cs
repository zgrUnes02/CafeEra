namespace CafeEraApi.Helpers
{
    /// <summary>
    /// The History class serves as a base class for models that require auditing fields.
    /// It includes common fields for tracking the creation, updating, and deletion of records,
    /// </summary>
    public class History
    {
        public DateTime created_at {  get; set; }
        public DateTime? updated_at {  get; set; }
        public DateTime? deleted_at {  get; set; }

        public string created_by { get; set; }
        public string? updated_by { get; set; }
        public string? deleted_by { get; set; }

        public History() 
        { 
        
        }
    }
}
